using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class searchsetting
    {
        [Key]
        public int id { get; set; }
       public int profile_id { get; set; }
       public virtual profilemetadata profilemetadata { get; set; }   
        public int? agemax { get; set; }
        public int? agemin { get; set; }
        public DateTime? creationdate { get; set; }
        public int? distancefromme { get; set; }
        public int? heightmax { get; set; }
        public int? heightmin { get; set; }
        public DateTime? lastupdatedate { get; set; }
        public bool? myperfectmatch { get; set; } 
        public bool? savedsearch { get; set; }
        public string searchname { get; set; }
        public int? searchrank { get; set; }
        public int searchsettingsid { get; set; }
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
