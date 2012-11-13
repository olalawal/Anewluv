using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;



namespace Shell.MVC2.Interfaces
{
    public interface INotificationRepository
    {

        EmailMatchesViewModel getemailmatches(MembersViewModel model);
    
    }
}
