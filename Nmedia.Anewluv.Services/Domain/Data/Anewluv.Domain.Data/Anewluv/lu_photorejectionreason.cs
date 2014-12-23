using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    public partial class lu_photorejectionreason
    {
        public lu_photorejectionreason()
        {
            this.photos = new List<photo>();
        }

        public int id { get; set; }
        [NotMapped, DataMember]  public bool? isselected { get; set; }
        public string description { get; set; }
        public string userMessage { get; set; }
        public virtual ICollection<photo> photos { get; set; }
    }
}
