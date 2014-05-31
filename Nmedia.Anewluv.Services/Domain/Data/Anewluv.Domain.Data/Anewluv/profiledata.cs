using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class profiledata
    {
        public profiledata()
        {
            this.communicationquotas = new List<communicationquota>();
            this.photoreviews = new List<photoreview>();
            this.visiblitysettings = new List<visiblitysetting>();
        }

        public int profile_id { get; set; }
        public Nullable<int> age { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public string city { get; set; }
        public string countryregion { get; set; }
        public string stateprovince { get; set; }
        public Nullable<int> countryid { get; set; }
        public Nullable<double> longitude { get; set; }
        public Nullable<double> latitude { get; set; }
        public string aboutme { get; set; }
        public Nullable<long> height { get; set; }
        public string mycatchyintroLine { get; set; }
        public string phone { get; set; }
        public string postalcode { get; set; }
        public Nullable<int> profilemetadata_profile_id { get; set; }
        public Nullable<int> visibilitysettings_id { get; set; }
        public Nullable<int> gender_id { get; set; }
        public Nullable<int> bodytype_id { get; set; }
        public Nullable<int> eyecolor_id { get; set; }
        public Nullable<int> haircolor_id { get; set; }
        public Nullable<int> diet_id { get; set; }
        public Nullable<int> drinking_id { get; set; }
        public Nullable<int> exercise_id { get; set; }
        public Nullable<int> humor_id { get; set; }
        public Nullable<int> politicalview_id { get; set; }
        public Nullable<int> religion_id { get; set; }
        public Nullable<int> religiousattendance_id { get; set; }
        public Nullable<int> sign_id { get; set; }
        public Nullable<int> smoking_id { get; set; }
        public Nullable<int> educationlevel_id { get; set; }
        public Nullable<int> employmentstatus_id { get; set; }
        public Nullable<int> kidstatus_id { get; set; }
        public Nullable<int> incomelevel_id { get; set; }
        public Nullable<int> livingsituation_id { get; set; }
        public Nullable<int> maritalstatus_id { get; set; }
        public Nullable<int> profession_id { get; set; }
        public Nullable<int> wantsKidstatus_id { get; set; }
        public virtual ICollection<communicationquota> communicationquotas { get; set; }
        public virtual lu_bodytype lu_bodytype { get; set; }
        public virtual lu_diet lu_diet { get; set; }
        public virtual lu_drinks lu_drinks { get; set; }
        public virtual lu_educationlevel lu_educationlevel { get; set; }
        public virtual lu_employmentstatus lu_employmentstatus { get; set; }
        public virtual lu_exercise lu_exercise { get; set; }
        public virtual lu_eyecolor lu_eyecolor { get; set; }
        public virtual lu_gender lu_gender { get; set; }
        public virtual lu_haircolor lu_haircolor { get; set; }
        public virtual lu_havekids lu_havekids { get; set; }
        public virtual lu_humor lu_humor { get; set; }
        public virtual lu_incomelevel lu_incomelevel { get; set; }
        public virtual lu_livingsituation lu_livingsituation { get; set; }
        public virtual lu_maritalstatus lu_maritalstatus { get; set; }
        public virtual lu_politicalview lu_politicalview { get; set; }
        public virtual lu_profession lu_profession { get; set; }
        public virtual lu_religion lu_religion { get; set; }
        public virtual lu_religiousattendance lu_religiousattendance { get; set; }
        public virtual lu_sign lu_sign { get; set; }
        public virtual lu_smokes lu_smokes { get; set; }
        public virtual lu_wantskids lu_wantskids { get; set; }
        public virtual ICollection<photoreview> photoreviews { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual profile profile { get; set; }
        public virtual visiblitysetting visiblitysetting { get; set; }
        public virtual ICollection<visiblitysetting> visiblitysettings { get; set; }
    }
}
