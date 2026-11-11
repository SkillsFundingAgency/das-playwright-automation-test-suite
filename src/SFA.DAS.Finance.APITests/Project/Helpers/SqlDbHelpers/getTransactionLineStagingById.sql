SELECT
    Id,
    AccountId,
    DateCreated,
    SubmissionId,
    TransactionDate,
    TransactionType,
    LevyDeclared,
    Amount,
    EmpRef,
    PeriodEnd,
    Ukprn,
    SfaCoInvestmentAmount,
    EmployerCoInvestmentAmount,
    EnglishFraction,
    TransferSenderAccountId,
    TransferSenderAccountName,
    TransferReceiverAccountId,
    TransferReceiverAccountName
FROM [employer_financial].[TransactionLineStaging]
WHERE accountId = '<accountId>'
