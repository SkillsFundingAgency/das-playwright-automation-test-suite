SELECT TOP (1)
    PeriodEndId,
    CalendarPeriodMonth,
    CalendarPeriodYear,
    AccountDataValidAt,
    CommitmentDataValidAt,
    CompletionDateTime,
    PaymentsForPeriod
FROM [employer_financial].[PeriodEnd]
WHERE PeriodEndId = '<periodEndId>'
ORDER BY Id DESC
