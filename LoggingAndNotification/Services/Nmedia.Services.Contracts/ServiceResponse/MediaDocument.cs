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
    //- Purpose:  eChain Document Class                                   -
    //-                                                                   -
    //- Modifications:                                                    -
    //-                                                                   -
    //-    Name      Date     Description                                 -
    //-    tab     7/7/2011   Rewritten to get rid of stubs               -
    //---------------------------------------------------------------------
    [Serializable(), System.Xml.Serialization.SoapType()]
    [DataContract()]

    public class MediaDocument : IAttachment
    {
        #region "Private Members"

        private byte[] mAttachment;
        private DocumentTypes mDocumentType;
        private string mFileName;
        #endregion
        private string mMimeType;


        public MediaDocument()
        {
        }

        public MediaDocument(string fileName, string mimeType, DocumentTypes docType, byte[] document)
        {
            mFileName = fileName;
            mMimeType = mimeType;
            mDocumentType = docType;
            mAttachment = document;
        }

        [DataMember()]
        public byte[] Document
        {
            get { return mAttachment; }
            set { mAttachment = value; }
        }
        byte[] Attachment
        {
            get { return Document; }
            set { Document = value; }
        }

        [DataMember()]
        public DocumentTypes DocumentType
        {
            get { return mDocumentType; }
            set { mDocumentType = value; }
        }

        [DataMember()]
        public string FileName
        {
            get { return mFileName; }
            set { mFileName = value; }
        }

        [DataMember()]
        public string MimeType
        {
            get { return mMimeType; }
            set { mMimeType = value; }
        }

    }

    [Serializable(), System.Xml.Serialization.SoapInclude(typeof(MediaDocument))]
    public class eChainDocumentList : ArrayList
    {
        public new MediaDocument this[int index]
        {
            get { return (MediaDocument)base[index]; }
            set
            {
                throw new NotImplementedException();
            }
        }

        public new int Add(MediaDocument value)
        {
            return base.Add(value);
        }
    }

    

   

}
