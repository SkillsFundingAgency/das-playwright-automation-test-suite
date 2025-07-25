using System;

namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Tests.Pages.AppEmpCommonPages;

public class SearchNetworkEventsPage(ScenarioContext context) : SearchEventsBasePage(context)
{
    public override async Task VerifyPage()
    {
        await Assertions.Expect(page.Locator("h1")).ToContainTextAsync("Search network events");
    }

    //Clicking on last event so that the event would be for sure in the future
    public async Task<EventPage> ClickOnLastEvent()
    {
        await FilterEventFromTomorrow();

        await page.Locator(EventLink).Last.ClickAsync();

        return new EventPage(context);
    }

    public new async Task FilterEventByOneMonth()
    {
        await base.FilterEventByOneMonth();
    }

    public async Task FilterEventsWithNoResults()
    {
        await EnterKeywordFilter(Guid.NewGuid().ToString());

        await ApplyFilter();
        
    }

    public new async Task FilterEventByEventFormat_InPerson()
    {
        await base.FilterEventByEventFormat_InPerson();
        
    }

    public new async Task FilterEventByEventFormat_Online()
    {
        await base.FilterEventByEventFormat_Online();
        
    }

    public new async Task FilterEventByEventFormat_Hybrid()
    {
        await base.FilterEventByEventFormat_Hybrid();
        
    }

    public new async Task FilterEventByEventType_TrainingEvent()
    {
        await base.FilterEventByEventType_TrainingEvent();
        
    }

    public new async Task FilterEventByEventRegion_London()
    {
        await base.FilterEventByEventRegion_London();
        
    }

    public new async Task FilterEventByEventStatus_Cancelled()
    {
        await base.FilterEventByEventStatus_Cancelled();
        
    }

    public new async Task ClearAllFilters()
    {
        await base.ClearAllFilters();
        
    }
    public new async Task VerifyEventType_TrainingEvent_Filter()
    {
        await base.VerifyEventType_TrainingEvent_Filter();
        
    }

    public new async Task VerifyEventFormat_Inperson_Filter()
    {
        await base.VerifyEventFormat_Inperson_Filter();
        
    }

    public new async Task VerifyEventFormat_Online_Filter()
    {
        await base.VerifyEventFormat_Online_Filter();
        
    }

    public new async Task VerifyEventFormat_Hybrid_Filter()
    {
        await base.VerifyEventFormat_Hybrid_Filter();
        
    }

    public new async Task VerifyEventRegion_London_Filter()
    {
        await base.VerifyEventRegion_London_Filter();
        
    }
}