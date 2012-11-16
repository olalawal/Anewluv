using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    //10-29-2011 moved the  edit photo stuff here but it is not edit only, probably should be in photomodel
   
   [DataContract ]
    [Serializable ]
    public class PhotoEditModel
    {
        [DataMember]
        public Guid photoid { get; set; }
        [DataMember]
        public string screenname { get; set; }
        [DataMember]
        public int profileid { get; set; }
        [DataMember]
        public bool  aproved { get; set; }
        [DataMember]
        public string profileimagetype { get; set; }
        [DataMember]
        public bool checkedprimary { get; set; }
        [DataMember]
        public bool checkedphoto { get; set; }      
        [DataMember]
        public DateTime? photodate { get; set; }
        [DataMember]
        public string imagecaption { get; set; }
        [DataMember]
        public int photostatusid { get; set; }

        public bool boolimagetype(string profileimagetype)
        {
            bool check = false;

            if (profileimagetype == "Gallery")//"NoStatus vs Gallery"
            {
                check = true;
            }

            return check;
        }

    }  
}
