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

    public class metricheights
    {
        public string heightvalue { get; set; }
        public string heightindex { get; set; }
    }
    
    public class distance
    {
        public int distancevalue;
        public int distanceindex;

    }

    public class citystateprovince
    {
        public string city { get; set; }
        public string stateprovince { get; set; }

    }







}