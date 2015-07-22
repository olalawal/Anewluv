using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Nmedia.Infrastructure;
using System.Runtime.Serialization;


namespace Anewluv.Domain.Data.ViewModels
{
    
    [DataContract] 
    public class MailFoldersViewModel
    {
        public MailFoldersViewModel()
        {
            this.folders = new List<MailFolderViewModel>();
        }
        

         [DataMember]
        public List<MailFolderViewModel> folders { get; set; }
       

    }
}
