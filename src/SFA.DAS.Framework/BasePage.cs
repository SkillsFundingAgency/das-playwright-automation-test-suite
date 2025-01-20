using Microsoft.Playwright;
using SFA.DAS.FrameworkHelpers;
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
    }
}
