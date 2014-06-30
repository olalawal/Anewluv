using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Nmedia.Infrastructure.Domain.Data.ViewModels
{
    [Serializable]
    [DataContract(Name = "EmailViewModel")]
    public class EmailViewModel
    {



        //no contructor here will be populated in Notification service
        public EmailViewModel()
        {


        }



        [DataMember]
        public EmailModel userEmailViewModel { get; set; }
        [DataMember]
        public EmailModel adminEmailViewModel { get; set; }
        [DataMember]
        public string SysteMessages { get; set; } //12-19-2012 olawal added for error message logging

    }
}
