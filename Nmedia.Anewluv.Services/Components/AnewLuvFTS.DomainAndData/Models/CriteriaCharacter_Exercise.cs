using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class CriteriaCharacter_Exercise
    {
        public CriteriaCharacter_Exercise()
        {
            this.ProfileDatas = new List<ProfileData>();
            this.SearchSettings_Exercise = new List<SearchSettings_Exercise>();
        }

        public int ExerciseID { get; set; }
        public string ExerciseName { get; set; }
        public virtual ICollection<ProfileData> ProfileDatas { get; set; }
        public virtual ICollection<SearchSettings_Exercise> SearchSettings_Exercise { get; set; }
    }
}
