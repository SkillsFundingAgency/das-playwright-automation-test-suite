﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.FrameworkHelpers
{
    public static class OC_TestDataListExtension
    {
        #region Constants
        private const string AfterStepInformations = "afterstepinformations";
        private const string AfterScenarioExceptions = "afterscenarioexceptions";
        private const string DebugInformations = "testdebuginformations";
        private const string RetryInformations = "testretryinformations";

        #endregion

        public static void SetTestDataList(this ObjectContext objectContext, string[] tags)
        {
            objectContext.SetDebugInformations(tags);
            objectContext.SetAfterStepInformations();
            objectContext.SetAfterScenarioExceptions();
            objectContext.SetRetryInformations();
        }

        #region AfterStepInformations

        private static void SetAfterStepInformations(this ObjectContext objectContext) => objectContext.Set(AfterStepInformations, new List<string>() { $"{string.Empty}" });

        public static void SetAfterStepInformation(this ObjectContext objectContext, string value) => objectContext.GetAfterStepInformations().Add(value);

        private static List<string> GetAfterStepInformations(this ObjectContext objectContext) => objectContext.Get<List<string>>(AfterStepInformations);
        #endregion

        #region AfterScenarioExceptions

        private static void SetAfterScenarioExceptions(this ObjectContext objectContext) => objectContext.Set(AfterScenarioExceptions, new FrameworkList<Exception>() { null });

        public static void SetAfterScenarioException(this ObjectContext objectContext, Exception value) => objectContext.GetAfterScenarioExceptions().Add(value);

        public static FrameworkList<Exception> GetAfterScenarioExceptions(this ObjectContext objectContext) => objectContext.Get<FrameworkList<Exception>>(AfterScenarioExceptions);
        #endregion

        #region RetryInformations

        private static void SetRetryInformations(this ObjectContext objectContext) => objectContext.Set(RetryInformations, new FrameworkList<string>() { $"{string.Empty}" });

        public static void SetRetryInformation(this ObjectContext objectContext, string value) => objectContext.GetRetryInformations().Add($"{value}");

        private static FrameworkList<string> GetRetryInformations(this ObjectContext objectContext) => objectContext.Get<FrameworkList<string>>(RetryInformations);
        #endregion

        #region DebugInformations

        private static void SetDebugInformations(this ObjectContext objectContext, string[] tags)
        {
            objectContext.Set(DebugInformations, new FrameworkList<string>() { $"{string.Empty}" });

            objectContext.SetDebugInformation($"Scenario tags - {string.Join(", ", tags.Select(x => $"@{x}"))}");
        }

        public static void SetDebugInformation(this ObjectContext objectContext, string value) => objectContext.GetDebugInformations().Add($"-> {DateTime.UtcNow:dd/MM HH:mm:ss}: {value}");

        public static List<string> GetDebugInformations(this ObjectContext objectContext, string value) => objectContext.GetDebugInformations().Where(x => x.Contains(value)).ToList();

        private static FrameworkList<string> GetDebugInformations(this ObjectContext objectContext) => objectContext.Get<FrameworkList<string>>(DebugInformations);
        #endregion

    }
}