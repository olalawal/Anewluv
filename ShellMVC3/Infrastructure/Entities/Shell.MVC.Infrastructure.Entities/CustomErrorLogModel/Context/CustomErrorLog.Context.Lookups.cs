using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
    public partial class   CustomErrorLogContext : DbContext 
    {
        public DbSet<lu_application> lu_application { get; set; }
        public DbSet<lu_logseverity> lu_logseverity { get; set; }
        public DbSet<lu_logseverityinternal> lu_logseverityinternal { get; set; }
    }
}
