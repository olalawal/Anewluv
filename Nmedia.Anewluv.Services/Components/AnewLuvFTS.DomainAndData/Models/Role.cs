using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class Role
    {
        public Role()
        {
            this.MembersInRoles = new List<MembersInRole>();
        }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<MembersInRole> MembersInRoles { get; set; }
    }
}
