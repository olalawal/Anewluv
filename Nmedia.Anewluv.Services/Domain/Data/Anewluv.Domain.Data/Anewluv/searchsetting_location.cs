using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class searchsetting_location
    {
      [DataMember]  public int id { get; set; }
      [DataMember]
      public string city { get; set; }
      [DataMember]
      public Nullable<int> countryid { get; set; }
      [DataMember]
      public string postalcode { get; set; }
      [DataMember]
      public Nullable<int> searchsetting_id { get; set; }
     
      public virtual searchsetting searchsetting { get; set; }
    }
}
