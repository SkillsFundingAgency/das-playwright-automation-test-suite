using System;

namespace SFA.DAS.Approvals.UITests.Project.Events
{
    internal class LearningResumedEvent
    {

        public Guid LearningKey { get; set; }
        public int ApprenticeshipId { get; set; }
        public string Created { get; set; }
        public string ResumeDate { get; set; }

    }

}
