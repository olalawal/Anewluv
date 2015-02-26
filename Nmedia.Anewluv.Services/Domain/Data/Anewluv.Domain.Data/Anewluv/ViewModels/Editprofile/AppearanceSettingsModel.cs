using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

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
         public lu_bodytype bodytype { get; set; }
         [DataMember]
         public lu_haircolor haircolor { get; set; }
         [DataMember]
         public lu_eyecolor eyecolor { get; set; }
         [DataMember]
         public List<lu_hotfeature> hotfeaturelist { get; set; }
         [DataMember]
         public List<lu_ethnicity > ethnicitylist { get; set; }


      
    }
}
