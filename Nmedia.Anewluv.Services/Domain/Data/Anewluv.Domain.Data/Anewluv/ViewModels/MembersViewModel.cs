using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Globalization;
using System.Linq;
using System.Web;



//add ref to postal data




using System.Collections;


using System.Runtime.Serialization;





namespace Anewluv.Domain.Data.ViewModels
{




    //10-4-2012 jainrain added to model

    //defines a container for agreagating models. also data and type conversions , needs to be heavliy optimized though
    // [DataContract]
    [DataContract]
    [Serializable]
    public class MembersViewModel
    {

        //Added session ID
        [DataMember]
        public string sessionid { get; set; }
        [DataMember]
        public int profile_id { get; set; } 

        //added to hold RPX /OpenID data
        //[DataMember ]
        public rpxprofile rpxmodel { get; set; }

        //ADD A STATE VIEWMODEL AS WELL  to determine weather the member is in 
        //activation, logeed on or registration mode , that way we can use the same partial views and 
        //toggle actions acordingly
        //public MemberCurrentStatusViewModel MyCurrentStatus;

        //4-27-2012 added profile visiblity mode
        // [DataMember]
        public visiblitysetting  profilevisiblity { get; set; }

        //added 9/05/2011
        //determines if the member has been shown thier daily status message i.e if neede
        //[DataMember]
        public bool profilestatusmessageshown { get; set; }

        //id of this particually item
        //  [DataMember]
        public string membersviewmodelid { get; set; }

        // public photo  Photos { get; set; }

        //profileData should enumarate the entire model 
        //[DataMember]
        public profile profile { get; set; }

       // [IgnoreDataMember]
        public profiledata profiledata { get; set; }

        //[DataMember]
        public quicksearchmodel myquicksearch { get; set; }

        // [DataMember]
        public registermodel register { get; set; }

       //[DataMember]
        public AccountModel account { get; set; }

  
        //used for uploading photos , just temporary
     //   [DataMember ]
        public List<PhotoModel> myphotos { get; set; }

       // [IgnoreDataMember]
         public photoconversion galleryphoto { get; set; }

        //10-26-2011 added as another temp storrange
       // [DataMember ]
        public PhotoEditViewModel myeditprofilephotosviewmodel { get; set; }

        //1-12-2012 olawal added this to handle uploaded photos 
       //  [DataMember]
        public List<PhotoUploadModel> recentlyuploadedphotos { get; set; }

        //10-26-2011 another tmeporary model 
       // [DataMember]
        public List<MemberSearchViewModel> mycurrentsearchlist { get; set; }

        //counters
        //mail
          [DataMember]
        public string mymailcount { get; set; }
         [DataMember]
        public string whomailedme { get; set; }
         [DataMember]
        public string whomailedmenewcount { get; set; }
        //interests
         [DataMember]
        public string myintrestcount { get; set; }
         [DataMember]
        public string whoisinterestedinmecount { get; set; }
         [DataMember]
        public string whoisinterestedinmenewcount { get; set; }
        //peeks
         [DataMember]
        public string mypeekscount { get; set; }
         [DataMember]
        public string whopeekededatmecount { get; set; }
         [DataMember]
        public string whopeekedatmenewcount { get; set; }
        //likes
         [DataMember]
        public string wholikesmecount { get; set; }
         [DataMember]
        public string wholikesmenewcount { get; set; }
         [DataMember]
        public string whoilikecount { get; set; }
        //blocks

         [DataMember]
        public string myblockcount { get; set; }

        //male would be default looking for female etc
        // [DataMember]
        public HashSet<int> lookingforgendersid { get; set; }

        //gender ID when they do not have anything in search settings fallback 
       //  [DataMember]
       // public int lookingforgenderid { get; set; }
         [DataMember]
        public string lookingforagefrom { get; set; }
         [DataMember]
        public string lookingforageto { get; set; }

        //get genders collection and selection
        [DataMember]
        public int mygenderid { get; set; }
         [DataMember]
        public string myage { get; set; }
        //double vlaue represents the max distance i am looking for
         [DataMember]
        public double maxdistancefromme { get; set; }
        //Zip Code for search, pull out of postal data contex
        //replace zipcode with city on the membershome page list using thier country and postal code to find cities near them 
        [DataMember]
        public string mypostalcode { get; set; }
         [DataMember]
        public bool mypostalcodestatus { get; set; }  //this flag determines if the user is from a coutnry with postal codes , if they are let
        //them search by postal code otherwise hide it

        //geo data data
         [DataMember]
        public string mycountryname { get; set; }
         [DataMember]
        public string mycitystateprovince { get; set; }
         [DataMember]
        public string mycountrycode { get; set; }
         [DataMember]
        public string mycontinentcode { get; set; }
         [DataMember]
        public string myregionname { get; set; }
         [DataMember]
        public string mycity { get; set; }
         [DataMember]
        public string mylongitude { get; set; }
         [DataMember]
        public string mylatitude { get; set; }
         [DataMember]
        public string myuseragent { get; set; }
         [DataMember]
        public string myipaddress { get; set; }
        //convery the ID feild country to the actuall country name if it is needed         
         [DataMember]
        public int mycountryid { get; set; }



        //5/24/2012 add new feilds for mapping to JSON model


        // public List<string> Cities;


    }





}