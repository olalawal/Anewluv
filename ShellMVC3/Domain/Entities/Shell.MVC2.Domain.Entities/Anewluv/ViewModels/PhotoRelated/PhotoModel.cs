using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Shell.MVC2.Domain.Entities.Anewluv.ViewModels
{
    //10-29-2011 moved the  edit photo stuff here but it is not edit only, probably should be in photomodel

    [DataContract]
    [Serializable]
    public class PhotoModel
    {
        [DataMember]
        public Guid photoid { get; set; }
         [DataMember]
        public byte[] photo { get; set; }
        [DataMember]
        public string screenname { get; set; }
        [DataMember]
        public int profileid { get; set; }
        [DataMember]
        public lu_photoformat photoformat { get; set; }
         [DataMember]
        public DateTime? creationdate { get; set; }
        [DataMember]
        public string imagecaption { get; set; }
        [DataMember]
        public string imagename { get; set; }
        //[DataMember]
       // public int photostatusid { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public long? orginalsize { get; set; }
        [DataMember]
        public long? convertedsize { get; set; }
     //   [DataMember]
      //  public lu_photostatus photostatus { get; set; }
       // [DataMember]
       // public  lu_photoapprovalstatus approvalstatus { get; set; }
        //[DataMember]
       // public  lu_photoimagetype imagetype { get; set; }
       

    }
}