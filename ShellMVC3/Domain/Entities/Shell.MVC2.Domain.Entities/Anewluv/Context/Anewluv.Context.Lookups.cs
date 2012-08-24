using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public partial  class AnewluvContext : DbContext
    {
        public DbSet<lu_abusetype> lu_abusetypes { get; set; }
        public DbSet<lu_defaultmailboxfolder> lu_defaultmailboxfolder { get; set; }
        public DbSet<lu_gender> lu_genders { get; set; }
        public DbSet<lu_height> lu_heights { get; set; }
        public DbSet<lu_photorejectionreason> lu_photorejectionreasons { get; set; }   
        public DbSet<lu_photostatus> lu_photostatuses { get; set; }
        public DbSet<lu_photostatusdescription> lu_photostatusdescription { get; set; }
        public DbSet<lu_phototype> lu_photoImageTypes { get; set; }
        public DbSet<lu_phototypesize> lu_phototypesize { get; set; }
        public DbSet<lu_profilestatus> lu_profilestatuses { get; set; }
        public DbSet<lu_role> lu_roles { get; set; }
        public DbSet<lu_securityleveltype> lu_securityleveltypes { get; set; }
        public DbSet<lu_showme> lu_showme { get; set; }
        public DbSet<lu_sortbytype > lu_sortbytypes { get; set; }
       

    }
}
