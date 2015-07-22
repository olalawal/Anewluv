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
        [DataMember]
        public List<MemberSearchViewModel> results { get; set; }
        [DataMember]
        public int? totalresults { get; set; }
    
    }
}
