using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{

   
    [DataContract]
    public class lu_template
    {
        //we generate this manually from enums for now
     
 [Key]
       public int id { get; set; }
       public string description { get; set; }    
       public string physicallocation { get; set; }   
       public DateTime creationdate { get; set; }   
       public DateTime? removaldate { get; set; }     
       public bool active { get; set; }    
       public string razortemplatebody { get; set; } 
       public virtual lu_templatebody bodystring { get; set; }
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
