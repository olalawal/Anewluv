using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class profiledata
    {
            // Metadata classes are not meant to be instantiated.
            [Key]
            public string id { get; set; }   
        
            public int? age { get; set; }
            public DateTime birthdate { get; set; }          
            public string city { get; set; }
            public string countryregion { get; set; }
            public string stateprovince { get; set; }
            public int countryid { get; set; }
            public Nullable<double> longitude { get; set; }
            public Nullable<double> latitude { get; set; }
            public string aboutme { get; set; }
            public long height { get; set; }
            public string mycatchyintroLine { get; set; }
            public string phone { get; set; }
            public string postalcode { get; set; }
            public string profile_id { get; set; }
            public virtual profile profile { get; set; }


        //lookups for personal profile details 
            public virtual lu_gender gender { get; set; }
            public virtual ICollection<abusereport> abusereports { get; set; }
            public virtual lu_appearance_bodytype  bodytype { get; set; }
            public virtual lu_appearance_eyecolor eyecolor { get; set; }
            public virtual lu_appearance_haircolor haircolor { get; set; }
            public virtual lu_character_diet diet { get; set; }
            public virtual lu_character_drinks drinking { get; set; }
            public virtual lu_character_exercise exercise { get; set; }
            public virtual lu_character_humor humor { get; set; }
            public virtual lu_character_politicalview politicalview { get; set; }
            public virtual lu_character_religion religion { get; set; }
            public virtual lu_character_religiousattendance religiousattendance { get; set; }
            public virtual lu_character_sign sign { get; set; }
            public virtual lu_character_smokes smoking { get; set; }
            public virtual lu_life_educationlevel educationlevel { get; set; }
            public virtual lu_life_employmentstatus employmentstatus { get; set; }
            public virtual lu_life_havekids kidstatus { get; set; }
            public virtual lu_life_incomelevel incomelevel { get; set; }
            public virtual lu_life_livingsituation livingsituation { get; set; }
            public virtual lu_life_maritalstatus maritalstatus { get; set; }
            public virtual lu_life_profession profession { get; set; }
            public virtual lu_life_wantskids wantsKidstatus { get; set; }
                    
                 
            //member actions
            public virtual ICollection<favorite> favorites { get; set; }
            public virtual ICollection<friend> friends { get; set; }
            public virtual ICollection<interest> interests { get; set; }
            public virtual ICollection<like> likes { get; set; }
            public virtual ICollection<block> blocks { get; set; }
            public virtual ICollection<peek> peeks { get; set; }   


            public virtual ICollection<mailboxfolder> mailboxfolders { get; set; }
            public virtual ICollection<membersinrole> membersinroles { get; set; }
            public virtual ICollection<photoalbum> photoalbums { get; set; }
            public virtual ICollection<photo> photos { get; set; }
            public virtual ICollection<photoconversion> convertedphotos { get; set; } 
            public virtual ICollection<profiledata_ethnicity> ethnicities { get; set; }           
            public virtual ICollection<profiledata_hobby> hobbies { get; set; }           
            public virtual ICollection<profiledata_hotfeature> hotfeatures { get; set; }           
            public virtual ICollection<profiledata_lookingfor> lookingfor { get; set; }           
            public virtual ICollection<mailupdatefreqency> mailupdatefreqencies { get; set; }
            public virtual ICollection<ratingvalue> ratingvalues { get; set; }           
                   
            public virtual  visiblitysetting  visibilitysettings { get; set; }                    
            public virtual ICollection<searchsetting> searchsettings { get; set; }
            public virtual ICollection<userlogtime> logontimes { get; set; }
         
    }
}
