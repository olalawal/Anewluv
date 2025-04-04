﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Nmedia.Infrastructure.Domain.Data.log;
//using Nmedia.DataAccess;
using Repository.Pattern.Ef6;

namespace Nmedia.Infrastructure.Domain
{
    public partial class LoggingContext : DataContext 
    {
        public DbSet<lu_logapplication> lu_application { get; set; }
        public DbSet<lu_logenviroment> lu_enviroment { get; set; }        
        public DbSet<lu_logseverity> lu_logseverity { get; set; }
        public DbSet<lu_logseverityinternal> lu_logseverityinternal { get; set; }
    }
}
