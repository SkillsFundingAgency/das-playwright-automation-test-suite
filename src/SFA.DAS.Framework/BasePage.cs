﻿using Microsoft.Playwright;
using SFA.DAS.FrameworkHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework
{
    public abstract class BasePage
    {
        protected readonly ScenarioContext context;

        protected readonly ObjectContext objectContext;

        protected readonly Driver driver;

        protected readonly IPage page;

        protected static float LandingPageTimeout => 60000;

        /// <summary>
        /// The result of the asynchronous verification of this instance.
        /// </summary>
        public abstract Task VerifyPage();

        protected BasePage(ScenarioContext context)
        {
            this.context = context;

            objectContext = context.Get<ObjectContext>();

            driver = context.Get<Driver>();

            page = driver.Page;
        }

        public static async Task<T> VerifyPageAsync<T>(Func<T> func) where T : BasePage
        {
            var nextPage = func.Invoke();

            await nextPage.VerifyPage();

            return nextPage;
        }

        protected void VerifyPage(IReadOnlyList<string> actual, string expected)
        {
            if (actual.Any(x => x.ContainsCompareCaseInsensitive(expected)))
            {
                objectContext.SetDebugInformation(MessageHelper.OutputMessage("Verified page", [expected], actual));

                return;
            }

            throw new Exception(MessageHelper.GetExceptionMessage("Page", expected, actual));
        }
    }
}
