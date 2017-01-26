using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;


namespace Nmedia.Infrastructure.Domain.Data.Notification
{


     [DataContract(Namespace = "")]
    public class lu_application :Entity
    {
        //we generate this manually from enums for now
        [Key]
        [DataMember()]
        public int id { get; set; }
        [DataMember()]
        public string description { get; set; }
        [DataMember()]
        public bool? active { get; set; }
        [DataMember()]
        public DateTime? creationdate { get; set; }
        [DataMember()]
        public DateTime? removaldate { get; set; }

        [DataMember]
        public string fromemailaddress { get; set; }

        [DataMember]
        public string logourl { get; set; }
        [DataMember]
        public string emaildeliverystring { get; set; }
        [DataMember]
        public string photourl { get; set; }
        [DataMember]
        public string bottombulleturl { get; set; }

        [DataMember]
        public string activationURL { get; set; }

        [DataMember]
        public string recoveryURL { get; set; }

        [DataMember]
        public string contactusURL { get; set; }

        [DataMember]
        public string websiteURL { get; set; }

        [DataMember]
        public string loginURL { get; set; }

       // noreply @anewluv.com
    }
}
