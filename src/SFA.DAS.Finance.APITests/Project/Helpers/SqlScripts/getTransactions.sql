    SELECT
        YEAR(DateCreated) AS year,
        MONTH(DateCreated) AS month,
        SUM(Amount) AS amount
        -- CONCAT('/api/Accounts/{hasAccId}/transactions/', YEAR(DateCreated), '/', MONTH(DateCreated)) AS href
    FROM employer_financial.TransactionLine
    WHERE AccountId = '<AccountId>'
    GROUP BY YEAR(DateCreated), MONTH(DateCreated)
    ORDER BY year, month;