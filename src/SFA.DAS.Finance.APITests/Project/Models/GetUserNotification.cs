namespace SFA.DAS.Finance.APITests.Project.Models
{
    public class GetUserNotification
    {
        public string userRef { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string canReceiveNotifications { get; set; }
        public string status { get; set; }
    }
}
