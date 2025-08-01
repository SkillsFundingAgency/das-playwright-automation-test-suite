using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers.DataHelpers.ApprenticeshipModel
{
    internal interface IApprenticeFactory
    {
        Task<Apprentice> CreateApprenticeAsync();

    }
}
