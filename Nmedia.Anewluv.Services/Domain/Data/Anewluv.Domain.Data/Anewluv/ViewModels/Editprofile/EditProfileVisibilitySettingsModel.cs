using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using Anewluv.Domain.Data;
using System.Collections;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
           

    public class ProfileVisibilitySettingsModel
    {
        //basic settings
        [DataMember]
        public Boolean? mailpeeks { get; set; }
        [DataMember]
        public Boolean? mailinterests { get; set; }
        [DataMember]
        public Boolean? maillikes { get; set; }
        [DataMember]
        public Boolean? mailnews { get; set; }
        [DataMember]
        public Boolean mailchatrequest { get; set; }
        [DataMember]
        public Boolean? profilevisibility { get; set; }
        [DataMember]
        public int? countryid { get; set; }
        [DataMember]
        public int? genderid { get; set; }
        [DataMember]
        public Boolean? stealthpeeks { get; set; }
        [DataMember]
        public int? agemin { get; set; }
        [DataMember]
        public int? agemax { get; set; }
        [DataMember]
        public DateTime? lastupdatetime { get; set; }
        [DataMember]
        public Boolean? chatvisibilitytolikes { get; set; }
        [DataMember]
        public Boolean? chatvisibilitytointerests { get; set; }
        [DataMember]
        public Boolean? chatvisibilitytomatches { get; set; }
        [DataMember]
        public Boolean? chatvisibilitytopeeks { get; set; }
        [DataMember]
        public Boolean? chatvisibilitytosearch { get; set; }
        [DataMember]
        public Boolean? saveofflinechatmessages { get; set; }
      
    }
}
