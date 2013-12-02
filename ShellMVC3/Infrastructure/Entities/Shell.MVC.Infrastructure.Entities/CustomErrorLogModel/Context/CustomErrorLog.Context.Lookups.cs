using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Nmedia.Infrastructure.Domain.errorlog
{
    public partial class   ErrorlogContext : DbContext 
    {
        public DbSet<lu_application> lu_application { get; set; }
        public DbSet<lu_logseverity> lu_logseverity { get; set; }
        public DbSet<lu_logseverityinternal> lu_logseverityinternal { get; set; }
    }
}
