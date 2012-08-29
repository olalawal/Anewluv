using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    //TO DO drepreciate this code in Account controller :  public virtual ActionResult ActivateProfile(string ProfileID, string ActivationCode, bool? PhotoStatus)
       
    //defines a container for agreagating models
    [Serializable]
    public class ActivateProfileContainerViewModel
    {
                   
        public ActivateProfileModel ActivateProfileModel { get; set; }
        public PhotoViewModel  ActivateProfilePhotos { get; set; }
    }

   


 
}



   