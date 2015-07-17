using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Anewluv.Domain.Data
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into Initial Catalog= values when the context is created
    /// </summary>
    [DataContract]
    public enum activitytypeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("login")] 
        [EnumMember]
        login=1,
        [Description("logout")]
        [EnumMember]
        logout=2,
        [Description("updateprofile")]
        [EnumMember]
        updateprofile=3,
        [Description("advancedsearch created")]  //not logging right now
        [EnumMember]
        advancedsearchcreated = 4,
        [Description("advancedsearch updated")]  //not logging right now
        [EnumMember]
        advancedsearchupdated = 28,
        [Description("quicksearch")]
        [EnumMember]
        quicksearch=5,
        [Description("sent like")]
        [EnumMember]
        sentlike=6,
        [Description("sent interest")]
        [EnumMember]
        sentinterest=7,
        [Description("sent friend request")]
        [EnumMember]
        sentfriend=8,
        [Description("sent block")]
        [EnumMember]
        sentblock=9,
        [Description("sent abusereport")]
        [EnumMember]
        sentabusereport=10,
        [Description("sent mail")]
        [EnumMember]
        sentmail=11,
        [Description("viewed mail")]
        [EnumMember]
        viewedmail=12,
        [Description("sent chat request")]
        [EnumMember]
        sentchatrequest=13,
        [Description("sent chat message")]
        [EnumMember]
        sendchatmessage=14,
        [Description("viewed chat message")]
        [EnumMember]
        viewedchatmessage=15,
        [Description("updated search settings")]
        [EnumMember]
        updatedsearchsettings=16,
        [Description("uploaded photo")]
        [EnumMember]
        updloadedphoto=17,
       [Description("deleted photo")]
        [EnumMember]
        deletedphoto=18,
        [Description("created photo album")]
        [EnumMember]
        createdphotoalbum=19,
        [Description("viewed other member profile")]
         viewedmemberprofile=20,
         [Description("deleted profile")]
         deletedprofile=21,
         [Description("from old datatbase structure")]
         fromoldInitialdatabasestructure=22,
         [Description("updated gender settings (must upgrade to change)")]
         updatedgendersettings =23,
         [Description("change screen name (must upgrade to change)")]
         changescreenname =24,
         [Description("change birth date")]
         changebirthdate=25,
         [Description("create advanced search")]
         createadvancedsearch=26  ,
         [Description("updated profile match settings")]
         updatedprofilematchsettings = 27  


    }


  
}

