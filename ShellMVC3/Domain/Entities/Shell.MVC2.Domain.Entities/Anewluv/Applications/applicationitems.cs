using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    public class applicationitem
    {     
        [DataMember]
        [Key]
        public int id { get; set; }
        [DataMember]
        public application application { get; set; }
        [DataMember]
        public profile purchaserprofile { get; set; }
        [DataMember]
        public profile transferprofile { get; set; }
        [DataMember]
        public DateTime purchasedate { get; set; }
        [DataMember]
        public DateTime? transferdate { get; set; }
        public DateTime? activateddate { get; set; }
        public DateTime? expirationdate { get; set; }
        public lu_applicationitemtransfertype transfertype { get; set; }
        public lu_applicationpaymenttype paymenttype { get; set; }
        public 
    }
}
