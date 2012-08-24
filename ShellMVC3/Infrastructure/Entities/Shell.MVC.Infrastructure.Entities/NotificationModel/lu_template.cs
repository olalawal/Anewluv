using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{

   

    public class lu_template
    {
        //we generate this manually from enums for now
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
       [DataMember ]
       public int id { get; set; }
       [DataMember]
       public string description { get; set; }
       [DataMember]
       public string physicalLocation { get; set; }
       [DataMember]
       public DateTime creationDate { get; set; }
       [DataMember]
       public DateTime? removalDate { get; set; }
       [DataMember]
       public bool active { get; set; }
       [DataMember]
       public string razorTemplateBody { get; set; }
       [DataMember]
       public string stringTemplateSubject { get; set; }
       [DataMember]
       public string stringTemplateBody { get; set; }

        public static lu_template Create(Action<lu_template> init)
        {
            var messagetemplate = new lu_template();
            //address.MessageAddressID = Guid.NewGuid();
            messagetemplate.creationDate = DateTime.Now;
            messagetemplate.active = true;
            init(messagetemplate); return messagetemplate;
        }


    }
}
