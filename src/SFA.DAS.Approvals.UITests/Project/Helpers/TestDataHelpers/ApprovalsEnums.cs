namespace SFA.DAS.Approvals.UITests.Project.Helpers.TestDataHelpers;

internal enum ApprenticeshipStatus
{
    Live,
    WaitingToStart,
    Paused,
    Stopped,
    Completed
}

internal enum PaymentStatus
{
    Active,    
    Paused
}

internal enum FundingType
{
    DirectTransferFundsFromConnection,
    ReservedFunds,
    ReserveNewFunds,
    CurrentLevyFunds,
    TransferFunds
}

public enum EmployerType
{
    Levy,
    NonLevy,
    NonLevyUserAtMaxReservationLimit
}

public enum ApprenticeRequests
{
    ReadyForReview,
    WithEmployers,
    WithTrainingProviders,
    Drafts,
    WithTransferSendingEmployers
}

public enum ProviderUserRoles
{
    Contributor,
    ContributorWithApproval,
    AccountOwner,
    Viewer
}

internal enum LearningType
{
    Apprenticeship = 0,
    FoundationApprenticeship = 1,
    ShortCourses = 2
}

internal enum RequestResponse 
{
    OK = 200,
    Created = 201,
    BadRequest = 400,
    NotFound = 404,
    InternalServerError = 500
}
