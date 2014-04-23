using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;



//for RIA services contrib
//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;

namespace Shell.MVC2.Models
{
  [DataContract] public class AdminPhotoModel
    {

       

     [DataMember]       public Guid PhotoID { get; set; }
        [DataMember]  public string ProfileID { get; set; }
        [DataMember]  public string Aproved { get; set; }
        [DataMember] public bool checkedAproved { get; set; }
       [DataMember]     public List<string> AprovedList { get; set; }

        [DataMember]  public string ProfileImageType { get; set; }
       [DataMember]    public DateTime? PhotoDate { get; set; }
       [DataMember]    public DateTime? PhotoReviewDate { get; set; }
        [DataMember]  public string PhotoReviewerID { get; set; }
        [DataMember]  public string photoreviewID { get; set; }
        [DataMember]  public int PhotoRejectionReasonID { get; set; }

       [DataMember]     public List<string> PhotoRejectionReasonList { get; set; }


        //public bool Aproved(string Approved)
        //{
            
        //    bool check = false;

        //    if (Approved == "Yes")
        //    {
        //        check = true;
        //    }

        //    return check;
        //}

    }

}