using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;

namespace Shell.MVC2.Services.Contracts.ServiceResponse
{
    public interface IResponse
    {
        DateTime responsecreatedate { get; set; }
        string profileid1 { get; set; }
        string profileid2 { get; set; }
        string email { get; set; }
        profilestatusEnum profilestatus { get; set; }
        DateTime profilestatusdate { get; set; }
        bool requestreturnflag { get; set; }
        //Property ResponseMessages() As AnewluvMessageArrayList
        //Property AnewluvDocuments() As AnewluvDocumentList
    }

}
