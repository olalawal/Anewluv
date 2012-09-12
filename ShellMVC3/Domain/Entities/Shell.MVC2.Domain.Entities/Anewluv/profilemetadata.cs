using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public class profilemetadata
    {
            // Metadata classes are not meant to be instantiated.
        
            public int id { get; set; }            
                  
            public virtual profile profile { get; set; }
            public virtual profiledata profiledata { get; set; }
                                
                 
            //member actions
            public virtual ICollection<favorite> favoritesadded { get; set; }
            public virtual ICollection<favorite> favorites { get; set; }
            public virtual ICollection<friend> friendsadded { get; set; }
            public virtual ICollection<friend> friends { get; set; }
            public virtual ICollection<interest> interestsadded { get; set; }
            public virtual ICollection<interest> interests { get; set; }
            public virtual ICollection<like> likesadded { get; set; }
            public virtual ICollection<like> likes { get; set; }
            public virtual ICollection<block> blocksadded { get; set; }
            public virtual ICollection<block> blocks { get; set; }
            public virtual ICollection<peek> peeksadded { get; set; }
            public virtual ICollection<peek> peeks { get; set; }
            public virtual ICollection<hotlist> hotlistsadded { get; set; }
            public virtual ICollection<hotlist> hotlists { get; set; }  


            public virtual ICollection<mailboxfolder> mailboxfolders { get; set; }
            public virtual ICollection<mailboxmessage> sentmailboxmessages { get; set; }
            public virtual ICollection<mailboxmessage> receivedmailboxmessages { get; set; }   
            public virtual ICollection<membersinrole> membersinroles { get; set; }
            public virtual ICollection<photoalbum> photoalbums { get; set; }
            public virtual ICollection<photo> photos { get; set; }
            public virtual ICollection<photoconversion> convertedphotos { get; set; } 
            public virtual ICollection<profiledata_ethnicity> ethnicities { get; set; }           
            public virtual ICollection<profiledata_hobby> hobbies { get; set; }           
            public virtual ICollection<profiledata_hotfeature> hotfeatures { get; set; }           
            public virtual ICollection<profiledata_lookingfor> lookingfor { get; set; }           
            public virtual ICollection<mailupdatefreqency> mailupdatefreqencies { get; set; }
            public virtual ICollection<ratingvalue> ratingvalues { get; set; }           
                   
                          
            public virtual ICollection<searchsetting> searchsettings { get; set; }
            public virtual ICollection<userlogtime> logontimes { get; set; }
            public virtual ICollection<abusereport> abusereportadded { get; set; }   //list of reports created by this user 
            public virtual ICollection<abusereport> abusereports { get; set; } 
            
    }
}
