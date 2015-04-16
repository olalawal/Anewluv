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
        public int destinationmailboxfolderid { get; set; }
        [DataMember]
        public string mailboxfoldername { get; set; }
        [DataMember]
        public List<int> deletemailboxmessagesids { get; set; }
        [DataMember]
        public List<int> movemailboxmessagesids { get; set; }


        //message details being sent
        [DataMember]
        public int recipeintprofileid { get; set; }
        [DataMember]
        public string subject { get; set; }
        [DataMember]
        public string body { get; set; }
              
        
        //paging variables
        [DataMember]
        public int? page { get; set; }
        [DataMember]
        public int? numberperpage { get; set; }
        [DataMember]
        public int? currentpage { get; set; }
    }
}
