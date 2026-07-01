using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SFA.DAS.Approvals.UITests.Project.Events
{
    internal class LearningWithdrawnEvent
    {
        public Guid LearningKey { get; set; }
        public int ApprenticeshipId { get; set; }
        public string Created { get; set; }
        public string WithdrawalDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? withdrawalReasonCode { get; set; }
    }
}
