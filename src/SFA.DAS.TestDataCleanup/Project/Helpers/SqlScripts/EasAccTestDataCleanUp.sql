﻿select id into #userids from employer_account.[User] where email in (select email from #emails);
select Accountid id into #accountids from employer_account.Membership where UserId in (select id from #userids);
select id into #accountlegalentityids from employer_account.AccountLegalEntity where AccountId in (select id from #accountids);

PRINT 'delete from Invitation' 
delete from employer_account.Invitation where AccountId in (select id from #accountids);
PRINT 'delete from UserAornFailedAttempts'
delete from employer_account.UserAornFailedAttempts where UserId in (select id from #userids);
PRINT 'delete from AccountHistoryNonUnique'
delete from employer_account.AccountHistoryNonUnique where AccountId in (select id from #accountids);
PRINT 'delete from EmployerAgreement_Backup'
delete from employer_account.EmployerAgreement_Backup where AccountId in (select id from #accountids);
PRINT 'delete from EmployerAgreement'
delete from employer_account.EmployerAgreement where AccountLegalEntityId in (select id from #accountlegalentityids) or SignedById in (select id from #userids);
PRINT 'delete from AccountEmployerAgreement'
delete from employer_account.AccountEmployerAgreement where AccountId in (select id from #accountids);
PRINT 'delete from AccountHistory'
delete from employer_account.AccountHistory where AccountId in (select id from #accountids);
PRINT 'delete from Paye'
delete from employer_account.Paye where Ref in ( select EmpRef from employer_account.GetAccountPayeSchemes where AccountId in (select id from #accountids));
PRINT 'delete from UserAccountSettings'
delete from employer_account.UserAccountSettings where AccountId in (select id from #accountids) or UserId in (select id from #userids);
PRINT 'delete from AccountLegalEntity'
delete from employer_account.AccountLegalEntity where AccountId in (select id from #accountids);
PRINT 'delete from Membership'
delete from employer_account.Membership where UserId in (select id from #userids);
PRINT 'delete from Account'
delete from employer_account.Account where id in (select id from #accountids);
PRINT 'delete from User'
delete from employer_account.[User] where id in (select id from #userids);