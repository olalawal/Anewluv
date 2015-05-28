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
    public class AppearanceSettingsViewModel
    {
        [DataMember ]
        public AppearanceSettingsModel appearancesettings { get; set; }
        [DataMember]
        public AppearanceSearchSettingsModel appearancesearchsettings { get; set; }

      
         //[DataMember]
         //public int heightmax { get; set; }
         //[DataMember]
         //public int heightmin { get; set; }
         //[DataMember]
         //public List<lu_bodytype> bodytypeslist = new List<lu_bodytype>();
         //[DataMember]
         //public List<lu_ethnicity> ethnicitylist = new List<lu_ethnicity>();
         //[DataMember]
         //public List<lu_eyecolor> eyecolorlist = new List<lu_eyecolor>();
         //[DataMember]
         //public List<lu_haircolor> haircolorlist = new List<lu_haircolor>();
         //[DataMember]
         //public List<lu_hotfeature> hotfeaturelist = new List<lu_hotfeature>();

        //9-16-2011 need to KILL the parametered contructor for the postbacks not to be messed up
        //MVC viewstate needs a paramerless contrsctuor ,  see 
    }
}
