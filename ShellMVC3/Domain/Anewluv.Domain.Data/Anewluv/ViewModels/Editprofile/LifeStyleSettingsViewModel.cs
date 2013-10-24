using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class LifeStyleSettingsViewModel
    {
        
        [DataMember]
        public LifeStyleSettingsModel lifestylesettings { get; set; }
        [DataMember]
        public LifeStyleSearchSettingsModel lifestylesearchsettings { get; set; }
       
      
    }
}
