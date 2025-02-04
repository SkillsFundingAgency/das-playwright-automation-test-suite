using Polly;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.FrameworkHelpers;

public class SqlDbRetryHelper
{
    internal static async Task<T> RetryOnException<T>(Func<Task<T>> func) => await RetryOnException(func, "Exception occurred while executing SQL query", string.Empty);

    private static async Task<T> RetryOnException<T>(Func<Task<T>> func, string exception, string title, TimeSpan[] timeSpans = null)
    {
        timeSpans ??= RetryTimeOut.Timeout();

        return await Policy
            .Handle<Exception>((x) => x.Message.Contains(exception))
             .WaitAndRetryAsync(timeSpans, (exception, timeSpan, retryCount, context) =>
             {
                 Logging.Report(retryCount, timeSpan, exception, "SqlDbRetryHelper", title);
             })
             .ExecuteAsync(func);
    }
}
