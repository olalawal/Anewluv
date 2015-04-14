using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Anewluv.Domain.Data;


using Anewluv.Domain.Data.ViewModels;



using System.Runtime.Serialization;
using Anewluv.Domain.Data.Helpers;

namespace Anewluv.Domain.Data.ViewModels
{
    [Serializable]
    [DataContract]
    public class FullProfileViewModel
    {
        public FullProfileViewModel()
        {
            //USe theis when we figure out how to do it using out 
          //  this.ProfileCriteria = new ProfileCriteriaModel();
          //  ProfileCriteria = new ProfileCriteriaModel();
        }
         
        //base profile stuff
        [DataMember]
        public string screenname { get; set; }
        [DataMember]
        public DateTime? birthdate { get; set; } //birthdate
        [DataMember]
        public string joindate { get; set; } //display value of last logged in    
        [DataMember]
        public string lastloggedonstring { get; set; } //display value of last logged in          
        [DataMember]
        public bool? online { get; set; } //not filled in yet      
        [DataMember]
        public string heightbyculture { get; set; }
        [DataMember]
        public string mycatchyintroline { get; set; }
        [DataMember]
        public string aboutme { get; set; }
        [DataMember]
        public int genderid { get; set; }
        [DataMember]
        public string gender { get; set; }  

        //TO DO split into separate calls
        //profile details stuff:
       // [DataMember]
       // public ProfileCriteriaModel ProfileCriteria { get; set; } 
        [DataMember]
        public MemberToMemberActionsModel MemeberToMemberActions { get; set; } 

        
        //Geo Stuff
        [DataMember]
        public double? distancefromme { get; set; }
        [DataMember]
        public string stateprovince { get; set; }
        [DataMember]
        public string postalcode { get; set; }
        [DataMember]
        public int? countryid { get; set; }
        [DataMember]
        public string countryname { get; set; }
        [DataMember]
        public string city { get; set; }
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


        //Photo Stuff
        [DataMember]
       // public PhotoViewModel profilephotos { get; set; }
      //  [DataMember]
        public Boolean? ProfileVisibility { get; set; }
        [DataMember]
        public bool hasgalleryphoto { get; set; }
        [DataMember]
        public PhotoViewModel galleryphoto { get; set; }
        //added code to to keep track of currently selected photo ID so we can swicth the large phot
        //i.e the zoomed one
        public Guid ViewingPhotoID { get; set; }
       
              
 
            

       
      
        //4-28-1added normalized screen name that replaces special chars , for now it just trims out spaces, update it to do more later
        //TO DO this is how we synch up the screen name with what is in chat since screen names can have spaces and UserNames and Identity can't

        public string screennamenormalized
        {
            get
            {
                // return "test";

                return (DataFormatingExtensions.NormalizeScreenName(this.screenname));

            }
        }

        public string screennametrimmed
        {
            get
            {
                // return "test";

                return (DataFormatingExtensions.ReduceStringLength(this.screenname, 10));

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
                return (DataFormatingExtensions.CalculateAge(birthdate.GetValueOrDefault()));

            }
            set
            {
                ;
            }
        }   

        //addded handling to chop the value i.e trim it using the extentionmethod automatically on a get
        [DataMember]
        public string MyCatchyIntroLineQuickSearch
        {
            get
            {
                if (mycatchyintroline != "")
                {
                    return ("Hi There");
                }
                else if (this.mycatchyintroline != null)
                {
                    return (DataFormatingExtensions.Chop(this.mycatchyintroline, 20));
                }

                return "Hi There";
            }
            set
            {
                ;
            }
        }





        
   
      
        //added collection of pictures
        
    }

  

}