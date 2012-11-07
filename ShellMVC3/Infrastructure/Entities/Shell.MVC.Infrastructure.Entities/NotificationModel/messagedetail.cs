using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{
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
        public int id { get; set; }
        public virtual lu_messagetype type { get; set; }
        public virtual lu_templatebody body { get; set; }
        public virtual lu_templatesubject subject { get; set; }

    }
}
