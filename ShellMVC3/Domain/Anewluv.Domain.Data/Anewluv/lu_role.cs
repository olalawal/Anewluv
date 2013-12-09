using System;
using System.Collections.Generic;

namespace Anewluv.Domain.Data
{
    public partial class lu_role
    {
        public lu_role()
        {
            this.applicationroles = new List<applicationrole>();
            this.membersinroles = new List<membersinrole>();
        }

        public int id { get; set; }
        public string description { get; set; }
        public virtual ICollection<applicationrole> applicationroles { get; set; }
        public virtual ICollection<membersinrole> membersinroles { get; set; }
    }
}
