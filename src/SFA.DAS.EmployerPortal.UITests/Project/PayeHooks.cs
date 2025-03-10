using SFA.DAS.EmployerPortal.UITests.Project.Helpers;

namespace SFA.DAS.EmployerPortal.UITests.Project;

[Binding]
public class PayeHooks(ScenarioContext context)
{
    private enum FundType { NonLevyFund, LevyFund, TransferFund }

    private readonly ObjectContext _objectContext = context.Get<ObjectContext>();
    private readonly TryCatchExceptionHelper _tryCatch = context.Get<TryCatchExceptionHelper>();
    private LoginCredentialsHelper _loginCredentialsHelper;
    private bool _isAddPayeDetails;

    [BeforeScenario(Order = 23)]
    [Scope(Tag = "addnonlevyfunds")]
    public async Task AddNonLevyFunds() => await AddPayeDetails(FundType.NonLevyFund);

    [BeforeScenario(Order = 24)]
    [Scope(Tag = "addtransferslevyfunds")]
    public async Task AddTransfersLevyFunds() => await AddPayeDetails(FundType.TransferFund);

    [BeforeScenario(Order = 25)]
    [Scope(Tag = "addlevyfunds")]
    public async Task AddLevyFunds() => await AddPayeDetails(FundType.LevyFund);

    [BeforeScenario(Order = 26)]
    [Scope(Tag = "addanothernonlevypayedetails")]
    public async Task SetUpAnotherNonLevyPayeDetails() => await AddAnotherPayeDetails(FundType.NonLevyFund, 1);

    [BeforeScenario(Order = 27)]
    [Scope(Tag = "addsecondlevyfunds")]
    public async Task SetUpSecondLevyPayeDetails() => await AddAnotherPayeDetails(FundType.LevyFund, 1);

    [BeforeScenario(Order = 27)]
    [Scope(Tag = "addthirdlevyfunds")]
    public async Task SetUpThirdLevyPayeDetails() => await AddAnotherPayeDetails(FundType.LevyFund, 2);

    [BeforeScenario(Order = 28)]
    [Scope(Tag = "addmultiplelevyfunds")]
    public async Task SetUpMultipleLevyPayeDetails()
    {
        int noOfPayeToAdd = int.Parse(context.GetUser<AddMultiplePayeLevyUser>().NoOfPayeToAdd);

        for (int i = 1; i < noOfPayeToAdd; i++) await AddAnotherPayeDetails(FundType.LevyFund, i);
    }

    private async Task AddPayeDetails(FundType fundType)
    {
        _isAddPayeDetails = true;

        var mongoDbDataGenerator = new MongoDbDataGenerator(context, string.Empty);

        await mongoDbDataGenerator.AddGatewayUsers(0);

        var employerPortalDatahelpers = context.Get<EmployerPortalDataHelper>();

        _loginCredentialsHelper = context.Get<LoginCredentialsHelper>();

        _loginCredentialsHelper.SetLoginCredentials(employerPortalDatahelpers.RandomEmail, string.Empty, employerPortalDatahelpers.CompanyTypeOrg);

        await AddFunds(mongoDbDataGenerator, fundType);
    }

    private async Task AddAnotherPayeDetails(FundType fundType, int index)
    {
        _objectContext.SetDataHelper(new EmployerUserNameDataHelper(context.ScenarioInfo.Tags));

        var anotherMongoDbDataGenerator = new MongoDbDataGenerator(context, string.Empty);

        await anotherMongoDbDataGenerator.AddGatewayUsers(index);

        await AddFunds(anotherMongoDbDataGenerator, fundType);
    }

    private async Task AddFunds(MongoDbDataGenerator mongoDbDataGenerator, FundType fundType)
    {
        if (fundType == FundType.NonLevyFund || context.ScenarioInfo.Tags.Contains("adddynamicfunds")) { return; }

        var (fraction, calculatedAt, levyDeclarations) = fundType == FundType.TransferFund ? LevyDeclarationDataHelper.TransferslevyFunds() : LevyDeclarationDataHelper.LevyFunds();

        await mongoDbDataGenerator.AddLevyDeclarations(fraction, calculatedAt, levyDeclarations);

        _loginCredentialsHelper.SetIsLevy();
    }

    [AfterScenario(Order = 21)]
    public async Task DeletePayeDetails()
    {
        if (!_isAddPayeDetails) { return; }

        await _tryCatch.AfterScenarioException(async () =>
        {
            var empRefs = _objectContext.GetMongoDbDataHelpers().Select(x => x.EmpRef).ToList();

            foreach (var empRef in empRefs)
            {
                if (context.TryGetValue($"{typeof(DeclarationsDataGenerator).FullName}_{empRef}", out MongoDbHelper levyDecMongoDbHelper))
                {
                    await levyDecMongoDbHelper.AsyncDeleteData();
                    SetDebugInformation($"Declarations Deleted for, EmpRef: {empRef}");

                    if (context.TryGetValue($"{typeof(EnglishFractionDataGenerator).FullName}_{empRef}", out MongoDbHelper englishFractionMongoDbHelper))
                    {
                        await englishFractionMongoDbHelper.AsyncDeleteData();
                        SetDebugInformation($"English Fraction Deleted for, EmpRef: {empRef}");
                    }
                }

                if (context.TryGetValue($"{typeof(EmpRefLinksDataGenerator).FullName}_{empRef}", out MongoDbHelper emprefMongoDbHelper))
                {
                    await emprefMongoDbHelper.AsyncDeleteData();
                    SetDebugInformation($"EmpRef Links Deleted, EmpRef: {empRef}");
                }

                if (context.TryGetValue($"{typeof(GatewayUserDataGenerator).FullName}_{empRef}", out MongoDbHelper gatewayusermongoDbHelper))
                {
                    await gatewayusermongoDbHelper.AsyncDeleteData();
                    SetDebugInformation($"Gateway User Deleted, EmpRef: {empRef}");
                }
            }

            SetDebugInformation($"deleted '{empRefs.Count}' emprefs from Mongo db");
        });
    }

    private void SetDebugInformation(string x) => _objectContext.SetDebugInformation(x);
}
