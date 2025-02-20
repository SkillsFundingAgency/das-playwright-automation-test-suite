﻿using SFA.DAS.MongoDb.DataGenerator;
using SFA.DAS.MongoDb.DataGenerator.Helpers;
using SFA.DAS.Registration.UITests.Project.Helpers;
using System;

namespace SFA.DAS.Registration.UITests.Project.Steps;

[Binding]
public class BackgroundDataSteps
{
    private readonly ScenarioContext _context;
    private readonly MongoDbDataGenerator _levyDeclarationHelper;
    private readonly LoginCredentialsHelper _loginCredentialsHelper;

    public BackgroundDataSteps(ScenarioContext context)
    {
        _context = context;
        _loginCredentialsHelper = context.Get<LoginCredentialsHelper>();
        _levyDeclarationHelper = new MongoDbDataGenerator(_context, string.Empty);
    }

    [Given(@"the following levy declarations with english fraction of (.*) calculated at (.*)")]
    public async Task GivenTheFollowingLevyDeclarationsWithEnglishFractionOfCalculatedAt(decimal fraction, DateTime calculatedAt, Table table)
    {
        await _levyDeclarationHelper.AddLevyDeclarations(fraction, calculatedAt, table);

        _loginCredentialsHelper.SetIsLevy();
    }

    [Given(@"levy declarations are added for the past (.*) months with levypermonth as (.*)")]
    public async Task GivenLevyDeclarationsIsAddedForPastMonthsWithLevypermonthAs(string duration, string levyPerMonth)
    {
        //If you are using this step please add @adddynamicfunds tag.
        var (fraction, calculatedAt, levyDeclarations) = LevyDeclarationDataHelper.LevyFunds(duration, levyPerMonth);

        await _levyDeclarationHelper.AddLevyDeclarations(fraction, calculatedAt, levyDeclarations);

        _loginCredentialsHelper.SetIsLevy();
    }
}
