using NUnit.Framework;
using Polly;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SFA.DAS.FrameworkHelpers
{
    public class RetryHelper(ScenarioInfo scenarioInfo, ObjectContext objectContext)
    {
        private readonly string _title = scenarioInfo.Title;

        public async Task RetryOnEmpHomePage<T>(Func<Task> func, Func<Task<T>> retryfunc) => await RetryOnException(func, RetryTimeOut.GetTimeSpan([5, 8, 13]), retryfunc);

        public void RetryOnNUnitException(Action action, TimeSpan[] timespan)
        {
            var logging = GetRetryLogging();

            Policy
                 .Handle<AssertionException>()
                 .Or<MultipleAssertException>()
                 .WaitAndRetry(timespan, (exception, timeSpan, retryCount, context) =>
                 {
                     logging.Report(retryCount, timeSpan, exception, _title);
                 })
                 .Execute(() =>
                 {
                     using var testcontext = new NUnit.Framework.Internal.TestExecutionContext.IsolatedContext();

                     action.Invoke();
                 });
        }

        private async Task RetryOnException<T>(Func<Task> func, TimeSpan[] timespan, Func<Task<T>> retryfunc)
        {
            var logging = GetRetryLogging();

            await Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(timespan, async (exception, timeSpan, retryCount, context) =>
                    {
                        logging.Report(retryCount, timeSpan, exception, _title);

                        await retryfunc.Invoke();
                    })
                    .ExecuteAsync(async () =>
                    {
                        using var testcontext = new NUnit.Framework.Internal.TestExecutionContext.IsolatedContext();

                        await func.Invoke();
                    });
        }

        private RetryLogging GetRetryLogging() => new(objectContext, RandomDataGenerator.GenerateRandomWholeNumber(4));
    }

    public class RetryLogging(ObjectContext objectContext, string uniqueIdentifier)
    {
        public void Report(int retryCount, TimeSpan timeSpan, Exception exception, string scenarioTitle)
        {
            objectContext.SetDebugInformation($"RETRY Count : {retryCount}, UniqueIdentifier : {uniqueIdentifier}");

            objectContext.SetRetryInformation(Logging.Message(retryCount, timeSpan, exception, scenarioTitle, uniqueIdentifier));
        }

    }
}