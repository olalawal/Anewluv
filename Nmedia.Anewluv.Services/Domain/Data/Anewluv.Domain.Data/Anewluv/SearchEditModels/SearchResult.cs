using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Domain.Data.ViewModels
{    

        [DataContract]
        [Serializable]
        public class SearchResult
        {          
            [DataMember]
            public int id { get; set; }
            [DataMember]
            public bool selected { get; set; }
            [DataMember]
            public int searchindex { get; set; }         
  


        }
    
}
