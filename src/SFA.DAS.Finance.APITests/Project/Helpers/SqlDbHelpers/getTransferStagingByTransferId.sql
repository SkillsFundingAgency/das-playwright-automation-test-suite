SELECT
    TransferId,
    SenderAccountId,
    ReceiverAccountId,
    ReceiverAccountName,
    Amount,
    TransferDate,
    PeriodEnd,
    CollectionPeriodMonth,
    CollectionPeriodYear,
    Ukprn,
    CourseName,
    CreatedBy,
    CorrelationId
FROM [employer_financial].[TransferStaging]
WHERE TransferId = <transferId>
