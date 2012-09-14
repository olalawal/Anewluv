using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public partial  class AnewluvContext : DbContext
    {
        public DbSet<lu_abusetype> lu_abusetype { get; set; }
        public DbSet<lu_activitytype > lu_activitytype { get; set; }
        public DbSet<lu_notetype> lu_notetype { get; set; }
        public DbSet<lu_defaultmailboxfolder> lu_defaultmailboxfolder { get; set; }
        public DbSet<lu_gender> lu_gender { get; set; }
        public DbSet<lu_height> lu_height { get; set; }
       // public DbSet<lu_photoreviewstatustype> lu_photoreviewstatustype { get; set; }
        public DbSet<lu_photorejectionreason> lu_photorejectionreason { get; set; }
        public DbSet<lu_photoapprovalstatus> lu_photoapprovalstatus { get; set; }  
        public DbSet<lu_photostatus> lu_photostatus { get; set; }
        public DbSet<lu_photostatusdescription> lu_photostatusdescription { get; set; }
        public DbSet<lu_photoformat> lu_photoformat { get; set; }
        public DbSet<lu_photosize> lu_photosize { get; set; }
        public DbSet<lu_profilestatus> lu_profilestatus { get; set; }
        public DbSet<lu_role> lu_role { get; set; }
        public DbSet<lu_securityleveltype> lu_securityleveltype { get; set; }
        public DbSet<lu_showme> lu_showme { get; set; }
        public DbSet<lu_sortbytype > lu_sortbytype { get; set; }
        public DbSet<lu_securityquestion> lu_securityquestion { get; set; }

    }
}
