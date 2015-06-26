using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class action :Entity
    {
        public action()
        {
          //  this.lu_actiontype = new lu_actiontype();
          //  this.targetprofilemetadata = new profilemetadata();
          //  this.creatorprofilemetadata = new profilemetadata();
            this.notes = new List<note>();
        }

        [DataMember]  
       public int id { get; set; }
       public int creator_profile_id { get; set; }
       public int target_profile_id { get; set; }
        public int actiontype_id { get; set; }

        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<System.DateTime> viewdate { get; set; }
        public Nullable<System.DateTime> modificationdate { get; set; }
        public Nullable<System.DateTime> deletedbycreatordate { get; set; }
        public Nullable<System.DateTime> deletedbytargetdate { get; set; }
        
       [DataMember] public bool active { get; set; }
       
     
       [DataMember]
       public virtual lu_actiontype lu_actiontype { get; set; }
       public virtual profilemetadata targetprofilemetadata { get; set; }
       public virtual profilemetadata creatorprofilemetadata { get; set; }
       public virtual ICollection<note> notes { get; set; }
    }
}
