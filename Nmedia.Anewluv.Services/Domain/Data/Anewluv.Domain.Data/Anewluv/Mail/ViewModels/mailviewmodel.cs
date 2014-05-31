using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Nmedia.Infrastructure;
using System.Runtime.Serialization;
using Anewluv.Domain.Data.Helpers;

namespace Anewluv.Domain.Data.ViewModels
{
     [DataContract] public class mailviewmodel
    {
       


        //to do maybe change these to the object and use enums ?
        [DataMember]  public int senderstatus_id { get; set; }
        [DataMember]  public int recipientstatus_id { get; set; }

        public  bool? blockstatus { get; set; }
        //added to hide blocked messaged and banned
        [DataMember]  public int mailboxmessageid { get; set; }

        [DataMember]  public string body { get; set; }
        [DataMember]  public string subject { get; set; }
        [DataMember]  public int mailboxmessagefolder_id { get; set; }
        [DataMember]  public int mailboxfolder_id { get; set; }
        public DateTime age { get; set; }
        [DataMember]  public int? uniqueid { get; set; }
        [DataMember]  public string mailboxfoldername { get; set; }
        [DataMember]  public string mailboxfolderprofile_id { get; set; }
        [DataMember]  public string city { get; set; }
        [DataMember]  public string state { get; set; }
       [DataMember]    public DateTime? creationdate { get; set; }
        [DataMember]  public int sender_id { get; set; }
        [DataMember]  public string senderscreenname { get; set; }
        [DataMember]  public int recipient_id { get; set; }
        [DataMember]  public string recipientscreenname { get; set; }
       [DataMember]    public DateTime? readdate { get; set; }
       [DataMember]    public DateTime? replieddate { get; set; }


        //[DataMember]  public string read
        //{
        //    get
        //    {

        //        return (ConvertReadReplied(messageread));
        //    }
        //    set
        //    {
        //        ;
        //    }
        //}
        //[DataMember]  public string Replied
        //{
        //    get
        //    {

        //        return (ConvertReadReplied(messagereplied));
        //    }
        //    set
        //    {
        //        ;
        //    }
        //}
        [DataMember]  public int dob
        {
            get
            {
                return (DataFormatingExtensions.CalculateAge(age));
            }
            set
            {
                ;
            }
        }     


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
