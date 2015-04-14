using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class MailModel
    {

        [DataMember]
        public int profileid { get; set; }
        [DataMember]
        public int mailboxfolderid { get; set; }
        [DataMember]
        public int mailboxfoldername { get; set; }

        //paging variables
        [DataMember]
        public int? page { get; set; }
        [DataMember]
        public int? numberperpage { get; set; }
        [DataMember]
        public int? currentpage { get; set; }
    }
}
