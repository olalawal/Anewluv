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
        public DbSet<searchsetting_educationlevel> searchsetting_educationlevel { get; set; }
        public DbSet<searchsetting_employmentstatus> searchsetting_employmentstatus { get; set; }
        public DbSet<searchsetting_ethnicity> searchsetting_ethnicity { get; set; }
        public DbSet<searchsetting_exercise> searchsetting_exercise { get; set; }
        public DbSet<searchsetting_eyecolor> searchsetting_eyecolor { get; set; }
        public DbSet<searchsetting_gender> searchsetting_gender { get; set; }
        public DbSet<searchsetting_haircolor> searchsetting_haircolor { get; set; }
        


        public DbSet<searchsetting_havekids> searchsetting_havekids { get; set; }
        public DbSet<searchsetting_hobby> searchsetting_hobby { get; set; }
  
    
      
        public DbSet<searchsetting_humor> searchsetting_humor { get; set; }
        public DbSet<searchsetting_incomelevel> searchsetting_incomelevel { get; set; }
        public DbSet<searchsetting_livingstituation> searchsetting_livingstituation { get; set; }
        public DbSet<searchsetting_location> searchsetting_location { get; set; }
        public DbSet<searchsetting_maritalstatus> searchsetting_maritalstatus { get; set; }
        public DbSet<searchsetting_politicalview> searchsetting_politicalview { get; set; }
        public DbSet<searchsetting_lookingfor> searchsetting_lookingfor { get; set; }

        public DbSet<searchsetting_profession> searchsetting_profession { get; set; }
        public DbSet<searchsetting_religion> searchsetting_religion { get; set; }
        public DbSet<searchsetting_religiousattendance> searchsetting_religiousattendance { get; set; }
        public DbSet<searchsetting_sign> searchsetting_sign { get; set; }
        public DbSet<searchsetting_smokes> searchsetting_smokes { get; set; }
        public DbSet<searchsetting_showme> searchsetting_showme { get; set; }
        public DbSet<searchsetting_sortbytype> searchsetting_sortbytype { get; set; }
       


    }
}
