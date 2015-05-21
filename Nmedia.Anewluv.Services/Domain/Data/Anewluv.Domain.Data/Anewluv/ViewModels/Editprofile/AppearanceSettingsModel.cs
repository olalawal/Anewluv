using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Nmedia.Infrastructure.DTOs;

    //TO DO move this data out to a Searchsettings model and create a full view model maybe 
  //that way the edit code is re-usuable, final code change will be search settings object
//i.e   , and appearancesettings  combine into viewmodel
//the search peice will be udpated via searchrepostiory as a separate call maybe since even the matches 
//settings which we are updating is actually just a search
namespace Anewluv.Domain.Data.ViewModels
{
      [DataContract]
    public class AppearanceSettingsModel
    {

    
         [DataMember]
         public long? height { get; set; }
         [DataMember]
         public List<listitem> bodytypelist { get; set; }
         [DataMember]
         public List<listitem> haircolorlist { get; set; }
         [DataMember]
         public List<listitem> eyecolorlist { get; set; }
         [DataMember]
         public List<listitem> hotfeaturelist { get; set; }
         [DataMember]
         public List<listitem> ethnicitylist { get; set; }


      
    }
}
