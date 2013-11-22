using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Nmedia.Infrastructure.Domain.Data.ViewModels
{

    [DataContract]
    public class ProfileModel
    {
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public int? profileid { get; set; }
        [DataMember]
        public string screenname { get; set; }
        [DataMember]
        public string sessionid { get; set; }
        [DataMember]
        public Guid? photoid { get; set; }
        [DataMember]
        public string securityanswer { get; set; }
        [DataMember]
        public string securityquestion { get; set; }
        [DataMember]
        public string activationcode { get; set; }
        [DataMember]
        public string openididentifier { get; set; }
        [DataMember]
        public string openidprovider { get; set; }
        [DataMember]
        public string Country { get; set; }
    }
}
