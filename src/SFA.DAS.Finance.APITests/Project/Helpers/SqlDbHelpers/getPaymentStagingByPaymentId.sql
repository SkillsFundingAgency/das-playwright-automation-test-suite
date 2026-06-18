SELECT
    PaymentId,
    Ukprn,
    Uln,
    AccountId,
    ApprenticeshipId,
    DeliveryPeriodMonth,
    DeliveryPeriodYear,
    CollectionPeriodId,
    CollectionPeriodMonth,
    CollectionPeriodYear,
    FundingSource,
    TransactionType,
    Amount,
    EvidenceSubmittedOn,
    EmployerAccountVersion,
    ApprenticeshipVersion
FROM [employer_financial].[PaymentStaging]
WHERE PaymentId = '<paymentId>'
