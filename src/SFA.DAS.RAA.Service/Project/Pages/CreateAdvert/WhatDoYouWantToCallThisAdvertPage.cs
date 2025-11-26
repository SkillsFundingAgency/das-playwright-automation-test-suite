namespace SFA.DAS.RAA.Service.Project.Pages.CreateAdvert;

public class WhatDoYouWantToCallThisAdvertPage(ScenarioContext context) : BaseVacancyTitlePage(context)
{
    public override async Task VerifyPage()
    {
        string PageTitle = isRaaEmployer ? "What do you want to call this advert?" : "What do you want to call this vacancy?";

        await Assertions.Expect(page.Locator("label")).ToContainTextAsync(PageTitle);
    }
}
