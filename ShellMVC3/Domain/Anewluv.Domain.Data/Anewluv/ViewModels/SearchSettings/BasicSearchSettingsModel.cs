using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    [DataContract]
    public class BasicSearchSettingsModel
    {

        [DataMember]
        public int? agemax { get; set; }
        [DataMember]
        public int? agemin { get; set; }
        [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public int? distancefromme { get; set; }
        //Moved to appearabnce settings
        //[DataMember]
        //public int? heightmax { get; set; }
        //[DataMember]
        //public int? heightmin { get; set; }
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

        [DataMember]
        public List<lu_showme> showmelist { get; set; }
        [DataMember]
        public List<lu_gender> genderlist { get; set; }
        [DataMember]
        public List<lu_sortbytype> sortbylist { get; set; }
        [DataMember]
        public List<searchsetting_location> locationlist { get; set; }
    }
}
