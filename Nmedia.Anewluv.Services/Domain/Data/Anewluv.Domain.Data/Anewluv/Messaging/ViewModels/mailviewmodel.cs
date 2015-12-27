using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Nmedia.Infrastructure;
using System.Runtime.Serialization;


namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class MailViewModel
    {      
        //base data
        [DataMember]
        public int senderprofile_id { get; set; }
        [DataMember]
        public string senderscreenname { get; set; }
        [DataMember]
        public int recipientprofile_id { get; set; }
        [DataMember]
        public string recipientscreenname { get; set; }

        //to do maybe change these to the object and use enums ?
        [DataMember]  public int senderstatus_id { get; set; }
        [DataMember]  public int recipientstatus_id { get; set; }  
        [DataMember]  public string body { get; set; }
        [DataMember]  public string subject { get; set; }
      
         //Composite key
        [DataMember]
        public int mailboxmessageid { get; set; }
        [DataMember]  public int mailboxfolder_id { get; set; }
        [DataMember]
        public string mailboxfoldername { get; set; }
      

        [DataMember]
        public int recipientage { get; set; }
        [DataMember]
        public int senderage { get; set; }

        [DataMember]  public int? uniqueid { get; set; }
      
        [DataMember]  public string sendercity { get; set; }
        [DataMember]  public string senderstate { get; set; }
        [DataMember]
        public string sendercountry { get; set; }

        [DataMember]
        public string recipientcity { get; set; }
        [DataMember]
        public string recipientstate { get; set; }
        [DataMember]
        public string recipientcountry { get; set; }
       
       [DataMember]   public DateTime? creationdate { get; set; }
        
       [DataMember]   public DateTime? readdate { get; set; }
        [DataMember]  public bool? read { get; set; }
       [DataMember]   public DateTime? replieddate { get; set; }

       [DataMember] 
       public bool? readbyrecipient { get; set; }
      

        
        [DataMember]
        public int sendergenderid { get; set; }
        [DataMember]
       public PhotoViewModel sendergalleryphoto { get; set; }
     //  [DataMember]
      // public bool senderhasgalleryphoto { get; set; }
        [DataMember]
        public int recipientgenderid { get; set; }
       [DataMember]
       public PhotoViewModel recipientgalleryphoto { get; set; }
      // [DataMember]
      // public bool recipienthasgalleryphoto { get; set; }


       


        public static string ConvertReadReplied(int? ReadReplied)
        {


            string readreplied;

            if (ReadReplied == 1)
            {
                readreplied = "Yes";
            }
            else
            {
                readreplied = "No";
            }
            return readreplied;
        }
        public static int CalculateAge(DateTime BirthDate)
        {
            DateTime Now = DateTime.Today;

            int years = Now.Year - BirthDate.Year;

            if (Now.Month < BirthDate.Month || (Now.Month == BirthDate.Month && Now.Day < BirthDate.Day))
            {
                --years;
            }

            return years;
        }

    }
}
