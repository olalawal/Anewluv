using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
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
        public virtual lu_messagetype type { get; set; }
        [DataMember()]
        public virtual lu_templatebody body { get; set; }
        [DataMember()]
        public virtual lu_templatesubject subject { get; set; }

    }
}
