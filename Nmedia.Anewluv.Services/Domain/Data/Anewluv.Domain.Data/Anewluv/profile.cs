using Repository.Pattern.Ef6;
using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class profile : Entity
    {
        public profile()
        {
            
            this.membersinroles = new List<membersinrole>();
            this.openids = new List<openid>();
            this.profileactivities = new List<profileactivity>();
            this.userlogtimes = new List<userlogtime>();
        }

        public int id { get; set; }
       // public int profiledata_id { get; set; }
       // public int profilemeatadata_id  { get; set; }
        public bool? isuseradmin { get; set; } //used to for admin profiles so we dont have to serch on role everytime
        //this way we can hide admin profiles from searchs and not allow them to interact with users.
        //it this is set you also have to add the admin role too.
        public string username { get; set; }
        public string emailaddress { get; set; }
        public string screenname { get; set; }
        public string activationcode { get; set; }
        public string passwordresettoken { get; set; }
        public DateTime? passwordresetwindow { get; set; }
        public Nullable<int> dailsentmessagequota { get; set; }
        public Nullable<int> dailysentemailquota { get; set; }
        public Nullable<byte> forwardmessages { get; set; }
        public Nullable<System.DateTime> logindate { get; set; }
        public Nullable<System.DateTime> modificationdate { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<bool> readprivacystatement { get; set; }
        public Nullable<bool> readtemsofuse { get; set; }
        public string password { get; set; }
        public Guid apikey { get; set; }
        public Nullable<System.DateTime> passwordChangeddate { get; set; }
        public Nullable<int> passwordchangecount { get; set; }
        public Nullable<System.DateTime> failedpasswordchangedate { get; set; }
        public Nullable<int> failedpasswordchangeattemptcount { get; set; }
        public string salt { get; set; }
        public string securityanswer { get; set; }
        public Nullable<int> sentemailquotahitcount { get; set; }
        public Nullable<int> sentmessagequotahitcount { get; set; }     
        public Nullable<int> passwordchangeattempts { get; set; }   
        public int status_id { get; set; }
        public int? securityquestion_id { get; set; }     
        public virtual lu_profilestatus lu_profilestatus { get; set; }
        public virtual lu_securityquestion lu_securityquestion { get; set; }
        public virtual ICollection<membersinrole> membersinroles { get; set; }
        public virtual ICollection<openid> openids { get; set; }
        public virtual ICollection<profileactivity> profileactivities { get; set; }
        public virtual profiledata profiledata { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual ICollection<userlogtime> userlogtimes { get; set; }
        public virtual ICollection<photo> adminmodfiedphotos { get; set; }  //lists photos the user has modfied in admin capcity
    }
}
