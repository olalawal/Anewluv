using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Anewluv.Domain.Data.Chat;
//using Nmedia.DataAccess;
//using Nmedia.DataAccess.Test;
using Repository.Pattern.Ef6;

namespace Anewluv.Domain.Chat
{
    public partial  class ChatContext : DataContext
    {
        public DbSet<lu_roomstatus> lu_roomstatus { get; set; }
        public DbSet<lu_userstatus> lu_userstatus { get; set; }
  
    }
}
