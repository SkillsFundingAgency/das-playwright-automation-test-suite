﻿select id into #accountLegalIds from dbo.AccountLegalEntities where AccountId in (select id from #accountids);  
select id into #accountProviderids from dbo.AccountProviders where AccountId in (select id from #accountids);  
select id into #accountProviderLegalids from dbo.AccountProviderLegalEntities where AccountLegalEntityId in (select id from #accountLegalIds) and AccountProviderId in (select id from #accountProviderids); 
PRINT 'delete from AddedAccountProviderEventAudits';
delete from dbo.AddedAccountProviderEventAudits where AccountId in (select id from #accountids); 
PRINT 'delete from UpdatedPermissionsEventAudits';
delete from dbo.UpdatedPermissionsEventAudits where AccountId in (select id from #accountids); 
PRINT 'delete from DeletedPermissionsEventAudits';
delete from dbo.DeletedPermissionsEventAudits where AccountProviderLegalEntityId in (select id from #accountProviderLegalids); 
PRINT 'delete from AccountLegalEntityProvider';
delete from dbo.AccountLegalEntityProvider where AccountLegalEntityId in (select id from #accountLegalIds); 
PRINT 'delete from Permissions';
delete from dbo.[Permissions] WHERE AccountProviderLegalEntityId in (select id from #accountProviderLegalids); 
PRINT 'delete from AccountProviderLegalEntities';
delete from dbo.AccountProviderLegalEntities where AccountProviderId in (select id from #accountProviderids); 
PRINT 'delete from AccountProviders';
delete from dbo.AccountProviders where AccountId in (select id from #accountids); 
PRINT 'delete from AccountLegalEntities';
delete from dbo.AccountLegalEntities where AccountId in (select id from #accountids); 
PRINT 'delete from Accounts';
delete from dbo.Accounts where id in (select id from #accountids);