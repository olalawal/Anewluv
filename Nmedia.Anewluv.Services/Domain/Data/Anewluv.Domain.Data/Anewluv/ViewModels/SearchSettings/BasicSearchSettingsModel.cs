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

        public BasicSearchSettingsModel()
        {

            this.showmelist = new List<listitem>();
            this.genderlist = new List<listitem>();
            this.sortbylist = new List<listitem>();
            this.agelist = new List<age>();
            this.locationlist = new List<searchsetting_location>();
        
        }

        [DataMember]
        public int? profileid { get; set; }
        [DataMember]
        public int? searchid { get; set; }
        [DataMember]
        public string searchname { get; set; }
        [DataMember]
        public int? mygenderid { get; set; }

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
        public List<listitem> genderlist { get; set; }
        [DataMember]
        public List<listitem> sortbylist { get; set; }
        [DataMember]
        public List<age> agelist { get; set; }
        //Viewmodel only feild for lisitng locations 
        [DataMember]
        public List<searchsetting_location> locationlist { get; set; }
    }
}
