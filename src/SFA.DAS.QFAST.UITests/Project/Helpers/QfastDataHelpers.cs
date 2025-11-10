
namespace SFA.DAS.QFAST.UITests.Project.Helpers;

public class QfastDataHelpers
{
    public QfastDataHelpers()
    {
        FormName = "Test From";
        FormDescription = "This is a Test Form";
        DraftStatus = "Draft";
        SectionName = "Test Section";
        PageName = "Test Page";
        FirstQuestionTitle = "Awarding organisation";
        SecondQuestionTitle = "Qualification title";
        ThirdQuestionTitle = "Phone Number";
        FourthQuestionTitle = "Date";
        FifthQuestionTitle = "Single Option";
        SixthQuestionTitle = "Multiple Option";
        SeventhQuestionTitle = "Attach documents";
    }

    public string FormName { get; }
    public string FormDescription { get; }
    public string DraftStatus { get; }
    public string SectionName { get; }
    public string PageName { get; }
    public string FirstQuestionTitle { get; } 
    public string SecondQuestionTitle { get; }
    public string ThirdQuestionTitle { get; }
    public string FourthQuestionTitle { get; }
    public string FifthQuestionTitle { get; }
    public string SixthQuestionTitle { get; }
    public string SeventhQuestionTitle { get; }
}
