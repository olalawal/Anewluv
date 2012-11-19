using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    [DataContract]
    public class profilemetadata
    {
            // Metadata classes are not meant to be instantiated.
            [DataMember]
            public int id { get; set; }            
                  
            public virtual profile profile { get; set; }
            public virtual profiledata profiledata { get; set; }
                                
                 
            //member actions
            [DataMember]
            public virtual ICollection<favorite> favoritesadded { get; set; }
            [DataMember]
            public virtual ICollection<favorite> favorites { get; set; }
            [DataMember]
            public virtual ICollection<friend> friendsadded { get; set; }
            [DataMember]
            public virtual ICollection<friend> friends { get; set; }
            [DataMember]
            public virtual ICollection<interest> interestsadded { get; set; }
            [DataMember]
            public virtual ICollection<interest> interests { get; set; }
            [DataMember]
            public virtual ICollection<like> likesadded { get; set; }
            [DataMember]
            public virtual ICollection<like> likes { get; set; }
            [DataMember]
            public virtual ICollection<peek> peeksadded { get; set; }
            [DataMember]
            public virtual ICollection<peek> peeks { get; set; }
            [DataMember]
            public virtual ICollection<hotlist> hotlistsadded { get; set; }
            [DataMember]
            public virtual ICollection<hotlist> hotlists { get; set; }
            [DataMember]
            public virtual ICollection<block> blocksadded { get; set; }
            [DataMember]
            public virtual ICollection<block> blocks { get; set; }
            [DataMember]
            public virtual ICollection<profiledata_ethnicity> ethnicities { get; set; }
            [DataMember]
            public virtual ICollection<profiledata_hobby> hobbies { get; set; }
            [DataMember]
            public virtual ICollection<profiledata_hotfeature> hotfeatures { get; set; }
            [DataMember]
            public virtual ICollection<profiledata_lookingfor> lookingfor { get; set; } 
            //public virtual ICollection<membersinrole> membersinroles { get; set; }  //roles is tied to profile
            [DataMember]
            public virtual ICollection<photoalbum> photoalbums { get; set; }
            [DataMember]
            public virtual ICollection<photo> photos { get; set; }


            [DataMember]
            public virtual ICollection<mailboxfolder> mailboxfolders { get; set; }
            [DataMember]
            public virtual ICollection<mailboxmessage> sentmailboxmessages { get; set; }
            [DataMember]
            public virtual ICollection<mailboxmessage> receivedmailboxmessages { get; set; }   
           //public virtual ICollection<photoconversion> convertedphotos { get; set; }  //conveted photos is a navigation proptery from photo 
            //i.e you have to go through photo to get it .
            [DataMember]
            public virtual ICollection<mailupdatefreqency> mailupdatefreqencies { get; set; }
            [DataMember]
            public virtual ICollection<ratingvalue> ratingvaluesadded { get; set; }
            [DataMember]
            public virtual ICollection<ratingvalue> ratingvalues { get; set; }

                  [DataMember]         
            public virtual ICollection<searchsetting> searchsettings { get; set; }
            [DataMember]  
            public virtual ICollection<abusereport> abusereportadded { get; set; }   //list of reports created by this user 
            [DataMember]
            public virtual ICollection<abusereport> abusereports { get; set; } 
            
    }
}
