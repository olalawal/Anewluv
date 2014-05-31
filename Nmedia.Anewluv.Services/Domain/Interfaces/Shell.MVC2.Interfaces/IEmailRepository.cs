using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels ;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels.Email  ;
using System.Web;



namespace Shell.MVC2.Interfaces
{
    public interface IEmailRepository
    {

        EmailViewModel getemailmatches(MembersViewModel model);
    
    }
}
