using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;

using Anewluv.Domain.Data;



//for RIA services contrib
//using RiaServicesContrib.Mvc.Services;


using System.Runtime.Serialization;
//using Anewluv.Domain.Data.Validation;


namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class DateValidateModel
    {
        [DataMember] 
        public string IsoDate { get; set; }

       

    }




}