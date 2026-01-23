    SELECT 
        -- Id as id,
        PeriodEndId as periodEndId, 
        CalendarPeriodMonth as calendarPeriodMonth,
        CalendarPeriodYear as calendarPeriodYear,
        AccountDataValidAt as accountDataValidAt,
        CommitmentDataValidAt as commitmentDataValidAt,
        CompletionDateTime as completionDateTime
        -- PaymentsForPeriod as paymentsForPeriod  
        FROM employer_financial.PeriodEnd
