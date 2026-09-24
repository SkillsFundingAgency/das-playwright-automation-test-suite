using System;

namespace SFA.DAS.Approvals.UITests.Project.Events
{
    internal class LearningPausedEvent
    {

        public Guid LearningKey { get; set; }
        public int ApprenticeshipId { get; set; }
        public string Created { get; set; }
        public string PauseDate { get; set; }

    }

}
