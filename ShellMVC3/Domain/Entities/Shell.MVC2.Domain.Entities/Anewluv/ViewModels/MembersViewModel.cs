using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Globalization;
using System.Linq;
using System.Web;


using Shell.MVC2.Domain.Entities;

//add ref to postal data




using System.Collections;


using System.Runtime.Serialization;





namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{


    #region Models




    //ComplexTypes
    public class Age
    {
        public string AgeValue { get; set; }
        public string AgeIndex { get; set; }
    }

    public class MetricHeights
    {
        public string HeightValue { get; set; }
        public string HeightIndex { get; set; }
    }





    public class Distance
    {
        public int DistanceValue;
        public int DistanceIndex;

    }

    public class CityStateProvince
    {
        public string City { get; set; }
        public string StateProvince { get; set; }

    }




    //defines a container for agreagating models. also data and type conversions , needs to be heavliy optimized though
    // [DataContract]
    [DataContract]
    [Serializable]
    public class MembersViewModel
    {



        //ADD A STATE VIEWMODEL AS WELL  to determine weather the member is in 
        //activation, logeed on or registration mode , that way we can use the same partial views and 
        //toggle actions acordingly
        //public MemberCurrentStatusViewModel MyCurrentStatus;

        //4-27-2012 added profile visiblity mode
        public visiblitysetting  ProfileVisiblity { get; set; }

        //added 9/05/2011
        //determines if the member has been shown thier daily status message i.e if neede
        public bool ProfileStatusMessageShown { get; set; }

        //id of this particually item
        public string MembersViewModelID { get; set; }

        // public photo  Photos { get; set; }

        //profileData should enumarate the entire model 
        [DataMember]
        public profile Profile { get; set; }

        [DataMember]
        public profiledata profiledata { get; set; }

        [DataMember]
        public QuickSearchModel MyQuickSearch { get; set; }

        public RegisterModel Register { get; set; }

        [DataMember]
        public AccountModel Account { get; set; }

        [DataMember]
        //used for uploading photos , just temporary
        public List<photo> MyPhotos { get; set; }

        //10-26-2011 added as another temp storrange
        public PhotoEditViewModel MyEditProfilePhotosViewModel { get; set; }
        //10-26-2011 another tmeporary model 
        public List<MemberSearchViewModel> MyCurrentSearchList { get; set; }

        //counters
        //mail
        public string MyMailCount { get; set; }
        public string WhoMailedMe { get; set; }
        public string WhoMailedMeNewCount { get; set; }
        //interests
        public string MyIntrestCount { get; set; }
        public string WhoisInterestedInMeCount { get; set; }
        public string WhoisInterestedInMeNewCount { get; set; }
        //peeks
        public string MyPeeksCount { get; set; }
        public string WhopeekededatMeCount { get; set; }
        public string WhopeekedatMeNewCount { get; set; }
        //likes
        public string WholikesMeCount { get; set; }
        public string WhoLikesMeNewCount { get; set; }
        public string WhoIlikeCount { get; set; }
        //blocks



        public string MyBlockCount { get; set; }


        //male would be default looking for female etc
        public HashSet<int> LookingForGendersID { get; set; }

        //gender ID when they do not have anything in search settings fallback 
        public int LookingForGenderId { get; set; }
        public string LookingForAgeFrom { get; set; }
        public string LookingForAgeTo { get; set; }


        //get genders collection and selection
        public int MyGenderID { get; set; }
        public string MyAge { get; set; }
        //double vlaue represents the max distance i am looking for
        public double MaxDistanceFromMe { get; set; }
        //Zip Code for search, pull out of postal data contex
        //replace zipcode with city on the membershome page list using thier country and postal code to find cities near them 


        public string MyPostalCode { get; set; }
        public bool MyPostalCodeStatus { get; set; }  //this flag determines if the user is from a coutnry with postal codes , if they are let
        //them search by postal code otherwise hide it

        //geo data data
        public string MyCountryName { get; set; }
        public string MyCityStateProvince { get; set; }
        public string MyCountryCode { get; set; }
        public string MyContinentCode { get; set; }
        public string MyRegionName { get; set; }
        public string MyCity { get; set; }
        public string MyLongitude { get; set; }
        public string MyLatitude { get; set; }
        public string MyUserAgent { get; set; }
        public string MyIpAddress { get; set; }
        //convery the ID feild country to the actuall country name if it is needed         
        public int MyCountryID { get; set; }



        //5/24/2012 add new feilds for mapping to JSON model


        // public List<string> Cities;


    }



    #endregion



}