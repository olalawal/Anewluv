using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
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
        public int? targetprofileid { get; set; }
        [DataMember]
        public string screenname { get; set; }        
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

        public List<String> targetscreennames { get; set; }

        //added items for stuff like geodata which we want to require maybe at some point
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string stateprovince { get; set; }       
        [DataMember]
        public string Countryname { get; set; }
        [DataMember]
        public double? lattitude { get; set; }
        [DataMember]
        public double? longitude { get; set; }

        //activity base items like IP address session etc
        [DataMember]
        public string ipaddress { get; set; }
        [DataMember]
        public string sessionid { get; set; }

        //paging variables
        [DataMember]
        public int?  page  { get; set; }
        [DataMember]
        public int? numberperpage  { get; set; }
        [DataMember]
        public int? currentpage { get; set; }

        //items needed for profile mapping
        [DataMember]
        public int? viewingprofileid { get; set; }
        [DataMember]
        public List<string> profileids { get; set; }
        [DataMember]
        public MemberSearchViewModel modeltomap { get; set; }
        [DataMember]
        public List<MemberSearchViewModel> modelstomap { get; set; }
        [DataMember]
        public bool allphotos { get; set; }


    }
}
