using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Helpers.DataHelpers
{
    internal enum ApprenticeshipStatus
    {
        Live,
        WaitingToStart,
        Paused,
        Stopped,
        Completed
    }

    internal enum FundingType
    {
        DirectTransferFundsFromConnection,
        ReservedFunds,
        ReserveNewFunds,
        CurrentLevyFunds,
        TransferFunds
    }

    internal enum EmployerType
    {
        Levy,
        NonLevy,
        NonLevyUserAtMaxReservationLimit
    }

}
