namespace SFA.DAS.QFAST.UITests.Project.Helpers
{
    public class QfastDbSqlHelpers(ObjectContext objectContext, DbConfig dbConfig) : SqlDbHelper(objectContext, dbConfig.QfastDbConnectionString)
    {      
        //public async Task<int> DeleteApplications()
        //{
        //    var sql = @"
        //    DELETE dbo.ApplicationReviewFeedbacks
        //    DELETE dbo.ApplicationReviewFundings
        //    delete dbo.ApplicationQuestionAnswers
        //    delete dbo.ApplicationPages
        //    delete dbo.Messages
        //    DELETE dbo.ApplicationReviews
        //    DELETE dbo.Applications
        //    delete dbo.Routes";
        //    return await ExecuteSqlCommand(sql);            
        //}
        //public async Task<int> DeleteForms()
        //{
        //    var sql = @"
        //    DELETE FROM dbo.QuestionOptions;
        //    DELETE FROM dbo.QuestionValidations;
        //    DELETE FROM dbo.Routes;
        //    DELETE FROM dbo.Questions;
        //    DELETE FROM dbo.Pages;
        //    DELETE FROM dbo.Sections;
        //    DELETE FROM dbo.FormVersions;
        //    DELETE FROM dbo.Forms;";
        //    return await ExecuteSqlCommand(sql);
        //}
    }
}
