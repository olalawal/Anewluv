using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class SearchSettings_Exercise
    {
        public int SearchSettings_ExerciseID { get; set; }
        public Nullable<int> SearchSettingsID { get; set; }
        public Nullable<int> ExerciseID { get; set; }
        public virtual CriteriaCharacter_Exercise CriteriaCharacter_Exercise { get; set; }
        public virtual SearchSetting SearchSetting { get; set; }
    }
}
