using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
   

        /// <summary>
        /// RPX Profile
        /// </summary>
        /// 
      [DataContract]
      [Serializable]
        public class rpxprofile
        {
            /// <summary>
            /// Display name
            /// </summary>
             [DataMember]
          public string displayname { get; set; }

            /// <summary>
            /// Preferred username
            /// </summary>
            [DataMember]  
          public string preferredusername { get; set; }

            /// <summary>
            /// Url
            /// </summary>
            [DataMember]  
          public string url { get; set; }

            /// <summary>
            /// Provider name
            /// </summary>
            [DataMember]  
          public string providername { get; set; }

            /// <summary>
            /// Identifier
            /// </summary>
            [DataMember]  
          public string identifier { get; set; }


             /// <summary>
            /// verifiedEmail
            /// </summary>
            [DataMember]  
          public string verifiedemail { get; set; }

             /// <summary>
            /// photo
            /// </summary>
            [DataMember]  
          public string photo { get; set; }


             /// <summary>
            /// gender
            /// </summary>
            [DataMember]  
          public string gender { get; set; }


              /// <summary>
            /// Birthday
            /// </summary>
            [DataMember]  
          public string birthday { get; set; }

                      

        }

    
}