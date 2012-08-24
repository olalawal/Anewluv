using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;


using System.Data.Objects;

using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

//namespace Dating.Server.Data.Models
//{
//    public partial class AnewLuvFTSEntities : ObjectContext
//    {

//       //!-- Remeber to change the  input data types to float and output to real if edmx changes on the DatingModel.edmx
//       [EdmFunction("AnewLuvFTSModel.Store", "fnGetDistance")]
//        public double fnGetDistance(double lat1, double long1, double lat2, double long2, string ReturnType)
//        {
//            throw new NotImplementedException();
//        }


//       [EdmFunction("AnewLuvFTSModel.Store", "fnGetLastLoggedOnTime")]
//       public string fnGetLastLoggedOnTime(DateTime lastloggedintime)
//       {
//           throw new NotSupportedException("This function can only be used in a LINQ to Entities query");
//       }

//       [EdmFunction("AnewLuvFTSModel.Store", "fnGetUserOlineStatus")]
//       public bool fnGetUserOlineStatus(string ProfileID)
//       {
//           throw new NotSupportedException("This function can only be used in a LINQ to Entities query");
//       }

//       [EdmFunction("AnewLuvFTSModel.Store", "fnTruncateString")]
//       public string fnTruncateString(string StringToTruncate, int LengthToTrunCateBy)
//       {
//           throw new NotSupportedException("This function can only be used in a LINQ to Entities query");
//       }

//       [EdmFunction("AnewLuvFTSModel.Store", "fnCheckIfBirthDateIsInRange")]
//       public bool  fnCheckIfBirthDateIsInRange(DateTime birthDate, int intAgeMin, int intAgeMax)
//       {
          
//           throw new NotSupportedException("This function can only be used in a LINQ to Entities query");
//       }
//    }
//}




