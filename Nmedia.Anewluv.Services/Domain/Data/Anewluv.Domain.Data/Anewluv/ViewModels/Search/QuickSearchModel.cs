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

        //added stuff for paging
        [DataMember]
        public int? page { get; set; }
        [DataMember]
        public int? numberperpage { get; set; }
      
        [DataMember]  public int? myselectediamgenderid { get; set; }
        [DataMember]  public int? myselectedseekinggenderid { get; set; }
        [DataMember]  public int? myselectedfromage { get; set; }
        [DataMember]  public int? myselectedtoage { get; set; }


        [DataMember]  public string myselectedcountryname { get; set; }
        [DataMember]  public int? myselectedcountryid { get; set; }
        [DataMember]  public string myselectedpostalcode { get; set; }
        //added 10/17/20011 so we can toggle postalcode box similar to register
      [DataMember]   public bool? myselectedpostalcodestatus { get; set; }

        [DataMember]  public string myselectedcity { get; set; }
      [DataMember]   public Boolean? myselectedphotostatus { get; set; }
        [DataMember]  public string myselectedstateprovince { get; set; }
     [DataMember]    public double? myselectedmaxdistancefromme { get; set; }

        //gps data added 10/17/2011
     [DataMember]    public double? myselectedlongitude { get; set; }
     [DataMember]    public double? myselectedlatitude { get; set; }


        // private MembersRepository membersrepository;
        //[DataMember]  public int myselectedpagesize { get; set; }
        //[DataMember]  public int? myselectedcurrentpage { get; set; }
        //add flag to let us know that the data comes from geocoinding ?
        public bool? geocodeddata { get; set; }


    }
}
