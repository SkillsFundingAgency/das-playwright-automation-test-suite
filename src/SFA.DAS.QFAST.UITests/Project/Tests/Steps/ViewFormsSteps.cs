using SFA.DAS.QFAST.UITests.Project.Tests.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.QFAST.UITests.Project.Tests.Steps
{
    public class ViewFormsSteps(ScenarioContext context)
    {
        private readonly ViewForms_Page _viewFormsPage = new(context);
        private readonly Admin_Page _adminPage = new(context);   

    }
}
