using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    //TO do use stripped down classes , we only need the quick search and the geo stuff from register
   [ DataContract]
    public class ValidateRegistrationGeoDataModel
    {
       [DataMember ]
        public registermodel GeoRegisterModel {get;set;}
        [DataMember]
        public MembersViewModel GeoMembersModel { get; set; }

    }
}
