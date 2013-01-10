using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Globalization;
using System.Linq;
using System.Web;


using Shell.MVC2.Domain.Entities;

//add ref to postal data




using System.Collections;


using System.Runtime.Serialization;





namespace Shell.MVC2.Domain.Entities.Anewluv
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
    
    [DataContract]
    public class distance
    {
        [DataMember]
        public int distancevalue;
        [DataMember]
        public int distanceindex;
        [DataMember]
        public bool selected { get; set; }
    }

    [DataContract]
    public class city
    {
        [DataMember]
        public string cityvalue { get; set; }
        [DataMember]
        public string cityindex { get; set; }
        [DataMember]
        public bool selected { get; set; }
    }

    [DataContract]
    public class country
    {
        [DataMember]
        public string countryvalue { get; set; }
        [DataMember]
        public string countryindex { get; set; }
        [DataMember]
        public bool selected { get; set; }

    }

    [DataContract]
    public class citystateprovince
    {
        [DataMember]
        public string stateprovinceindex { get; set; }
        [DataMember]
        public string stateprovince { get; set; }
        [DataMember]
        public bool selected { get; set; }
    }


    [DataContract]
    public class postalcodes
    {
        [DataMember]
        public string postalcodevalue { get; set; }
        [DataMember]
        public string postalcodevalueindex { get; set; }
        [DataMember]
        public bool selected { get; set; }
    }








}