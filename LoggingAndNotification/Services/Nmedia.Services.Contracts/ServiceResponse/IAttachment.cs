using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Services.Contracts.ServiceResponse
{
    public interface IAttachment
    {
        string filename { get; set; }
        string mimetype { get; set; }
        DocumentTypes documenttype { get; set; }
        //string attachment { get; set; }
        byte[] document { get; set; }
    }

    //public interface IAttachment
    //{
    //    string FileName { get; set; }
    //    string MimeType { get; set; }
    //    DocumentTypes DocumentType { get; set; }
       
    //}
}
