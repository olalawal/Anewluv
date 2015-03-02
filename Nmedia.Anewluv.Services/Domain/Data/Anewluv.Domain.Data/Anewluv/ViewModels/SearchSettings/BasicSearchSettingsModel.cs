using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Nmedia.Infrastructure.DTOs;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class BasicSearchSettingsModel
    {
        [DataMember]
        public int profileid { get; set; }
        [DataMember]
        public int searchid { get; set; }
        [DataMember]
        public string searchname { get; set; }
        [DataMember]
        public int mygenderid { get; set; }

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
        public int? searchrank { get; set; }
        [DataMember]
        public bool? systemmatch { get; set; }
        [DataMember]
        public List<listitem> showmelist { get; set; }
        [DataMember]
        public List<lu_gender> genderlist { get; set; }
        [DataMember]
        public List<lu_sortbytype> sortbylist { get; set; }
        [DataMember]
        public List<age> agelist { get; set; }
        [DataMember]
        public List<searchsetting_location> locationlist { get; set; }
    }
}
