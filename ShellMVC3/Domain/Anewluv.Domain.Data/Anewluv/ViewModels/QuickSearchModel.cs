using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Anewluv.Domain.Data.ViewModels
{
    [Serializable]
    [DataContract]
    public class quicksearchmodel
    {

        //current values selected from User Interface , we bind them to the user 
        //viewmodel for now unless we run into memory issues and effecinery, then they will expire with the view
          [DataMember]
        public int myselectediamgenderid { get; set; }

        public int myselectedseekinggenderid { get; set; }
        public int myselectedfromage { get; set; }
        public int myselectedtoage { get; set; }


        public string myselectedcountryname { get; set; }
        public int myselectedcountryid { get; set; }
        public string myselectedpostalcode { get; set; }
        //added 10/17/20011 so we can toggle postalcode box similar to register
        public Boolean myselectedpostalcodestatus { get; set; }

        public string myselectedcity { get; set; }
        public Boolean myselectedphotostatus { get; set; }
        public string myselectedcitystateprovince { get; set; }
        public double? myselectedmaxdistancefromme { get; set; }

        //gps data added 10/17/2011
        public double? myselectedlongitude { get; set; }
        public double? myselectedlatitude { get; set; }


        // private MembersRepository membersrepository;
        public int myselectedpagesize { get; set; }
        public int? myselectedcurrentpage { get; set; }
        //add flag to let us know that the data comes from geocoinding ?
        public bool geocodeddata { get; set; }

    }
}
