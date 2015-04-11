using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Nmedia.Infrastructure;
using System.Runtime.Serialization;
using Anewluv.Domain.Data.Helpers;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class mailfoldermodel
    {
         [DataMember]
         public int folderid { get; set; }
         [DataMember]
         public string foldername { get; set; }
         [DataMember]
         public int? totalmessagecount { get; set; }
         [DataMember]
         public int? undreadmessagecount { get; set; }
         [DataMember]
         public bool active { get; set; }
    }
}
