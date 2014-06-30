using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Services.Contracts.ServiceResponse
{
    #region "Anewluv response Message Object"
    [Serializable(), System.Xml.Serialization.SoapType()]
    [DataContract(Name = "ResponseMessage")]
    public class ResponseMessage : IMessage
    {

        private string mDataElement = "";
        private string mMessage = "";
        private string mErrorMessage = "";

        public ResponseMessage()
        {
        }

        public ResponseMessage(string dataElement, string message, string errormessage)
        {
            mDataElement = dataElement;
            mMessage = message;
            mErrorMessage = errormessage;
        }

        [System.Xml.Serialization.SoapElement()]
        [DataMember()]
        public string dataelement
        {
            get { return mDataElement; }
            // Throw New NotSupportedException
            set { mDataElement = value; }
        }

        [System.Xml.Serialization.SoapElement()]
        [DataMember()]
        public string message
        {
            get { return mMessage; }
            // Throw New NotSupportedException
            set { mMessage = value; }
        }

        [System.Xml.Serialization.SoapElement()]
        [DataMember()]
        public string errormessage
        {
            get { return mErrorMessage; }
            //Throw New NotSupportedException
            set { mErrorMessage = value; }
        }
    }
    #endregion
}
