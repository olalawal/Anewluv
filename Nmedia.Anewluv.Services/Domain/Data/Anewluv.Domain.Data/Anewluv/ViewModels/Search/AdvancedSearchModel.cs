using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    [Serializable]
    [DataContract]
    public class advancedsearchmodel
    {

        [DataMember]
        public BasicSearchSettingsModel basicsearchsettings { get; set; }
        [DataMember]
        public AppearanceSearchSettingsModel appearancesearchsettings { get; set; }
        [DataMember]
        public LifeStyleSearchSettingsModel lifestylesearchsettings { get; set; }
        [DataMember]
        public CharacterSearchSettingsModel charactersearchsettings { get; set; }
        //index of what page we are looking at i.e we want to split up this model into diff partial views
        [DataMember]
        public int? profileid { get; set; }
        [DataMember]
        public int? searchid { get; set; }
        [DataMember]
        public string searchname { get; set; }
        [DataMember]
        public int? searchrank { get; set; }
        [DataMember]
        public int? viewindex { get; set; }
      
     

     

    }
}
