using Anewluv.Domain.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Domain.Data.ViewModels
{

    [Serializable]
    [DataContract]
   public class MailSearchResultsViewModel
    {
        public MailSearchResultsViewModel()
        {
            this.results = new List<MailViewModel>();
        }


        [DataMember]
        public List<MailViewModel> results { get; set; }
        [DataMember]
        public int? totalresults { get; set; }
    
    }
}
