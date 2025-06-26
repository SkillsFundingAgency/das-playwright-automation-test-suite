using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Project.Helpers
{
    internal class ApprovalsEmailsHelper
    {
    }

    public static class EmailTemplateHelper
    {
        internal static EmailTemplate GetEmailTemplate(string emailType, string recipient)
        {
            return emailType switch
            {
                "CohortApproved" => new EmailTemplate
                {
                    Subject = "Your Cohort Has Been Approved!",
                    Body = $"Hello {recipient},\n\nYour cohort has been successfully approved and is now ready for processing.\n\nBest regards,\nTeam",
                    Recipients = new List<string> { recipient }
                },
                "CohortReadyForReview" => new EmailTemplate
                {
                    Subject = "Cohort Ready for Review",
                    Body = $"Hello {recipient},\n\nYour cohort is now ready for review. Please log in to the portal to proceed.\n\nThanks,\nTeam",
                    Recipients = new List<string> { recipient }
                },
                "CohortRejected" => new EmailTemplate
                {
                    Subject = "Cohort Rejected",
                    Body = $"Hello {recipient},\n\nUnfortunately, your cohort has been rejected. Please review the feedback and resubmit.\n\nBest regards,\nTeam",
                    Recipients = new List<string> { recipient }
                },
                "CohortAwaitingApproval" => new EmailTemplate
                {
                    Subject = "Cohort Awaiting Your Approval",
                    Body = $"Hello {recipient},\n\nA cohort is awaiting your approval. Please take action at your earliest convenience.\n\nBest,\nTeam",
                    Recipients = new List<string> { recipient }
                },
                _ => new EmailTemplate
                {
                    Subject = "Unknown Email Type",
                    Body = $"Hello {recipient},\n\nThis email type is not recognized.\n\nRegards,\nTeam",
                    Recipients = new List<string> { recipient }
                }
            };

        }
    }

    internal class EmailTemplate
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Recipients { get; set; }
    }

}
