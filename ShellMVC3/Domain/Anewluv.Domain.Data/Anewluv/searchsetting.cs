using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class searchsetting
    {
        public searchsetting()
        {
            this.searchsetting_bodytype = new List<searchsetting_bodytype>();
            this.searchsetting_diet = new List<searchsetting_diet>();
            this.searchsetting_drink = new List<searchsetting_drink>();
            this.searchsetting_educationlevel = new List<searchsetting_educationlevel>();
            this.searchsetting_employmentstatus = new List<searchsetting_employmentstatus>();
            this.searchsetting_ethnicity = new List<searchsetting_ethnicity>();
            this.searchsetting_exercise = new List<searchsetting_exercise>();
            this.searchsetting_eyecolor = new List<searchsetting_eyecolor>();
            this.searchsetting_gender = new List<searchsetting_gender>();
            this.searchsetting_haircolor = new List<searchsetting_haircolor>();
            this.searchsetting_havekids = new List<searchsetting_havekids>();
            this.searchsetting_hobby = new List<searchsetting_hobby>();
            this.searchsetting_hotfeature = new List<searchsetting_hotfeature>();
            this.searchsetting_humor = new List<searchsetting_humor>();
            this.searchsetting_incomelevel = new List<searchsetting_incomelevel>();
            this.searchsetting_livingstituation = new List<searchsetting_livingstituation>();
            this.searchsetting_location = new List<searchsetting_location>();
            this.searchsetting_lookingfor = new List<searchsetting_lookingfor>();
            this.searchsetting_maritalstatus = new List<searchsetting_maritalstatus>();
            this.searchsetting_politicalview = new List<searchsetting_politicalview>();
            this.searchsetting_profession = new List<searchsetting_profession>();
            this.searchsetting_religion = new List<searchsetting_religion>();
            this.searchsetting_religiousattendance = new List<searchsetting_religiousattendance>();
            this.searchsetting_showme = new List<searchsetting_showme>();
            this.searchsetting_sign = new List<searchsetting_sign>();
            this.searchsetting_smokes = new List<searchsetting_smokes>();
            this.searchsetting_sortbytype = new List<searchsetting_sortbytype>();
            this.searchsetting_wantkids = new List<searchsetting_wantkids>();
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
        public virtual ICollection<searchsetting_bodytype> searchsetting_bodytype { get; set; }
        public virtual ICollection<searchsetting_diet> searchsetting_diet { get; set; }
        public virtual ICollection<searchsetting_drink> searchsetting_drink { get; set; }
        public virtual ICollection<searchsetting_educationlevel> searchsetting_educationlevel { get; set; }
        public virtual ICollection<searchsetting_employmentstatus> searchsetting_employmentstatus { get; set; }
        public virtual ICollection<searchsetting_ethnicity> searchsetting_ethnicity { get; set; }
        public virtual ICollection<searchsetting_exercise> searchsetting_exercise { get; set; }
        public virtual ICollection<searchsetting_eyecolor> searchsetting_eyecolor { get; set; }
        public virtual ICollection<searchsetting_gender> searchsetting_gender { get; set; }
        public virtual ICollection<searchsetting_haircolor> searchsetting_haircolor { get; set; }
        public virtual ICollection<searchsetting_havekids> searchsetting_havekids { get; set; }
        public virtual ICollection<searchsetting_hobby> searchsetting_hobby { get; set; }
        public virtual ICollection<searchsetting_hotfeature> searchsetting_hotfeature { get; set; }
        public virtual ICollection<searchsetting_humor> searchsetting_humor { get; set; }
        public virtual ICollection<searchsetting_incomelevel> searchsetting_incomelevel { get; set; }
        public virtual ICollection<searchsetting_livingstituation> searchsetting_livingstituation { get; set; }
        public virtual ICollection<searchsetting_location> searchsetting_location { get; set; }
        public virtual ICollection<searchsetting_lookingfor> searchsetting_lookingfor { get; set; }
        public virtual ICollection<searchsetting_maritalstatus> searchsetting_maritalstatus { get; set; }
        public virtual ICollection<searchsetting_politicalview> searchsetting_politicalview { get; set; }
        public virtual ICollection<searchsetting_profession> searchsetting_profession { get; set; }
        public virtual ICollection<searchsetting_religion> searchsetting_religion { get; set; }
        public virtual ICollection<searchsetting_religiousattendance> searchsetting_religiousattendance { get; set; }
        public virtual ICollection<searchsetting_showme> searchsetting_showme { get; set; }
        public virtual ICollection<searchsetting_sign> searchsetting_sign { get; set; }
        public virtual ICollection<searchsetting_smokes> searchsetting_smokes { get; set; }
        public virtual ICollection<searchsetting_sortbytype> searchsetting_sortbytype { get; set; }
        public virtual ICollection<searchsetting_wantkids> searchsetting_wantkids { get; set; }
    }
}
