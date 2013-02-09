using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public partial class AnewluvContext : DbContext
    {

        public DbSet<searchsetting> searchsetting { get; set; }
        public DbSet<searchsetting_bodytype> searchsetting_bodytype { get; set; }
        public DbSet<searchsetting_drink> searchsetting_drink { get; set; }
        public DbSet<searchsetting_diet> searchsetting_diet { get; set; }
        
       


    }
}
