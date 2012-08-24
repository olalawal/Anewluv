using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Shell.MVC2.Domain.Entities;



//for RIA services contrib
//using RiaServicesContrib.Mvc;
//using RiaServicesContrib.Mvc.Services;

namespace Shell.MVC2.Models
{
    public class AdminPhotoModel
    {

       

        public Guid PhotoID { get; set; }
        public string ProfileID { get; set; }
        public string Aproved { get; set; }
        public bool checkedAproved { get; set; }
        public List<string> AprovedList { get; set; }

        public string ProfileImageType { get; set; }
        public DateTime? PhotoDate { get; set; }
        public DateTime? PhotoReviewDate { get; set; }
        public string PhotoReviewerID { get; set; }
        public string PhotoReviewStatusID { get; set; }
        public int PhotoRejectionReasonID { get; set; }

        public List<string> PhotoRejectionReasonList { get; set; }

        public bool BoolAproved(string Approved)
        {
            bool check = false;

            if (Approved == "Yes")
            {
                check = true;
            }

            return check;
        }

    }

}