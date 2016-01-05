using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Anewluv.Domain.Data;




namespace Anewluv.Domain.Data.ViewModels
{

    [Serializable]
  [DataContract]
    public class SearchResultsViewModel
    {
        public SearchResultsViewModel()
        {
            this.orderedresultids = new List<SearchResult>();
        }



        [DataMember]
        public List<MemberSearchViewModel> results { get; set; }
        [DataMember]
        public int? totalresults { get; set; }

        //list of all the profile ID's in order with the counters in the results
        [DataMember]
        public List<SearchResult> orderedresultids { get; set; }
    
    }
}
