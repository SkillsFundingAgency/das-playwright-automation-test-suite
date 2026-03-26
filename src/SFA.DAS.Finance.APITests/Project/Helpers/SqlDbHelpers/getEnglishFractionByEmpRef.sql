SELECT TOP (1000)
    [Id],
    [DateCalculated],
    [Amount],
    [EmpRef],
    [DateCreated]
FROM [employer_financial].[EnglishFraction]
WHERE EmpRef = '<empRef>'
ORDER BY Id DESC
