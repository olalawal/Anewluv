using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    public class searchsetting
    {
        [Key]
        public int id { get; set; }
        [DataMember]
        public int profile_id { get; set; }
       public virtual profilemetadata profilemetadata { get; set; }
         [DataMember]
        public int? agemax { get; set; }
         [DataMember]
        public int? agemin { get; set; }
         [DataMember]
        public DateTime? creationdate { get; set; }
         [DataMember]
        public int? distancefromme { get; set; }
         [DataMember]
        public int? heightmax { get; set; }
         [DataMember]
        public int? heightmin { get; set; }
         [DataMember]
        public DateTime? lastupdatedate { get; set; }
         [DataMember]
        public bool? myperfectmatch { get; set; }
         [DataMember]
        public bool? savedsearch { get; set; }
         [DataMember]
        public string searchname { get; set; }
         [DataMember]
        public int? searchrank { get; set; }
         [DataMember]
        //public int searchsettingsid { get; set; }
        // [DataMember]
        public bool? systemmatch { get; set; }
        public virtual ICollection<searchsetting_bodytype> bodytypes { get; set; }        
        public virtual ICollection<searchsetting_diet> diets { get; set; }        
        public virtual ICollection<searchsetting_drink>  drinks { get; set; }        
        public virtual ICollection<searchsetting_educationlevel>  educationlevels { get; set; }        
        public virtual ICollection<searchsetting_employmentstatus>  employmentstatus { get; set; }        
        public virtual ICollection<searchsetting_ethnicity>  ethnicitys { get; set; }        
        public virtual ICollection<searchsetting_exercise>  exercises { get; set; }        
        public virtual ICollection<searchsetting_eyecolor>  eyecolors { get; set; }        
        public virtual ICollection<searchsetting_gender>  genders { get; set; }        
        public virtual ICollection<searchsetting_haircolor>  haircolors { get; set; }
        public virtual ICollection<searchsetting_hotfeature> hotfeatures { get; set; }  
        public virtual ICollection<searchsetting_havekids>  havekids { get; set; }        
        public virtual ICollection<searchsetting_hobby>  hobbies { get; set; }        
        //public virtual ICollection<searchsetting_hotfeature>  hotfeature { get; set; }        
        public virtual ICollection<searchsetting_humor>  humors { get; set; }        
        public virtual ICollection<searchsetting_incomelevel>  incomelevels { get; set; }        
        public virtual ICollection<searchsetting_livingstituation>  livingstituations { get; set; }        
        public virtual ICollection<searchsetting_location>  locations { get; set; }        
        public virtual ICollection<searchsetting_lookingfor>  lookingfor { get; set; }        
        public virtual ICollection<searchsetting_maritalstatus>  maritalstatuses { get; set; }        // 
        //public virtual ICollection<SearchSettings_NigerianState> SearchSettings_NigerianState { get; set; }        
        public virtual ICollection<searchsetting_politicalview>  politicalviews { get; set; }        
        public virtual ICollection<searchsetting_profession>  professions { get; set; }        
        public virtual ICollection<searchsetting_religion>  religions { get; set; }        
        public virtual ICollection<searchsetting_religiousattendance>  religiousattendances { get; set; }
        public virtual ICollection<searchsetting_showme>  showme { get; set; }        
        public virtual ICollection<searchsetting_sign>  signs { get; set; }        
        public virtual ICollection<searchsetting_smokes>  smokes { get; set; }        
        public virtual ICollection<searchsetting_sortbytype>  sortbytypes { get; set; } 
        public virtual ICollection<searchsetting_wantkids>  wantkids { get; set; }
     
    }
}
