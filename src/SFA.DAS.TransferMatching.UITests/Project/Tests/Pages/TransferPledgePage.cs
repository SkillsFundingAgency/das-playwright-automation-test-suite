using System.IO;
using System.IO.Compression;
using System.Text;
using SFA.DAS.EmployerPortal.UITests.Project;

namespace SFA.DAS.TransferMatching.UITests.Project.Tests.Pages;

public class TransferPledgePage(ScenarioContext context) : TransferMatchingBasePage(context)
{
    protected override string PageTitle => $"Transfer pledge {GetPledgeId()}";
    private ILocator PledgedFunds => page.Locator("#main-content > div.govuk-width-container > div:nth-child(3) > div:nth-child(1) > div > p.govuk-heading-l.app-data__figure");
    private ILocator EstimatedRemainingFunds => page.Locator("#main-content > div.govuk-width-container > div:nth-child(3) > div:nth-child(2) > div > p.govuk-heading-l.app-data__figure");

    public async Task<TransferPledgePage> VerifyPledgeAmount()
    {
        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Pledged funds", Exact = true })).ToBeVisibleAsync();
        await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Remaining funds", Exact = true })).ToBeVisibleAsync();
        return this;
    }

    public async Task<TransferPledgePage> VerifyClosePledgeButtonIsNotDisplayed()
    {
        await Assertions.Expect(page.Locator("#close-pledge-button")).ToHaveCountAsync(0);
        return this;
    }

    public async Task<int> GetEstimatedRemainingFunds() => ReadAmount(await EstimatedRemainingFunds.InnerTextAsync());
    public async Task<int> GetTotalPledgeFunds() => ReadAmount(await PledgedFunds.InnerTextAsync());

    public async Task<TransferPledgePage> VerifyCostingModel()
    {
        var expected = ReadAmount(tMDataHelper.GetEstimatedCostOfTrainingForApplicationDetail());
        var pledged = await GetTotalPledgeFunds();
        var remaining = await GetEstimatedRemainingFunds();
        NUnit.Framework.Assert.That(pledged - remaining, NUnit.Framework.Is.EqualTo(expected));
        return this;
    }

    public async Task<ClosePledgePage> ClosePledge()
    {
        await page.Locator("#close-pledge-button").ClickAsync();
        return await VerifyPageAsync(() => new ClosePledgePage(context));
    }

    public async Task<TransferPledgePage> DownloadExcel()
    {
        var download = await page.RunAndWaitForDownloadAsync(async () =>
            await page.Locator("#main-content > div > div:nth-child(1) > div.govuk-grid-column-one-third > p > a").ClickAsync());
        NUnit.Framework.Assert.That(await download.FailureAsync(), NUnit.Framework.Is.Null, "The pledge export download failed");
        NUnit.Framework.Assert.That(Path.GetExtension(download.SuggestedFilename).ToLowerInvariant(),
            NUnit.Framework.Is.AnyOf(".xlsx", ".xls", ".csv"), "Expected an Excel-compatible pledge export");
        await using var stream = await download.CreateReadStreamAsync();
        NUnit.Framework.Assert.That(stream, NUnit.Framework.Is.Not.Null);
        using var contents = new MemoryStream();
        await stream.CopyToAsync(contents);
        NUnit.Framework.Assert.That(contents.Length, NUnit.Framework.Is.GreaterThan(0), "The pledge export is empty");
        contents.Position = 0;
        var extension = Path.GetExtension(download.SuggestedFilename);
        if (extension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            using var workbook = new ZipArchive(contents, ZipArchiveMode.Read);
            NUnit.Framework.Assert.That(workbook.GetEntry("xl/workbook.xml"), NUnit.Framework.Is.Not.Null,
                "The downloaded export is not an Excel workbook");
            NUnit.Framework.Assert.That(workbook.Entries.Any(entry =>
                entry.FullName.StartsWith("xl/worksheets/", StringComparison.Ordinal) && entry.Length > 0),
                NUnit.Framework.Is.True, "The workbook has no worksheet data");
        }
        else if (extension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
        {
            var csv = Encoding.UTF8.GetString(contents.ToArray());
            NUnit.Framework.Assert.That(csv.Split('\n', StringSplitOptions.RemoveEmptyEntries).Length,
                NUnit.Framework.Is.GreaterThan(1), "The export must contain headings and application data");
        }
        else
        {
            var bytes = contents.ToArray();
            NUnit.Framework.Assert.That(bytes.Take(4), NUnit.Framework.Is.EqualTo(new byte[] { 0xD0, 0xCF, 0x11, 0xE0 }),
                "The downloaded export is not an Excel binary workbook");
        }
        return await VerifyPageAsync(() => this);
    }

    public async Task<TransferPledgePage> ConfirmApplicationStatus(string expected)
    {
        await Assertions.Expect(page.Locator("#ApplicationsToReject > fieldset > table > tbody > tr > td:nth-child(6) > strong"))
            .ToHaveTextAsync(expected);
        return this;
    }

    public async Task<ApproveAppliationPage> GoToApproveAppliationPage() =>
        await FollowLink(objectContext.GetOrganisationName(), () => new ApproveAppliationPage(context));

    public async Task<TransferPledgePage> SortByApplicant()
    {
        foreach (var name in new[] { "Applicant", "Estimated yearly cost", "Typical duration", "Criteria", "Status" })
        {
            await page.GetByRole(AriaRole.Link, new() { Name = name, Exact = true }).ClickAsync();
            await VerifyPage();
        }
        return this;
    }

    public async Task<RejectingTheApprenticeshipApplicationsPage> SelectBulkReject()
    {
        await page.Locator(".govuk-checkboxes__input").First.CheckAsync();
        await page.Locator("#applications-action").ClickAsync();
        return await VerifyPageAsync(() => new RejectingTheApprenticeshipApplicationsPage(context));
    }
}
