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
        public string agevalue { get; set; }
        public string ageindex { get; set; }
    }

    public class MetricHeights
    {
        public string heightvalue { get; set; }
        public string heightindex { get; set; }
    }





    public class Distance
    {
        public int distancevalue;
        public int distanceindex;

    }

    public class CityStateProvince
    {
        public string city { get; set; }
        public string stateprovince { get; set; }

    }


    //10-4-2012 jainrain added to model

    //defines a container for agreagating models. also data and type conversions , needs to be heavliy optimized though
    // [DataContract]
    [DataContract]
    [Serializable]
    public class MembersViewModel
    {

        //
        rpxprofile rpxmodel { get; set; }

        //ADD A STATE VIEWMODEL AS WELL  to determine weather the member is in 
        //activation, logeed on or registration mode , that way we can use the same partial views and 
        //toggle actions acordingly
        //public MemberCurrentStatusViewModel MyCurrentStatus;

        //4-27-2012 added profile visiblity mode
        public visiblitysetting  profilevisiblity { get; set; }

        //added 9/05/2011
        //determines if the member has been shown thier daily status message i.e if neede
        public bool profilestatusmessageshown { get; set; }

        //id of this particually item
        public string membersviewmodelid { get; set; }

        // public photo  Photos { get; set; }

        //profileData should enumarate the entire model 
        [DataMember]
        public profile profile { get; set; }

        [DataMember]
        public profiledata profiledata { get; set; }

        [DataMember]
        public quicksearchmodel myquicksearch { get; set; }

        public RegisterModel register { get; set; }

        [DataMember]
        public AccountModel account { get; set; }

        [DataMember]
        //used for uploading photos , just temporary
        public List<photo> myphotos { get; set; }

        //10-26-2011 added as another temp storrange
        public PhotoEditViewModel myeditprofilephotosviewmodel { get; set; }
        //10-26-2011 another tmeporary model 
        public List<MemberSearchViewModel> mycurrentsearchlist { get; set; }

        //counters
        //mail
        public string mymailcount { get; set; }
        public string whomailedme { get; set; }
        public string whomailedmenewcount { get; set; }
        //interests
        public string myintrestcount { get; set; }
        public string whoisinterestedinmecount { get; set; }
        public string whoisinterestedinmenewcount { get; set; }
        //peeks
        public string mypeekscount { get; set; }
        public string whopeekededatmecount { get; set; }
        public string whopeekedatmenewcount { get; set; }
        //likes
        public string wholikesmecount { get; set; }
        public string wholikesmenewcount { get; set; }
        public string whoilikecount { get; set; }
        //blocks



        public string myblockcount { get; set; }


        //male would be default looking for female etc
        public HashSet<int> lookingforgendersid { get; set; }

        //gender ID when they do not have anything in search settings fallback 
        public int lookingforgenderid { get; set; }
        public string lookingforagefrom { get; set; }
        public string lookingforageto { get; set; }


        //get genders collection and selection
        public int mygenderid { get; set; }
        public string myage { get; set; }
        //double vlaue represents the max distance i am looking for
        public double maxdistancefromme { get; set; }
        //Zip Code for search, pull out of postal data contex
        //replace zipcode with city on the membershome page list using thier country and postal code to find cities near them 


        public string mypostalcode { get; set; }
        public bool mypostalcodestatus { get; set; }  //this flag determines if the user is from a coutnry with postal codes , if they are let
        //them search by postal code otherwise hide it

        //geo data data
        public string mycountryname { get; set; }
        public string mycitystateprovince { get; set; }
        public string mycountrycode { get; set; }
        public string mycontinentcode { get; set; }
        public string myregionname { get; set; }
        public string mycity { get; set; }
        public string mylongitude { get; set; }
        public string mylatitude { get; set; }
        public string myuseragent { get; set; }
        public string myipaddress { get; set; }
        //convery the ID feild country to the actuall country name if it is needed         
        public int mycountryid { get; set; }



        //5/24/2012 add new feilds for mapping to JSON model


        // public List<string> Cities;


    }



    #endregion



}