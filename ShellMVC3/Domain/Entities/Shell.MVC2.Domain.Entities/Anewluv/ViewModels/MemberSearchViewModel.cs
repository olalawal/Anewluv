using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Shell.MVC2.Domain.Entities.Anewluv;

using Shell.MVC2.Infrastructure;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{

    [Serializable]
  [DataContract]
    public class MemberSearchViewModel
    {

        //added stuff for photo VMS
           [DataMember]
        public PhotoEditViewModel profilephotos { get; set; }


        //added stuff from visiblity table
           [DataMember]
        public Boolean? ProfileVisibility { get; set; }




        //added date feilds for times of events
           [DataMember]
        public DateTime? interestdate { get; set; }
           [DataMember]
        public DateTime? peekdate { get; set; }
           [DataMember]
        public DateTime? likedate { get; set; }
           [DataMember]
        public DateTime? blockdate { get; set; }

           [DataMember]
        public DateTime? birthdate { get; set; }
           [DataMember]
        public DateTime? creationdate { get; set; }
           [DataMember]
        public DateTime? lastlogindate { get; set; }
           [DataMember]
        public int? lookingforid { get; set; }
         
        public profile profile { get; set; }
         
        public profiledata profiledata { get; set; }
        //added a conttructor with profile ID for building this item since its one per page now
         [DataMember]
        public searchsetting  perfectmatchsettings { get; set; }
       // public EditProfilePhotosViewModel ProfilePhotos { get; set; }
        // properties       
         [DataMember]
        public int id { get; set; }
          [DataMember]
        public string stateprovince { get; set; }
          [DataMember]
        public string postalcode { get; set; }
          [DataMember]
        public int? countryid { get; set; }
        //public DateTime? lastloggedontime { get; set; }
         [DataMember]
        public string lastloggedonstring { get; set; } //display value of last logged in          
         [DataMember]
        public bool? online { get; set; } //not filled in yet          
          [DataMember]
        public bool hasgalleryphoto { get; set; }
          [DataMember]
        public photoconversion galleryphoto { get; set; }
          [DataMember]
        public string countryname { get; set; }
        public string city { get; set; }
        //added screen name since we dont pass profile anymore
          [DataMember]
        public string screenname { get; set; }
        public string mycatchyintroline { get; set; }
           [DataMember]
        public string aboutme { get; set; }
           [DataMember]
        public int? genderid { get; set; }
           [DataMember]
        public double? distancefromme { get; set; }
        //stuff I am looking for , loaded from search settings or 
        //intially loaded by system , which just picks defaults i.e
        //male would be default looking for female etc         
           [DataMember]
        public string lookingforagefrom { get; set; }
          [DataMember]
        public string lookingForageto { get; set; }
        //double vlaue represents the max distance i am looking for        

        //Zip Code for search, pull out of postal data contex
        //replace zipcode with city on the membershome page list using thier country and postal code to find cities near them          
         [DataMember]
        public bool postalcodestatus { get; set; }  //this flag determines if the user is from a coutnry with postal codes , if they are let
        //them search by postal code otherwise hide it
        //gps data    
          [DataMember]
        public double? longitude { get; set; }
          [DataMember]
        public double? latitude { get; set; }
        //convery the ID feild country to the actuall country name if it is needed 
        //addded handling to chop the value i.e trim it using the extentionmethod automatically on a get         

        //4-28-1added normalized screen name that replaces special chars , for now it just trims out spaces, update it to do more later
        //TO DO this is how we synch up the screen name with what is in chat since screen names can have spaces and UserNames and Identity can't
        
        public string screennamenormalized
        {
            get
            {
                // return "test";

                return (Extensions.NormalizeScreenName(this.screenname ));

            }
        }
        
        public string screennametrimmed
        {
            get
            {
                // return "test";

                return (Extensions.ReduceStringLength(this.screenname , 10));

            }
            set
            {
                ;
            }
        }
           [DataMember]
        public int? age
        {
            get
            {
                //return 21;
                return (Extensions.CalculateAge(birthdate.GetValueOrDefault()  ));

            }
            set
            {
                ;
            }
        }   

        //added code to to keep track of currently selected photo ID so we can swicth the large phot
        //i.e the zoomed one
        public Guid ViewingPhotoID { get; set; }

        //addded handling to chop the value i.e trim it using the extentionmethod automatically on a get
        [DataMember]
        public string MyCatchyIntroLineQuickSearch
        {
            get
            {
                if (profiledata != null)
                {
                    return (Extensions.Chop(profiledata.mycatchyintroLine , 20));
                }
                else if (this.mycatchyintroline   != null)
                {
                    return (Extensions.Chop(this.mycatchyintroline , 20));
                }

                return "Hi There";
            }
            set
            {
                ;
            }
        }


    




    }
}
