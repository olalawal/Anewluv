using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class profilemetadata : Entity
    {
        public profilemetadata()
        {
          
          
         
            this.mailboxfolders = new List<mailboxfolder>();
            this.sentmailboxmessages = new List<mailboxmessage>();
            this.receivedmailboxmessages = new List<mailboxmessage>();
            this.mailupdatefreqencies = new List<mailupdatefreqency>();           
            this.photoalbums = new List<photoalbum>();
            this.photos = new List<photo>();
            this.profiledata_ethnicity = new List<profiledata_ethnicity>();
            this.profiledata_hobby = new List<profiledata_hobby>();
            this.profiledata_hotfeature = new List<profiledata_hotfeature>();
            this.profiledata_lookingfor = new List<profiledata_lookingfor>();
            this.profiledata = new profiledata();
            this.raterratingvalues = new List<ratingvalue>();
            this.rateeratingvalues = new List<ratingvalue>();
       
            this.searchsettings = new List<searchsetting>();
            this.photoreviews = new List<photoreview>();

        }

        public int id { get; set; }
        public int profiledata_id { get; set; }
        public int profile_id { get; set; }      
        public virtual ICollection<mailboxfolder> mailboxfolders { get; set; }       
        public virtual ICollection<mailboxmessage> sentmailboxmessages { get; set; }
        public virtual ICollection<mailboxmessage> receivedmailboxmessages { get; set; }     
        public virtual ICollection<mailupdatefreqency> mailupdatefreqencies { get; set; }       
        public virtual ICollection<photoalbum> photoalbums { get; set; }
        public virtual ICollection<photo> photos { get; set; }
        public virtual ICollection<photoreview> photoreviews { get; set; }
        public virtual ICollection<profiledata_ethnicity> profiledata_ethnicity { get; set; }
        public virtual ICollection<profiledata_hobby> profiledata_hobby { get; set; }
        public virtual ICollection<profiledata_hotfeature> profiledata_hotfeature { get; set; }
        public virtual ICollection<profiledata_lookingfor> profiledata_lookingfor { get; set; }
        public virtual profiledata profiledata { get; set; }
        public virtual profile profile { get; set; }

        public virtual ICollection<ratingvalue> raterratingvalues { get; set; }
        public virtual ICollection<ratingvalue> rateeratingvalues { get; set; }

        public virtual ICollection<searchsetting> searchsettings { get; set; }

        

        //added actions for consolidation
        public virtual ICollection<action> createdactions { get; set; }
        public virtual ICollection<action> targetofactions { get; set; }
        //consoldiated applications]
        public virtual ICollection<application> applications { get; set; }


    }
}
