using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting  : Entity
    {
        public searchsetting()
        {
            this.details = new List<searchsettingdetail>();
            this.locations = new List<searchsetting_location>();
            //this.searchsetting_diet = new List<searchsetting_diet>();
            //this.searchsetting_drink = new List<searchsetting_drink>();
            //this.searchsetting_educationlevel = new List<searchsetting_educationlevel>();
            //this.searchsetting_employmentstatus = new List<searchsetting_employmentstatus>();
            //this.searchsetting_ethnicity = new List<searchsetting_ethnicity>();
            //this.searchsetting_exercise = new List<searchsetting_exercise>();
            //this.searchsetting_eyecolor = new List<searchsetting_eyecolor>();
            //this.searchsetting_gender = new List<searchsetting_gender>();
            //this.searchsetting_haircolor = new List<searchsetting_haircolor>();
            //this.searchsetting_havekids = new List<searchsetting_havekids>();
            //this.searchsetting_hobby = new List<searchsetting_hobby>();
            //this.searchsetting_hotfeature = new List<searchsetting_hotfeature>();
            //this.searchsetting_humor = new List<searchsetting_humor>();
            //this.searchsetting_incomelevel = new List<searchsetting_incomelevel>();
            //this.searchsetting_livingstituation = new List<searchsetting_livingstituation>();
            //this.searchsetting_location = new List<searchsetting_location>();
            //this.searchsetting_lookingfor = new List<searchsetting_lookingfor>();
            //this.searchsetting_maritalstatus = new List<searchsetting_maritalstatus>();
            //this.searchsetting_politicalview = new List<searchsetting_politicalview>();
            //this.searchsetting_profession = new List<searchsetting_profession>();
            //this.searchsetting_religion = new List<searchsetting_religion>();
            //this.searchsetting_religiousattendance = new List<searchsetting_religiousattendance>();
            //this.searchsetting_showme = new List<searchsetting_showme>();
            //this.searchsetting_sign = new List<searchsetting_sign>();
            //this.searchsetting_smokes = new List<searchsetting_smokes>();
            //this.searchsetting_sortbytype = new List<searchsetting_sortbytype>();
            //this.searchsetting_wantkids = new List<searchsetting_wantkids>();
        }

        public int id { get; set; }
        public int profile_id { get; set; }
        public Nullable<int> agemax { get; set; }
        public Nullable<int> agemin { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<int> distancefromme { get; set; }
        public Nullable<int> heightmax { get; set; }
        public Nullable<int> heightmin { get; set; }
        public Nullable<System.DateTime> lastupdatedate { get; set; }
        public Nullable<bool> myperfectmatch { get; set; }
        public Nullable<bool> savedsearch { get; set; }
        public string searchname { get; set; }
        public Nullable<int> searchrank { get; set; }
        public Nullable<bool> systemmatch { get; set; }


        public virtual profilemetadata profilemetadata { get; set; }       

        public virtual ICollection<searchsettingdetail> details { get; set; }
        public virtual ICollection<searchsetting_location> locations { get; set; }
        
        public string selectedcountryname { get; set; }
       
        public int? selectedcountryid { get; set; }
      
        public string selectedpostalcode { get; set; }
        //added 10/17/20011 so we can toggle postalcode box similar to register
       
        public bool? selectedpostalcodestatus { get; set; }
       
        public string selectedcity { get; set; }     
       
        public string selectedstateprovince { get; set; }
       
        //gps data added 10/17/2011       
        public double? selectedlongitude { get; set; }
       
        public double? selectedlatitude { get; set; }



    }



}
