using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Nmedia.Infrastructure.Domain.Data.Notification
{
    [DataContract]
    public class EmailModel
    {

     [DataMember] public int messagetypeid { get; set; }  //TO DO use the template maybe to determined what kind of email it is   
     [DataMember]
     public int addresstypeid { get; set; }    
     [DataMember] public int templateid { get; set; }

    [DataMember]
    public int applicationid { get; set; }

    [DataMember]
    public string applicationname { get; set; }
    [DataMember] public string emailaddress { get; set; }
     //[DataMember] public addresstypeenum addresstypefrom { get; set; }
     // [DataMember] public string from { get; set; }  //only used in contact us

     [DataMember]
     public string to { get; set; }
     [DataMember]
     public string from { get; set; }  //only used in contact us   
     [DataMember]
     public string subject { get; set; }
     [DataMember]
     public string body { get; set; }    
     [DataMember]
     public string Messages { get; set; }  //Personal user updates strored here
     [DataMember]
     public string username { get; set; }  //Personal user updates strored here
     [DataMember]
     public string targetscreenname { get; set; }  //Personal user updates strored here
     [DataMember]
     public string screenname { get; set; }  //Personal user updates strored here
     [DataMember]
     public string userlogon { get; set; }  //Personal user updates strored here
     [DataMember]
     public string passwordtoken { get; set; }  //Personal user updates strored here
     [DataMember]
     public string activationcode { get; set; }  //Personal user updates strored here
     [DataMember]
     public string openidprovidername { get; set; }  //tells the admins what provider i,e facebook, IG etc was used to validte this user
                                                     //[DataMember]
                                                     // public int? addresstypeid { get; set; }  //Personal user updates strored here

     //new feilds to make the templates generic to any dating site

    [DataMember]
    public string logourl   { get; set; }
    [DataMember]
    public string  emaildeliverystring  { get; set; }
    [DataMember]
    public string photourl  { get; set; }
    [DataMember]
    public string  bottombulleturl  { get; set; }


    }
}
