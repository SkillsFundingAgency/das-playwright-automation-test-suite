using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.Framework
{
    public abstract class BasePage
    {
        protected readonly ScenarioContext context;

        protected readonly Driver driver;

        protected abstract Task VerifyPage();

        protected BasePage(ScenarioContext context)
        {
            this.context = context;

            driver = context.Get<Driver>();

            Task.Run(VerifyPage).Wait();
        }
    }
}
