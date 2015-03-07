using System;
using System.Collections.Generic;

namespace AnewLuvFTS.DomainAndData.Models
{
    public partial class Interest
    {
        public int RecordID { get; set; }
        public string ProfileID { get; set; }
        public int MutualInterest { get; set; }
        public Nullable<System.DateTime> InterestDate { get; set; }
        public string InterestID { get; set; }
        public Nullable<bool> IntrestViewed { get; set; }
        public Nullable<System.DateTime> IntrestViewedDate { get; set; }
        public Nullable<bool> DeletedByProfileID { get; set; }
        public Nullable<System.DateTime> DeletedByProfileIDDate { get; set; }
        public Nullable<bool> DeletedByInterestID { get; set; }
        public Nullable<System.DateTime> DeletedByInterestIDDate { get; set; }
        public virtual ProfileData ProfileData { get; set; }
    }
}
