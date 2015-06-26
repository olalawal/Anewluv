using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class lu_hotfeature : Entity
    {
        public lu_hotfeature()
        {
            this.profiledata_hotfeature = new List<profiledata_hotfeature>();
            //this.searchsetting_hotfeature = new List<searchsetting_hotfeature>();
        }

        [DataMember]
        public int id { get; set; }
        [NotMapped, DataMember]
        public bool selected { get; set; }
        [DataMember]
        public string description { get; set; }
        public virtual ICollection<profiledata_hotfeature> profiledata_hotfeature { get; set; }
        //public virtual ICollection<searchsetting_hotfeature> searchsetting_hotfeature { get; set; }
    }
}
