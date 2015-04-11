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
    public class mailfolderviewmodel
    {
         public mailfolderviewmodel ()
        {
            this.folders = new List<mailfoldermodel>();
        }
        

         [DataMember]
         public List<mailfoldermodel> folders { get; set; }
       

    }
}
