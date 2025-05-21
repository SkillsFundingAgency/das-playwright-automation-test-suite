﻿namespace SFA.DAS.API.Framework;

public static class ScenarioContextExtension
{
    #region Constants
    private static string RestClientKey<T>() => $"{(typeof(T).Name)}_restclient";
    #endregion

    #region Setters
    public static void SetRestClient<T>(this ScenarioContext context, T value) => Set(context, value, RestClientKey<T>());
    private static void Set<T>(ScenarioContext context, T value, string key) => context.Set(value, key);
    #endregion

    #region Getters
    public static Outer_ApiAuthTokenConfig GetOuter_ApiAuthTokenConfig(this ScenarioContext context) => context.Get<Outer_ApiAuthTokenConfig>();
    public static ApprenticeCommitmentsJobsAuthTokenConfig GetApprenticeCommitmentsJobsAuthTokenConfig(this ScenarioContext context) => context.Get<ApprenticeCommitmentsJobsAuthTokenConfig>();
    public static T GetRestClient<T>(this ScenarioContext context) => Get<T>(context, RestClientKey<T>());
    public static T Get<T>(ScenarioContext context, string key) => context.GetValue<T>(key);
    #endregion
}