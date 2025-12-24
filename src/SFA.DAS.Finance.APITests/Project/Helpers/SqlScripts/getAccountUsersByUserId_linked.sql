SELECT TOP 1 mv.UserRef as userRef, mv.Email as Email,
    mv.UserRef as employerUserId,
    mv.HashedAccountId as encodedAccountId,
    mv.AccountName as dasAccountName,
    CASE WHEN mv.Role = 1 THEN 'Owner' WHEN mv.Role = 0 THEN 'User' ELSE 'Unknown' END AS role,
    CASE WHEN acc.ApprenticeshipEmployerType = 1 THEN 'Levy' WHEN acc.ApprenticeshipEmployerType = 0 THEN 'NonLevy' ELSE 'Unknown' END AS apprenticeshipEmployerType
FROM employer_account.MembershipView mv
JOIN employer_account.Account acc ON mv.HashedAccountId = acc.HashedId
