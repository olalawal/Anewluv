using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class favorite
    {
        public int id { get; set; }
        public int profile_id { get; set; }
        public int favoriteprofile_id { get; set; }
        public Nullable<System.DateTime> creationdate { get; set; }
        public Nullable<System.DateTime> modificationdate { get; set; }
        public Nullable<System.DateTime> viewdate { get; set; }
        public Nullable<System.DateTime> deletedbymemberdate { get; set; }
        public Nullable<System.DateTime> deletedbyfavoritedate { get; set; }
        public Nullable<bool> mutual { get; set; }
        public virtual profilemetadata profilemetadata { get; set; }
        public virtual profilemetadata profilemetadata1 { get; set; }
    }
}
