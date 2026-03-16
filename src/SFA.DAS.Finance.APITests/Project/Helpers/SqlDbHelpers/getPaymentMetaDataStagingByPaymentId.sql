SELECT TOP (1)
    PaymentId,
    ProviderName,
    StandardCode,
    FrameworkCode,
    ProgrammeType,
    PathwayCode,
    PathwayName,
    ApprenticeshipCourseName,
    ApprenticeshipCourseStartDate,
    ApprenticeshipCourseLevel,
    ApprenticeName,
    ApprenticeNINumber,
    IsHistoricProviderName,
    CreatedBy,
    CorrelationId
FROM [employer_financial].[PaymentMetaDataStaging]
WHERE PaymentId = '<paymentId>'
ORDER BY Id DESC
