using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{
     [DataContract(Namespace = "")]
    public class messagedetail
    {

        public messagedetail()
{
	//messages = new List<message>();
//	addressType = new lu_addressType();
	//creationdate = DateTime.Now;
	//active = true;
}
        [Key]
        [DataMember()]
        public int id { get; set; }
        [DataMember()]
        public virtual int messagetype_id { get; set; }
        [DataMember()]
        public virtual lu_messagetype messagetype { get; set; }
        [DataMember()]
        public virtual int templatebody_id { get; set; }
        [DataMember()]
        public virtual lu_templatebody templatebody { get; set; }
        [DataMember()]
        public virtual int templatesubject_id { get; set; }
        [DataMember()]
        public virtual lu_templatesubject templatesubject { get; set; }

    }
}
