namespace SFA.DAS.Campaigns.UITests.Project.Tests.Pages;

public class CampaingnsVerifyLinks(ScenarioContext context) : CampaingnsBasePage(context)
{
    public override async Task VerifyPage() => await Task.CompletedTask;

    private ILocator Links => page.GetByRole(AriaRole.Link);

    private ILocator VideoLinks => page.Locator(".fiu-video-player iframe");

    public async Task VerifyLinks() => await VerifyLinks(Links, AttributeHelper.Href, async (x) => await x.TextContentAsync());

    public async Task VerifyVideoLinks() => await VerifyLinks(VideoLinks, "src", async (x) => await x?.GetAttributeAsync("title"));

    public async Task VerifyLinks(ILocator locator, string attributeName, Func<ILocator, Task<string>> func)
    {
        var internalLinks = await locator.AllAsync();

        foreach (var item in internalLinks)
        {
            var attributeValue = await item.GetAttributeAsync(attributeName);

            var text = await func(item);

            var msg = $"'{text}' element's, '{attributeName}' attribute - attributeValue : '{attributeValue}'";

            if (string.IsNullOrEmpty(attributeValue) && !string.IsNullOrEmpty(text))
                throw new Exception($"{msg} is broken");

            objectContext.SetDebugInformation(msg);
        }
    }
}
