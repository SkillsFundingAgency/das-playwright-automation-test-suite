﻿//namespace SFA.DAS.ApprenticeAmbassadorNetwork.UITests.Project.Hooks;

//[Binding, Scope(Tag = "@aanadmin")]
//public class AANAdminHooks(ScenarioContext context) : AANBaseHooks(context)
//{
//    [BeforeScenario(Order = 31)]
//    public void Navigate_Admin() => tabHelper.GoToUrl(UrlConfig.AAN_Admin_BaseUrl);

//    [BeforeScenario(Order = 33)]
//    public void SetUpDataHelpers()
//    {
//        if (context.ScenarioInfo.Tags.Contains("aanadmincreateevent"))
//        {
//            context.Set(new AanAdminCreateEventDatahelper());
//        }

//        if (context.ScenarioInfo.Tags.Contains("aanadminchangeevent"))
//        {
//            context.Set(new AanAdminUpdateEventDatahelper());
//        }
//    }

//    [AfterScenario(Order = 30), Scope(Tag = "@aanadmincreateevent")]
//    public void DeleteAdminCreatedEvent()
//    {
//        if (context.TestError != null) return;

//        context.Get<TryCatchExceptionHelper>().AfterScenarioException(() =>
//        {
//            var eventId = context.Get<ObjectContext>().GetAanAdminEventId();

//            if (!string.IsNullOrEmpty(eventId)) context.Get<AANSqlHelper>().DeleteAdminCreatedEvent(eventId);
//        });
//    }

//    [AfterScenario(Order = 34), Scope(Tag = "@aanadn01b")]
//    public void DeleteLocationFilterEvents()
//    {
//        context.Get<TryCatchExceptionHelper>().AfterScenarioException(() =>
//        {
//            context.Get<AANSqlHelper>().DeleteLocationFilterEventsBeginning("Location Filter Test");
//        });
//    }
//}
