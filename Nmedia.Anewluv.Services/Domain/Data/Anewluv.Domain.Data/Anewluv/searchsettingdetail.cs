using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data
{
    [DataContract]
    public partial class searchsettingdetail
    {
        public searchsettingdetail()
        {
            this.lu_searchsettingdetailtype = new lu_searchsettingdetailtype();
            this.searchsetting = new searchsetting();
        }

        [DataMember]
        public int id { get; set; }
        public int searchsetting_id { get; set; }
        public int searchsettingdetailtype_id { get; set; }
        public int value { get; set; } //actual value of this instance 
       

        public Nullable<System.DateTime> creationdate { get; set; }        
        public Nullable<System.DateTime> modificationdate { get; set; }     
       
        [DataMember]
        public bool active { get; set; }


        [DataMember]
        public virtual lu_searchsettingdetailtype lu_searchsettingdetailtype { get; set; }
        public virtual searchsetting searchsetting { get; set; }       
       
    }
}
