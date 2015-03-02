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
    public class lu_template :Entity
    {
        //we generate this manually from enums for now

        [Key]
        [DataMember()]
        public int id { get; set; }
        [DataMember()]
        public string description { get; set; }
        [DataMember()]
        public string defaultlocation { get; set; }
        [DataMember()]
        public DateTime? creationdate { get; set; }
        [DataMember()]
        public DateTime? removaldate { get; set; }
        [DataMember()]
        public bool active { get; set; }
        [DataMember()]
        public virtual int filename_id { get; set; }
        [DataMember()]
        public virtual lu_templatefilename filename { get; set; }
        [DataMember()]
        public virtual int body_id { get; set; }
        [DataMember()]
        public virtual lu_templatebody body { get; set; }
        [DataMember()]
        public virtual int subject_id { get; set; }
        [DataMember()]
        public virtual lu_templatesubject subject { get; set; }

        public static lu_template Create(Action<lu_template> init)
        {
            var messagetemplate = new lu_template();
            //address.MessageAddressID = Guid.NewGuid();
            messagetemplate.creationdate = DateTime.Now;
            messagetemplate.active = true;
            init(messagetemplate); return messagetemplate;
        }


    }
}
