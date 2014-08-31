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
    public enum photostatusEnum : int
    {
        [Description("NotSet")]
        [EnumMember]
        NotSet,
        [Description("Gallery")] 
        [EnumMember]
        Gallery,
        [Description("nostatus")]
        [EnumMember]
        Nostatus,
        [Description("private")]
        [EnumMember]
        PrivatePhoto,
        [Description("deletedbyuser")]
        [EnumMember]
        deletedbyuser,
        [Description("deletedbyadmin")]
        [EnumMember]
        deletedbyadmin
        
    }

//    1	Gallery	This is the main photo that is seen, set it to gallery in code
//2	nostatus 	this means the photo is just a standerd photo not viewable unless the peron sets it to gallery
//3	private	if it is proivate only friends can see it
//4	deletedbyuser	NULL
//5	deletedbyadmin	NULL
//NULL	NULL	NULL

  
}

