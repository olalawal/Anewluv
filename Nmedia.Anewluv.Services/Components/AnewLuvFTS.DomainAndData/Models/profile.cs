using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class profile
    {
        public profile()
        {
            this.MembersInRoles = new List<MembersInRole>();
            this.ProfileGeoDataLoggers = new List<ProfileGeoDataLogger>();
            this.profileOpenIDStores = new List<profileOpenIDStore>();
        }

        public int ProfileIndex { get; set; }
        public string UserName { get; set; }
        public string ProfileID { get; set; }
        public Nullable<byte> ForwardMessages { get; set; }
        public System.DateTime CreationDate { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public System.DateTime LoginDate { get; set; }
        public string ActivationCode { get; set; }
        public int ProfileStatusID { get; set; }
        public string ScreenName { get; set; }
        public Nullable<byte> SecurityQuestionID { get; set; }
        public string SecurityAnswer { get; set; }
        public string Password { get; set; }
        public string salt { get; set; }
        public Nullable<System.DateTime> PasswordChangedDate { get; set; }
        public Nullable<int> PasswordChangeAttempts { get; set; }
        public Nullable<int> PasswordChangedCount { get; set; }
        public Nullable<bool> ReadTemsOfUse { get; set; }
        public Nullable<bool> ReadPrivacyStatement { get; set; }
        public Nullable<int> DailySentEmailQuota { get; set; }
        public Nullable<int> DailSentMessageQuota { get; set; }
        public Nullable<int> SentEmailQuotaHitCount { get; set; }
        public Nullable<int> SentMessageQuotaHitCount { get; set; }
        public virtual ICollection<MembersInRole> MembersInRoles { get; set; }
        public virtual ProfileData ProfileData { get; set; }
        public virtual ICollection<ProfileGeoDataLogger> ProfileGeoDataLoggers { get; set; }
        public virtual ICollection<profileOpenIDStore> profileOpenIDStores { get; set; }
        public virtual profilestatus profilestatus { get; set; }
        public virtual SecurityQuestion SecurityQuestion { get; set; }
    }
}
