using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{
    public partial  class ChatContext : DbContext
    {
        public DbSet<lu_roomstatus> lu_roomstatus { get; set; }
        public DbSet<lu_userstatus> lu_userstatus { get; set; }
  
    }
}
