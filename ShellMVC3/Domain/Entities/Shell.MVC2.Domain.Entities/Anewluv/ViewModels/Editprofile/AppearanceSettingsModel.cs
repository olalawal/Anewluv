using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

    //TO DO move this data out to a Searchsettings model and create a full view model maybe 
  //that way the edit code is re-usuable, final code change will be search settings object
//i.e appearancesearchsettings  , and appearancesettings  combine into viewmodel
//the search peice will be udpated via searchrepostiory as a separate call maybe since even the matches 
//settings which we are updating is actually just a search
namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
      [DataContract]
    public class AppearanceSettingsModel
    {

         [DataMember]
         public int myheight { get; set; }
         [DataMember]
         public lu_bodytype mybodytype { get; set; }
         [DataMember]
         public lu_haircolor myhaircolor { get; set; }
         [DataMember]
         public lu_eyecolor myeyecolor { get; set; }
         [DataMember]
         public List<lu_hotfeature> myhotfeaturelist { get; set; }
         [DataMember]
         public List<lu_ethnicity> myethnicitylist { get; set; }


      
    }
}
