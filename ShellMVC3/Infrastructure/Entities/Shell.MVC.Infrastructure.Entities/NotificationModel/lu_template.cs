using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{


   [DataContract(Namespace = "")]
    public class lu_template
    {
        //we generate this manually from enums for now
     
 [Key]
       [DataMember()]
       public int id { get; set; }
 [DataMember()]
 public string description { get; set; }
 [DataMember()]
 public string physicallocation { get; set; }
 [DataMember()]
 public string filename { get; set; }
 [DataMember()]
 public DateTime? creationdate { get; set; }
 [DataMember()]
 public DateTime? removaldate { get; set; }
 [DataMember()]
 public bool active { get; set; }
 [DataMember()]
 public string razortemplatebody { get; set; }
 [DataMember()]
 public virtual lu_templatebody bodystring { get; set; }
 [DataMember()]
 public virtual lu_templatesubject subjectstring { get; set; }

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
