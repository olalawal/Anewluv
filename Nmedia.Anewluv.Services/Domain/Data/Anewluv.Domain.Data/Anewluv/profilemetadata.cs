using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class profilemetadata
    {
        public profilemetadata()
        {
            this.abusereportnotes = new List<abusereportnote>();
            this.abusereports = new List<abusereport>();
            this.abusereports1 = new List<abusereport>();
            this.blocknotes = new List<blocknote>();
            this.blocks = new List<block>();
            this.blocks1 = new List<block>();
            this.favorites = new List<favorite>();
            this.favorites1 = new List<favorite>();
            this.friends = new List<friend>();
            this.friends1 = new List<friend>();
            this.hotlists = new List<hotlist>();
            this.hotlists1 = new List<hotlist>();
            this.interests = new List<interest>();
            this.interests1 = new List<interest>();
            this.likes = new List<like>();
            this.likes1 = new List<like>();
            this.mailboxfolders = new List<mailboxfolder>();
            this.mailboxmessages = new List<mailboxmessage>();
            this.mailboxmessages1 = new List<mailboxmessage>();
            this.mailupdatefreqencies = new List<mailupdatefreqency>();
            this.peeks = new List<peek>();
            this.peeks1 = new List<peek>();
            this.photoalbums = new List<photoalbum>();
            this.photos = new List<photo>();
            this.profiledata_ethnicity = new List<profiledata_ethnicity>();
            this.profiledata_hobby = new List<profiledata_hobby>();
            this.profiledata_hotfeature = new List<profiledata_hotfeature>();
            this.profiledata_lookingfor = new List<profiledata_lookingfor>();
            this.profiledatas = new List<profiledata>();
            this.ratingvalues = new List<ratingvalue>();
            this.ratingvalues1 = new List<ratingvalue>();
            this.searchsettings = new List<searchsetting>();
        }

        public int profile_id { get; set; }
        public virtual ICollection<abusereportnote> abusereportnotes { get; set; }
        public virtual ICollection<abusereport> abusereports { get; set; }
        public virtual ICollection<abusereport> abusereports1 { get; set; }
        public virtual ICollection<blocknote> blocknotes { get; set; }
        public virtual ICollection<block> blocks { get; set; }
        public virtual ICollection<block> blocks1 { get; set; }
        public virtual ICollection<favorite> favorites { get; set; }
        public virtual ICollection<favorite> favorites1 { get; set; }
        public virtual ICollection<friend> friends { get; set; }
        public virtual ICollection<friend> friends1 { get; set; }
        public virtual ICollection<hotlist> hotlists { get; set; }
        public virtual ICollection<hotlist> hotlists1 { get; set; }
        public virtual ICollection<interest> interests { get; set; }
        public virtual ICollection<interest> interests1 { get; set; }
        public virtual ICollection<like> likes { get; set; }
        public virtual ICollection<like> likes1 { get; set; }
        public virtual ICollection<mailboxfolder> mailboxfolders { get; set; }
        public virtual ICollection<mailboxmessage> mailboxmessages { get; set; }
        public virtual ICollection<mailboxmessage> mailboxmessages1 { get; set; }
        public virtual ICollection<mailupdatefreqency> mailupdatefreqencies { get; set; }
        public virtual ICollection<peek> peeks { get; set; }
        public virtual ICollection<peek> peeks1 { get; set; }
        public virtual ICollection<photoalbum> photoalbums { get; set; }
        public virtual ICollection<photo> photos { get; set; }
        public virtual ICollection<profiledata_ethnicity> profiledata_ethnicity { get; set; }
        public virtual ICollection<profiledata_hobby> profiledata_hobby { get; set; }
        public virtual ICollection<profiledata_hotfeature> profiledata_hotfeature { get; set; }
        public virtual ICollection<profiledata_lookingfor> profiledata_lookingfor { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual profile profile { get; set; }
        public virtual ICollection<ratingvalue> ratingvalues { get; set; }
        public virtual ICollection<ratingvalue> ratingvalues1 { get; set; }
        public virtual ICollection<searchsetting> searchsettings { get; set; }
    }
}
