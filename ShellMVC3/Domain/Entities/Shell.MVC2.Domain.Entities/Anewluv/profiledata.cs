using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    [Serializable]
    public class profiledata
    {
            // Metadata classes are not meant to be instantiated.

             [DataMember]
            public int id { get; set; }      
            [DataMember]
            public int? age { get; set; }
            [DataMember] 
            public DateTime birthdate { get; set; }
         [DataMember]
            public string city { get; set; }
         [DataMember]    
        public string countryregion { get; set; }
         [DataMember]    
        public string stateprovince { get; set; }
         [DataMember]    
        public int countryid { get; set; }
         [DataMember]    
        public Nullable<double> longitude { get; set; }
         [DataMember]    
        public Nullable<double> latitude { get; set; }
         [DataMember]    
        public string aboutme { get; set; }
         [DataMember]    
        public long? height { get; set; }
         [DataMember]    
        public string mycatchyintroLine { get; set; }
         [DataMember]    
        public string phone { get; set; }
         [DataMember]    
        public string postalcode { get; set; }           
            public virtual profile profile { get; set; }
            public virtual profilemetadata profilemetadata { get; set; }
            public virtual visiblitysetting visibilitysettings { get; set; }   


            //lookups for personal profile details 
            public virtual lu_gender gender { get; set; }          
            public virtual lu_bodytype bodytype { get; set; }
            public virtual lu_eyecolor eyecolor { get; set; }
            public virtual lu_haircolor haircolor { get; set; }
            public virtual lu_diet diet { get; set; }
            public virtual lu_drinks drinking { get; set; }
            public virtual lu_exercise exercise { get; set; }
            public virtual lu_humor humor { get; set; }
            public virtual lu_politicalview politicalview { get; set; }
            public virtual lu_religion religion { get; set; }
            public virtual lu_religiousattendance religiousattendance { get; set; }
            public virtual lu_sign sign { get; set; }
            public virtual lu_smokes smoking { get; set; }
            public virtual lu_educationlevel educationlevel { get; set; }
            public virtual lu_employmentstatus employmentstatus { get; set; }
            public virtual lu_havekids kidstatus { get; set; }
            public virtual lu_incomelevel incomelevel { get; set; }
            public virtual lu_livingsituation livingsituation { get; set; }
            public virtual lu_maritalstatus maritalstatus { get; set; }
            public virtual lu_profession profession { get; set; }
            public virtual lu_wantskids wantsKidstatus { get; set; }
          
    }
}
