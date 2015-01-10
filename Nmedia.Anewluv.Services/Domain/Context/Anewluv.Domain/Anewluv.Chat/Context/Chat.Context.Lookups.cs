using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Anewluv.Domain.Data.Chat;
using Nmedia.DataAccess;

namespace Anewluv.Domain.Chat
{
    public partial  class ChatContext : ContextBase
    {
        public DbSet<lu_roomstatus> lu_roomstatus { get; set; }
        public DbSet<lu_userstatus> lu_userstatus { get; set; }
  
    }
}
