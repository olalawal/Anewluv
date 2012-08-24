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
    public enum professionEnum : int
    {
        [Description("Any")] 
        [EnumMember]
        Any ,
        [Description("Administrative / Clerical")]
        [EnumMember]
        AdministrativeClerical,
        [Description("Legal / Lawyer / Attorney")]
        [EnumMember]
        LegalLawyerAttorney,
        [Description("Health / Medical")]
        [EnumMember]
        HealthMedical,
        [Description("Media /  Entertainment")]
        [EnumMember]
        MediaEntertainment,
        [Description("Sales /  Marketing")]
        [EnumMember]
        SalesMarketing,
        [Description("Financial / Banking Real Estate")]
        [EnumMember]
        FinancialBankingRealEstate,
        [Description("Retail / Food Service")]
        [EnumMember]
        RetailFoodService,
        [Description("Transportation / Hospitality / Travel")]
        [EnumMember]
        TransportationHospitalityTravel,
        [Description("Executive / Management")]
        [EnumMember]
        ExecutiveManagement,
        [Description("Construction / Labor")]
        [EnumMember]
        ConstructionLabor,
        [Description("Government / Politics / Military")]
        [EnumMember]
        GovernmentPoliticsMilitary,        
        [Description("Entrepreneur")]
        [EnumMember]
        Entrepreneur,
        [Description("Education / Academia")]
        [EnumMember]
        EducationAcademia,
        [Description("Engineering / Technical / Science")]
        [EnumMember]
        EngineeringTechnicalScience,
        [Description(" Manufacturing / Distribution")]
        [EnumMember]
        ManufacturingDistribution,
        [Description("Writer / Artist / Musician")]
        [EnumMember]
        WriterArtistMusician,
        [Description("Other")]
        [EnumMember]
        Other,
        [Description("Computer Industry")]
        [EnumMember]
        ComputerIndustry
   
    }


  
}

