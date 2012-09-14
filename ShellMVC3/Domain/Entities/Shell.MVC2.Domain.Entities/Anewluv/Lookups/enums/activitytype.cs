using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
 

    /// <summary>
    /// This is an enumeration type for the log severity types we track
    /// this is parsed into database values when the context is created
    /// </summary>
    [DataContract]
    public enum activitytypeEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("login")] 
        [EnumMember]
        login,
        [Description("logout")]
        [EnumMember]
        logout,
        [Description("updateprofile")]
        [EnumMember]
        updateprofile,
        [Description("advancedsearch")]
        [EnumMember]
        search,
        [Description("quicksearch")]
        [EnumMember]
        quicksearch,
        [Description("sent like")]
        [EnumMember]
        sentlike,
        [Description("sent interest")]
        [EnumMember]
        sentinterest,
        [Description("sent friend request")]
        [EnumMember]
        sentfriend,
        [Description("sent block")]
        [EnumMember]
        sentblock,
        [Description("sent abusereport")]
        [EnumMember]
        sentabusereport,
        [Description("sent mail")]
        [EnumMember]
        sentmail,
        [Description("viewed mail")]
        [EnumMember]
        viewedmail,
        [Description("sent chat request")]
        [EnumMember]
        sentchatrequest,
        [Description("sent chat message")]
        [EnumMember]
        sendchatmessage,
        [Description("viewed chat message")]
        [EnumMember]
        viewedchatmessage,
        [Description("updated search settings")]
        [EnumMember]
        updatedsearchsettings,
        [Description("uploaded photo")]
        [EnumMember]
        updloadedphoto,
       [Description("deleted photo")]
        [EnumMember]
        deletedphoto,
        [Description("created photo album")]
        [EnumMember]
        createdphotoalbum,
        [Description("viewed other member profile")]
        viewedmemberprofile,
         [Description("deleted profile")]
       deletedprofile,
         [Description("from old database structure")]
         fromolddatabasestructure
         
    }


  
}

