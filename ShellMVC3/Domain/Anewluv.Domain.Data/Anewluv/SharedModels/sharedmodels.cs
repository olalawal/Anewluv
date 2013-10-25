using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Globalization;
using System.Linq;
using System.Web;



//add ref to postal data




using System.Collections;


using System.Runtime.Serialization;





namespace Anewluv.Domain.Data
{







    //ComplexTypes
    [DataContract]
    public class age
    {
        [DataMember]public string agevalue { get; set; }
        [DataMember]
        public string ageindex { get; set; }
        [DataMember]
        public bool selected { get; set; }
    }

    [DataContract]
    public class metricheight
    {
        [DataMember]
        public string heightvalue { get; set; }
        [DataMember]
        public string heightindex { get; set; }
        [DataMember]
        public bool selected { get; set; }
    }
    
    //[DataContract]
    //public class distance
    //{
    //    [DataMember]
    //    public int distancevalue;
    //    [DataMember]
    //    public int distanceindex;
    //    [DataMember]
    //    public bool selected { get; set; }
    //}

    //[DataContract]
    //public class city
    //{
    //    [DataMember]
    //    public string cityvalue { get; set; }    
    //    [DataMember]
    //    public bool selected { get; set; }
    //}

    //[DataContract]
    //public class country
    //{
    //    [DataMember]
    //    public string name { get; set; }
    //    [DataMember]
    //    public string id { get; set; }
    //    [DataMember]
    //    public bool selected { get; set; }

    //}

    //[DataContract]
    //public class countrypostalcode
    //{
    //     [DataMember]
    //    public string code { get; set; }
    //     [DataMember]
    //     public string region { get; set; }
    //     [DataMember]
    //     public int? customregionid { get; set; }
    //     [DataMember]
    //     public string id { get; set; }
    //     [DataMember]
    //     public string name { get; set; }
    //     [DataMember]
    //     public bool? haspostalcode { get; set; }

    //}


    //[DataContract]
    //public class citystateprovince
    //{
     
    //    [DataMember]
    //    public string  citystateprovincevalue { get; set; }    
    //    [DataMember]
    //    public bool selected { get; set; }
    //}


    //[DataContract]
    //public class postalcode
    //{
    //    [DataMember]
    //    public string postalcodevalue { get; set; }     
    //    [DataMember]
    //    public bool selected { get; set; }
    //}

    // [DataContract]
    //public class gpsdata
    //{
    //    [DataMember]
    //    public string postalcode { get; set; }
    //    [DataMember]
    //    public double? longitude { get; set; }
    //    [DataMember]
    //    public double? lattitude { get; set; }
    //    [DataMember]     
    //    public string stateprovince { get; set; }      
    //    [DataMember]
    //    public bool selected { get; set; }
    //}

      







}