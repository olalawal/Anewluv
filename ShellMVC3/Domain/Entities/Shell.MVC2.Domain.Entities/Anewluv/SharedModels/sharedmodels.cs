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
    public class age
    {
        public string agevalue { get; set; }
        public string ageindex { get; set; }
    }

    public class metricheight
    {
        public string heightvalue { get; set; }
        public string heightindex { get; set; }
    }
    
    public class distance
    {
        public int distancevalue;
        public int distanceindex;

    }


    public class city
    {
        public string cityvalue { get; set; }
        public string cityindex { get; set; }

    }

    public class country
    {
        public string countryvalue { get; set; }
        public string countryindex { get; set; }

    }

    public class citystateprovince
    {
        public string  stateprovinceindex { get; set; }
         public string stateprovince { get; set; }

    }


    public class postalcodes
    {
        public string postalcodevalue { get; set; }
        public string postalcodevalueindex { get; set; }

    }








}