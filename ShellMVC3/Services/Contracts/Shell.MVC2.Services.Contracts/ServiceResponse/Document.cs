using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shell.MVC2.Services.Contracts.ServiceResponse
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.Serialization;

    //---------------------------------------------------------------------
    //- Purpose:  eChain Document sister Class                            -
    //-                                                                   -
    //- Modifications:                                                    -
    //-                                                                   -
    //-    Name      Date     Description                                 -
    //-    olal     12/23//2011   JSOn cannot accept a byte value so      -
    //-     instead of modifying the EchainDOcument objject I created a   -
    //-   one that will eventually replace the older version at some point-
    //---------------------------------------------------------------------
    [Serializable(), System.Xml.Serialization.SoapType()]
    [DataContract()]

    public class Document : IAttachment//JSON
    {
        #region "Private Members"

        private byte[] mDocument;
        private DocumentTypes mDocumentType;
        private string mFileName;
        #endregion
        private string mMimeType;


        public Document()
        {
        }

        public Document(string filename, string mimeType, byte[] document, DocumentTypes docType)
        {
            mFileName = filename;
            mMimeType = mimeType;
            mDocumentType = docType;
            mDocument  = document ;


        }

        [DataMember()]
        public byte[] document
        {
            get { return mDocument; }
            set { mDocument = value; }
        }

        //public string attachment
        //{
        //    get { return document; }
        //    set { document = value; }
        //}

        [DataMember()]
        public DocumentTypes documenttype
        {
            get { return mDocumentType; }
            set { mDocumentType = value; }
        }

        [DataMember()]
        public string filename
        {
            get { return mFileName; }
            set { mFileName = value; }
        }

        [DataMember()]
        public string mimetype
        {
            get { return mMimeType; }
            set { mMimeType = value; }
        }

    }

    [Serializable(), System.Xml.Serialization.SoapInclude(typeof(Document))]
    public class eChainDocumentListJSON : ArrayList
    {
        public new Document this[int index]
        {
            get { return (Document)base[index]; }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Add(Document value)
        {
            return base.Add(value);
        }
    }

    //shares the documentTypes from eChainDocument class
    //Public Enum DocumentTypes
    //    Other = 0
    //    DonorPass = 1
    //    ChainOfCustody = 2
    //    ResultFile = 3
    //End Enum

   
}
