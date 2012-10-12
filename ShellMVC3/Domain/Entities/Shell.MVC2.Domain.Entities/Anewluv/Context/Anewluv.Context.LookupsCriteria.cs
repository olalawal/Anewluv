using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public partial  class AnewluvContext : DbContext
    {
        //appearence
        public DbSet<lu_bodytype> lu_bodytype { get; set; }
        public DbSet<lu_ethnicity> lu_ethnicity { get; set; }
        public DbSet<lu_eyecolor> lu_eyecolor { get; set; }
        public DbSet<lu_haircolor> lu_haircolor { get; set; }
        public DbSet<lu_hotfeature> lu_hotfeature { get; set; }
       
        //character
        public DbSet<lu_diet> lu_diet { get; set; }
        public DbSet<lu_drinks> lu_drinks { get; set; }
        public DbSet<lu_exercise> lu_exercise { get; set; }
        public DbSet<lu_hobby> lu_hobby { get; set; }     
        public DbSet<lu_humor> lu_humor { get; set; }
        public DbSet<lu_politicalview> lu_politicalview { get; set; }
        public DbSet<lu_religion> lu_religion { get; set; }
        public DbSet<lu_religiousattendance> lu_religiousattendance { get; set; }
        public DbSet<lu_sign> lu_sign { get; set; }
        public DbSet<lu_smokes> lu_smokes { get; set; }

        public DbSet<lu_educationlevel> lu_educationlevel { get; set; }
        public DbSet<lu_employmentstatus> lu_employmentstatus { get; set; }
        public DbSet<lu_havekids> lu_havekids { get; set; }
        public DbSet<lu_incomelevel> lu_incomelevel { get; set; }
        public DbSet<lu_livingsituation> lu_livingsituation { get; set; }
        public DbSet<lu_lookingfor> lu_lookingfor { get; set; }
        public DbSet<lu_maritalstatus> lu_maritalstatus { get; set; }
        public DbSet<lu_profession> lu_profession { get; set; }
        public DbSet<lu_wantskids> lu_wantskids { get; set; }
       
  
    
    }
}
