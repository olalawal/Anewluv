using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    public class mailviewmodel
    {
       


        //to do maybe change these to the object and use enums ?
        public int senderstatus_id { get; set; }
        public int recipientstatus_id { get; set; }

        public  bool? blockstatus { get; set; }
        //added to hide blocked messaged and banned
        public int mailboxmessageid { get; set; }

        public string body { get; set; }
        public string subject { get; set; }
        public int mailboxmessagefolder_id { get; set; }
        public int mailboxfolder_id { get; set; }
        public DateTime age { get; set; }
        public int? uniqueid { get; set; }
        public string mailboxfoldername { get; set; }
        public string mailboxfolderprofile_id { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public DateTime? creationdate { get; set; }
        public int sender_id { get; set; }
        public string senderscreenname { get; set; }
        public int recipient_id { get; set; }
        public string recipientscreenname { get; set; }
        public DateTime? readdate { get; set; }
        public DateTime? replieddate { get; set; }


        //public string read
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
        //public string Replied
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
        public int dob
        {
            get
            {
                return (Shell.MVC2.Infrastructure.Extensions.CalculateAge(age));
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
