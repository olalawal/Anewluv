using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    //TO DO drepreciate this code in Account controller :  public virtual ActionResult ActivateProfile(string ProfileID, string ActivationCode, bool? PhotoStatus)
       
    //defines a container for agreagating models
    //[Serializable]
    [DataContract]
    public class activateprofilecontainerviewmodel
    {
        [DataMember]        
        public activateprofilemodel activateprofilemodel { get; set; }
        //[DataMember]
        //public registermodel registermodel { get; set; }
        [DataMember]
        public List<PhotoUploadModel>  activateprofilephotos { get; set; }
    }

   


 
}



   