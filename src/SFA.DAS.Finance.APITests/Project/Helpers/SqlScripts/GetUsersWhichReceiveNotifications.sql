SELECT mv.UserRef as userRef,
    mv.FirstName as firstName, 
    mv.LastName as lastName,
    mv.Email as email,
    CASE 
        WHEN mv.Role = 1 THEN 'Owner'
        WHEN mv.Role = 0 THEN 'User'
    ELSE 'Unknown'
    END AS role,
    CASE 
        WHEN uas.ReceiveNotifications = 1 THEN 'true'
        WHEN uas.ReceiveNotifications = 0 THEN 'false'
    ELSE 'Unknown'
    END AS canReceiveNotifications
FROM employer_account.MembershipView mv
LEFT JOIN employer_account.UserAccountSettings uas 
    ON mv.AccountId = uas.AccountId
WHERE mv.AccountId = '<AccountId>' AND mv.role = 1 AND mv.UserId = uas.UserId
ORDER BY mv.UserRef;
