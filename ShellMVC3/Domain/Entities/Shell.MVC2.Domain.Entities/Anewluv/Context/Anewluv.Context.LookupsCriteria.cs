using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public partial  class AnewluvContext : DbContext
    {
        public DbSet<lu_appearance_bodytype> lu_appearance_bodytype { get; set; }
        public DbSet<lu_appearance_ethnicity> lu_appearance_ethnicity { get; set; }
        public DbSet<lu_appearance_eyecolor> lu_appearance_eyecolor { get; set; }
        public DbSet<lu_appearance_haircolor> lu_appearance_haircolor { get; set; }
       
        public DbSet<lu_character_diet> lu_character_diet { get; set; }
        public DbSet<lu_character_drinks> lu_character_drinks { get; set; }
        public DbSet<lu_character_exercise> lu_character_exercise { get; set; }
        public DbSet<lu_character_hobby> lu_character_hobby { get; set; }
        public DbSet<lu_character_hotfeature> lu_character_hotfeature { get; set; }
        public DbSet<lu_character_humor> lu_character_humor { get; set; }
        public DbSet<lu_character_politicalview> lu_character_politicalview { get; set; }
        public DbSet<lu_character_religion> lu_character_religion { get; set; }
        public DbSet<lu_character_religiousattendance> lu_character_religiousattendance { get; set; }
        public DbSet<lu_character_sign> lu_character_sign { get; set; }
        public DbSet<lu_character_smokes> lu_character_smokes { get; set; }

        public DbSet<lu_life_educationlevel> lu_life_educationlevel { get; set; }
        public DbSet<lu_life_employmentstatus> lu_life_employmentstatus { get; set; }
        public DbSet<lu_life_havekids> lu_life_havekids { get; set; }
        public DbSet<lu_life_incomelevel> lu_life_incomelevel { get; set; }
        public DbSet<lu_life_livingsituation> lu_life_livingsituation { get; set; }
        public DbSet<lu_life_maritalstatus> lu_life_maritalstatus { get; set; }
        public DbSet<lu_life_profession> lu_life_profession { get; set; }
        public DbSet<lu_life_wantskids> lu_life_wantskids { get; set; }
  
    
    }
}
