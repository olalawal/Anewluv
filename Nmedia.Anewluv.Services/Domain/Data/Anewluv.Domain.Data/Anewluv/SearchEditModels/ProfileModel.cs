using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace Anewluv.Domain.Data.ViewModels
{

    [DataContract]
    public class ProfileModel :Entity
    {
       public ProfileModel()
        { 
        
        
        }



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
    

        //added abitlity to add notes 
        [DataMember]
        public string note { get; set; }
        //acdded action to the body
        [DataMember]
        public actiontypeEnum actiontype { get; set; }
        
  


        //search stuff for mail
        [DataMember]
        public mailfoldertypeEnum mailboxfoldertype { get; set; }  //folder types to retrive , only one folder per call for now
        [DataMember]
        public MailViewModel mailviewmodel { get; set; } //mail being sent along with all relevant data 
       



    }
}
