select email into #apprenticeemails from dbo.Registration where commitmentsapprenticeshipid in (select id from #commitmentsapprenticeshipid)
select apprenticeid into #apprenticeid from dbo.Registration where commitmentsapprenticeshipid in (select id from #commitmentsapprenticeshipid) 

PRINT 'delete from Revision'; 
Delete from dbo.Revision where commitmentsapprenticeshipid in (select id from #commitmentsapprenticeshipid); 
PRINT 'delete from ApprenticeshipMatchAttempt'; 
Delete from dbo.ApprenticeshipMatchAttempt where Apprenticeid in (select apprenticeid from #apprenticeid); 
PRINT 'delete from Apprenticeship'; 
Delete from dbo.Apprenticeship where Apprenticeid in (select apprenticeid from #apprenticeid); 
PRINT 'delete from Registration'; 
Delete from dbo.Registration where commitmentsapprenticeshipid in (select id from #commitmentsapprenticeshipid);
