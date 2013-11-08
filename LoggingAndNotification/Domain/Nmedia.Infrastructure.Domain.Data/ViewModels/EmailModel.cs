using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nmedia.Infrastructure.Domain.Data.ViewModels
{
    public class EmailModel
    {
        public string to { get; set; }
        public string from { get; set; }  //only used in contact us
        public string subject { get; set; }
        public string body { get; set; }
        public string News { get; set; }  //Lists news 
        public string Messages { get; set; }  //Personal user updates strored here
    }
}
