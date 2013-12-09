using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class visiblitysetting
    {
        public visiblitysetting()
        {
            this.profiledatas = new List<profiledata>();
            this.visiblitysettings_country = new List<visiblitysettings_country>();
            this.visiblitysettings_gender = new List<visiblitysettings_gender>();
        }

        public int id { get; set; }
        public int profile_id { get; set; }
        public Nullable<int> agemaxvisibility { get; set; }
        public Nullable<int> ageminvisibility { get; set; }
        public Nullable<bool> chatvisiblitytointerests { get; set; }
        public Nullable<bool> chatvisiblitytolikes { get; set; }
        public Nullable<bool> chatvisiblitytomatches { get; set; }
        public Nullable<bool> chatvisiblitytopeeks { get; set; }
        public Nullable<bool> chatvisiblitytosearch { get; set; }
        public Nullable<System.DateTime> lastupdatedate { get; set; }
        public Nullable<bool> mailchatrequest { get; set; }
        public Nullable<bool> mailintrests { get; set; }
        public Nullable<bool> maillikes { get; set; }
        public Nullable<bool> mailmatches { get; set; }
        public Nullable<bool> mailnews { get; set; }
        public Nullable<bool> mailpeeks { get; set; }
        public Nullable<bool> profilevisiblity { get; set; }
        public Nullable<bool> saveofflinechat { get; set; }
        public Nullable<bool> steathpeeks { get; set; }
        public virtual ICollection<profiledata> profiledatas { get; set; }
        public virtual profiledata profiledata { get; set; }
        public virtual ICollection<visiblitysettings_country> visiblitysettings_country { get; set; }
        public virtual ICollection<visiblitysettings_gender> visiblitysettings_gender { get; set; }
    }
}
