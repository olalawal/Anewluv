using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class ProfileVisiblitySetting
    {
        public string ProfileID { get; set; }
        public Nullable<bool> MailPeeks { get; set; }
        public Nullable<bool> MailIntrests { get; set; }
        public Nullable<bool> MailLikes { get; set; }
        public Nullable<bool> MailMatches { get; set; }
        public Nullable<bool> MailNews { get; set; }
        public Nullable<bool> ProfileVisiblity { get; set; }
        public Nullable<int> CountryID { get; set; }
        public Nullable<int> GenderID { get; set; }
        public Nullable<bool> SteathPeeks { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<bool> ChatVisiblityToLikes { get; set; }
        public Nullable<bool> ChatVisiblityToInterests { get; set; }
        public Nullable<bool> ChatVisiblityToMatches { get; set; }
        public Nullable<bool> ChatVisiblityToPeeks { get; set; }
        public Nullable<bool> ChatVisiblityToSearch { get; set; }
        public Nullable<bool> MailChatRequest { get; set; }
        public Nullable<bool> SaveOfflineChat { get; set; }
        public Nullable<int> AgeMinVisibility { get; set; }
        public Nullable<int> AgeMaxVisibility { get; set; }
        public virtual gender gender { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
