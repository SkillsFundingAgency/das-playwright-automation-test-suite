using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.Approvals.UITests.Helpers.DataHelpers
{
    public class Apprenticeship
    {
        public Apprenticeship()
        {
            EmployerDetails = new Employer();
            ApprenticeDetails =new Apprentice();
            TrainingDetails = new Training();
            RPLDetails = new RPL();
        }

        public Apprenticeship(Apprentice apprentice, Training training, RPL rpl)
        {
            ApprenticeDetails = apprentice;
            TrainingDetails = training;
            RPLDetails = rpl;
        }

        public int UKPRN { get; set; }       
        public string ReservationID { get; set; }
        public string CohortReference { get; set; }  
        public Employer EmployerDetails { get; set; }
        public Apprentice ApprenticeDetails { get; set; }
        public Training TrainingDetails { get; set; }
        public RPL RPLDetails { get; set; }

    }

    public class Employer
    {
        public string AgreementId { get; set; }
        public string EmployerName { get; set; }
    }


    public class Apprentice
    {
        public string ULN { get; set; }                 
        public string FirstName { get; set; }           
        public string LastName { get; set; }            
        public string Email { get; set; }        
        public DateTime DateOfBirth { get; set; }         

    }

    public class Training
    {
        public DateTime StartDate { get; set; }           
        public DateTime EndDate { get; set; }             
        public int PercentageLearningToBeDelivered { get; set; }             
        public int EpaoPrice { get; set; }          
        public int TrainingPrice { get; set; }             
        public int TotalPrice { get; set; }
        public bool IsFlexiJob { get; set; }                        
        public int PlannedOTJTrainingHours { get; set; }            
        public int StandardCode { get; set; }                      
        public string ConsumerReference { get; set; }              
    }

    public class RPL
    {
        public string RecognisePriorLearning { get; set; }
        public int TrainingTotalHours { get; set; }
        public int TrainingHoursReduction { get; set; }
        public bool IsDurationReducedByRPL { get; set; }
        public int DurationReducedBy { get; set; }
        public int PriceReducedBy { get; set; }
    }


}
