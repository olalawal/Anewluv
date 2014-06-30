 using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using Shell.MVC2.Domain.Entities.Anewluv;

namespace Shell.MVC2.Services.Contracts.ServiceResponse
{
   
   
 

    //---------------------------------------------------------------------
    //- Purpose:  Anewluv Response Class                                   -
    //-                                                                   -
    //- Modifications:                                                    -
    //-                                                                   -
    //-    Name      Date     Description                                 -
    //-    tab     7/7/2011   Rewritten to get rid of stubs               -
    //---------------------------------------------------------------------

    [Serializable()]
    [DataContract(Name = "AnewluvResponse")]
    [KnownType(typeof(AnewluvResponse))]
    public class AnewluvResponse : IResponse
    {
        #region "Private Members"
        private string mProfileid1 = string.Empty;
        private string mProfileid2 = string.Empty;
        private string mEmail = string.Empty;
        private bool mRequestReturnFlag = false;
        private DateTime mResponseCreateDate;
        private profilestatusEnum  mCurrentStatusCode;
        private DateTime mCurrentStatusDate;
        private DateTime mProfileStatusDate;
        private profilestatusEnum mProfilestatus;
        private List<ResponseMessage> mResponseMessages = new List<ResponseMessage>();
        #endregion
        private List<Document> mDocuments = new List<Document>();


        #region "public members"
        /// <summary>
        /// Date the response object was created.
        /// </summary>
        /// 
        [DataMember()]
        public System.DateTime responsecreatedate
        {
            get { return mResponseCreateDate; }
            set { mResponseCreateDate = value; }
        }

        /// <summary>
        /// Date the response object was created.
        /// </summary>
        /// 
        [DataMember()]
        public System.DateTime profilestatusdate
        {
            get { return mProfileStatusDate ; }
            set { mProfileStatusDate  = value; }
        }

        /// <summary>
        /// Date the response object was created.
        /// </summary>
        /// 
        [DataMember()]
        public profilestatusEnum profilestatus
        {
            get { return mProfilestatus ; }
            set { mProfilestatus  = value; }
        }

        /// <summary>
        /// Primary customer order number.
        /// </summary>
        /// 
        [DataMember()]
        public string profileid1
        {
            get { return mProfileid1; }
            set { mProfileid1 = value; }
        }

        /// <summary>
        /// Secondary customer order number.
        /// </summary>
        /// 
        [DataMember()]
        public string profileid2
        {
            get { return mProfileid2; }
            set { mProfileid2 = value; }
        }

        /// <summary>
        /// Medtox Anewluv order number.
        /// </summary>
        /// 
        [DataMember()]
        public string email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        /// <summary>
        /// Current order status value.
        /// </summary>
        /// 
        [DataMember()]
        public profilestatusEnum  currentstatuscode
        {
            get { return mCurrentStatusCode; }
            set { mCurrentStatusCode = value; }
        }

        /// <summary>
        /// Date of current order status.
        /// </summary>
        /// 
        [DataMember()]
        public System.DateTime currentstatusdate
        {
            get { return mCurrentStatusDate; }
            set { mCurrentStatusDate = value; }
        }

        /// <summary>
        /// Indicator of success/failure of request.
        /// </summary>
        /// 
        [DataMember()]
        public bool requestreturnflag
        {
            get { return mRequestReturnFlag; }
            set { mRequestReturnFlag = value; }
        }

        [DataMember()]
        public List<ResponseMessage> ResponseMessages
        {
            get { return mResponseMessages; }
            set { mResponseMessages = value; }
        }

        [DataMember()]
        public List<Document> Documents
        {
            get { return mDocuments; }
            set { mDocuments = value; }
        }

        #endregion

        #region "Constructors"

        /// <summary>
        /// Constructor.
        /// </summary>
        /// 

        public AnewluvResponse()
        {
            
        }

        ///// -----------------------------------------------------------------------------
        ///// <summary>
        ///// Constructor.  Prepopulates properties from an IOrderStatus instance.
        ///// For use in IStatusUpdater.UpdateStatus.
        ///// </summary>
        ///// <param name="status">IOrderStatus instance</param>
        ///// <remarks>
        ///// </remarks>
        ///// <history>
        ///// 	[kstacy]	11/11/2005	Created
        ///// </history>
        ///// -----------------------------------------------------------------------------
        //public AnewluvResponseNew(profilestatusEnum status)
        //{
        //    mProfileid1 = status.;
        //    mProfileid2 = status.CustomerOrderNumber2;
        //    mEmail = status.OrderNumber;
        //    mRequestReturnFlag = false;
        //    mResponseCreateDate = DateTime.Now;
        //    mCurrentStatusCode = status.Status;
        //    mCurrentStatusDate = status.ReportedDate;
        //}
        /// -----------------------------------------------------------------------------
        /// <summary>
        /// Constructor.  Prepopulates properties from an IOrderResult instance.
        /// For use in IStatusUpdater.UpdateResult. or creation
        /// </summary>
        /// <param name="result">IOrderResult instance</param>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// 	[kstacy]	11/11/2005	Created
        /// </history>
        /// -----------------------------------------------------------------------------
        public AnewluvResponse(IProfileStatus result)
        {
            mProfileid1 = result.Profileid1 ;
            mProfileid2 = result.Profileid2 ;
            mEmail = result.Email ;
            mRequestReturnFlag = false;
            mResponseCreateDate = DateTime.Now;
            mCurrentStatusCode = result.Status ;
            mCurrentStatusDate = result.RequestCreateDate;
              
        }

        #endregion

        #region "Anewluv documents Object"

        #endregion

        

        

    }

}
