using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
     [DataContract]
    public partial class lu_ethnicity
    {
       
        public lu_ethnicity()
        {
            this.profiledata_ethnicity = new List<profiledata_ethnicity>();
            this.searchsetting_ethnicity = new List<searchsetting_ethnicity>();
        }

      
        [DataMember]   public int id { get; set; }      
        [DataMember]   public string description { get; set; }     
        [IgnoreDataMember]  public virtual ICollection<profiledata_ethnicity> profiledata_ethnicity { get; set; }        
        [IgnoreDataMember]  public virtual ICollection<searchsetting_ethnicity> searchsetting_ethnicity { get; set; }
    }
}
