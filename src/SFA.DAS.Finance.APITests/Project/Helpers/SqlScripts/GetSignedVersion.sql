
  SELECT MIN(ISNULL(SignedAgreementVersion, 0)) as minimumSignedAgreementVersion
    FROM [employer_account].[AccountLegalEntity] WHERE AccountId = '<AccountId>'
