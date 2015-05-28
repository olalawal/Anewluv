using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Anewluv.Domain.Data;
using System.Runtime.Serialization;
using Nmedia.Infrastructure.DTOs;

namespace Anewluv.Domain.Data.ViewModels

{
    [DataContract]
    public class BasicSettingsModel
    {

       
        // [DataMember]
        //public string searchname { get; set; }
        // [DataMember]
        //public int searchrank { get; set; }

        //My Settings 
        [DataMember]
        public DateTime? birthdate { get; set; }
        [DataMember]
        public lu_gender gender { get; set; }
        [DataMember]
        public int? countryid { get; set; }
        [DataMember]
        public string stateprovince { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string postalcode { get; set; }
        [DataMember]
        public string aboutme { get; set; }
        [DataMember]
        public string catchyintroline { get; set; }   
        [DataMember]
        public string phonenumber { get; set; }


        ////Match settings i.e search
        //[DataMember]
        //[Range(0, 5000, ErrorMessage = "DistanceFrom must be a postive number no greater than 5000")]
        //public int distancefromme { get; set; }
        //[DataMember]
        //public int agemax { get; set; }
        //[DataMember]
        //public int agemin { get; set; }             

        ////add the propeties for bound litsts here
        // [DataMember]
        // public IList<lu_showme> showmelist = new List<lu_showme>();
        // [DataMember]
        // public IList<lu_sortbytype> sortbytypelist = new List<lu_sortbytype>();
        ////gender is now allowing multiple selections
        // [DataMember]
        // public IList<lu_gender> genderslist = new List<lu_gender>();

        //// public SelectList Genders { get; set; }
        //9-16-2011 need to KILL the parametered contructor for the postbacks not to be messed up
        //MVC viewstate needs a paramerless contrsctuor ,  see 

    }
}
