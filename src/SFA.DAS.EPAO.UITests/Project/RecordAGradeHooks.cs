﻿namespace SFA.DAS.EPAO.UITests.Project;

[Binding, Scope(Tag = "recordagrade")]
public class RecordAGradeHooks
{
    private readonly ScenarioContext _context;
    private readonly ObjectContext _objectContext;
    private readonly TryCatchExceptionHelper _tryCatch;
    private readonly string[] _tags;

    public RecordAGradeHooks(ScenarioContext context)
    {
        _context = context;
        _tags = _context.ScenarioInfo.Tags;
        _objectContext = context.Get<ObjectContext>();
        _tryCatch = context.Get<TryCatchExceptionHelper>();
    }

    [BeforeScenario(Order = 35)]
    public void SetUpLearnerCriteria()
    {
        switch (true)
        {
            case bool _ when _tags.Contains("epaoca1standard1version0option"): SetLearnerCriteria(true, false, false, false, true, false); break;
            case bool _ when _tags.Contains("epaoca1standard1version1option"): SetLearnerCriteria(true, false, true, false, true, false); break;
            case bool _ when _tags.Contains("epaoca1standard2version0option"): SetLearnerCriteria(true, true, false, false, false, false); break;
            case bool _ when _tags.Contains("epaoca1standard2version1option"): SetLearnerCriteria(true, true, true, false, false, false); break;
            case bool _ when _tags.Contains("epaoca2standard1version0option"): SetLearnerCriteria(true, false, false, true, true, false); break;
            case bool _ when _tags.Contains("epaoca2standard1version1option"): SetLearnerCriteria(true, false, true, true, true, false); break;
            case bool _ when _tags.Contains("epaoca2standard2version0option"): SetLearnerCriteria(true, true, false, true, false, false); break;
            case bool _ when _tags.Contains("epaoca2standard2version1option"): SetLearnerCriteria(true, true, true, true, false, false); break;
            case bool _ when _tags.Contains("epaoca1standard2version1versionconfirmed"): SetLearnerCriteria(true, true, false, false, true, false); break;
            default: SetLearnerCriteria(true, false, false, false, true, false); break;
        }
        ;
    }

    [AfterScenario(Order = 35)]
    public async Task Recordagrade() => await _tryCatch.AfterScenarioException(async () => await _context.Get<EPAOAdminCASqlDataHelper>().DeleteCertificate(_objectContext.GetLearnerULN(), _objectContext.GetLearnerStandardCode()));

    private void SetLearnerCriteria(bool isActiveStandard, bool hasMultipleVersions, bool withOptions, bool hasMultiStandards, bool versionConfirmed, bool optionSet)
    {
        var learnerDetails = new LearnerCriteria(isActiveStandard, hasMultipleVersions, withOptions, hasMultiStandards, versionConfirmed, optionSet);

        _context.Set(learnerDetails);

        _objectContext.SetLearnerCriteria(isActiveStandard, hasMultipleVersions, withOptions, hasMultiStandards);
    }

}
