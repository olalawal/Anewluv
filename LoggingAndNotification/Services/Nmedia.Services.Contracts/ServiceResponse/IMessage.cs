using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Services.Contracts.ServiceResponse
{
    public interface IMessage
    {
        string dataelement { get; set; }
        string message { get; set; }
        string errormessage { get; set; }
    }
}
