
namespace Dating.Server.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using System.Runtime.Serialization;


    // The MetadataTypeAttribute identifies abuserMetadata as the class
    // that carries additional metadata for the abuser class.
       [MetadataTypeAttribute(typeof(abuser.abuserMetadata))]
    public partial class abuser
    {

        // This class allows you to attach custom attributes to properties
        // of the abuser class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class abuserMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private abuserMetadata()
            {
            }

           
        }
    }

    // The MetadataTypeAttribute identifies abusereportMetadata as the class
    // that carries additional metadata for the abusereport class.
          [MetadataTypeAttribute(typeof(abusereport.abusereportMetadata))]
    public partial class abusereport
    {

        // This class allows you to attach custom attributes to properties
        // of the abusereport class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class abusereportMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private abusereportMetadata()
            {
            }

            
        }
    }

    // The MetadataTypeAttribute identifies abusetypeMetadata as the class
    // that carries additional metadata for the abusetype class.
        [MetadataTypeAttribute(typeof(abusetype.abusetypeMetadata))]
    public partial class abusetype
    {

        // This class allows you to attach custom attributes to properties
        // of the abusetype class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class abusetypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private abusetypeMetadata()
            {
            }

            public EntityCollection<abusereport> abusereports { get; set; }

            public byte AbuseTypeID { get; set; }

            public string AbuseTypeName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaAppearance_BodytypesMetadata as the class
    // that carries additional metadata for the CriteriaAppearance_Bodytypes class.
        [MetadataTypeAttribute(typeof(CriteriaAppearance_Bodytypes.CriteriaAppearance_BodytypesMetadata))]
    public partial class CriteriaAppearance_Bodytypes
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaAppearance_Bodytypes class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaAppearance_BodytypesMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaAppearance_BodytypesMetadata()
            {
            }

            public string BodyTypeName { get; set; }

            public int BodyTypesID { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_BodyTypes> SearchSettings_BodyTypes { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaAppearance_EthnicityMetadata as the class
    // that carries additional metadata for the CriteriaAppearance_Ethnicity class.
          [MetadataTypeAttribute(typeof(CriteriaAppearance_Ethnicity.CriteriaAppearance_EthnicityMetadata))]
    public partial class CriteriaAppearance_Ethnicity
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaAppearance_Ethnicity class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaAppearance_EthnicityMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaAppearance_EthnicityMetadata()
            {
            }

            public int EthnicityID { get; set; }

            public string EthnicityName { get; set; }

            public EntityCollection<ProfileData_Ethnicity> ProfileData_Ethnicity { get; set; }

            public EntityCollection<SearchSettings_Ethnicity> SearchSettings_Ethnicity { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaAppearance_EyeColorMetadata as the class
    // that carries additional metadata for the CriteriaAppearance_EyeColor class.
          [MetadataTypeAttribute(typeof(CriteriaAppearance_EyeColor.CriteriaAppearance_EyeColorMetadata))]
    public partial class CriteriaAppearance_EyeColor
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaAppearance_EyeColor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaAppearance_EyeColorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaAppearance_EyeColorMetadata()
            {
            }

            public int EyeColorID { get; set; }

            public string EyeColorName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_EyeColor> SearchSettings_EyeColor { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaAppearance_HairColorMetadata as the class
    // that carries additional metadata for the CriteriaAppearance_HairColor class.
          [MetadataTypeAttribute(typeof(CriteriaAppearance_HairColor.CriteriaAppearance_HairColorMetadata))]
    public partial class CriteriaAppearance_HairColor
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaAppearance_HairColor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaAppearance_HairColorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaAppearance_HairColorMetadata()
            {
            }

            public int HairColorID { get; set; }

            public string HairColorName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_HairColor> SearchSettings_HairColor { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_DietMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_Diet class.
          [MetadataTypeAttribute(typeof(CriteriaCharacter_Diet.CriteriaCharacter_DietMetadata))]
    public partial class CriteriaCharacter_Diet
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_Diet class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_DietMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_DietMetadata()
            {
            }

            public int DietID { get; set; }

            public string DietName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_Diet> SearchSettings_Diet { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_DrinksMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_Drinks class.
          [MetadataTypeAttribute(typeof(CriteriaCharacter_Drinks.CriteriaCharacter_DrinksMetadata))]
    public partial class CriteriaCharacter_Drinks
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_Drinks class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_DrinksMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_DrinksMetadata()
            {
            }

            public int DrinksID { get; set; }

            public string DrinksName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_Drinks> SearchSettings_Drinks { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_ExerciseMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_Exercise class.
          [MetadataTypeAttribute(typeof(CriteriaCharacter_Exercise.CriteriaCharacter_ExerciseMetadata))]
    public partial class CriteriaCharacter_Exercise
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_Exercise class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_ExerciseMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_ExerciseMetadata()
            {
            }

            public int ExerciseID { get; set; }

            public string ExerciseName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_Exercise> SearchSettings_Exercise { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_HobbyMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_Hobby class.
       [MetadataTypeAttribute(typeof(CriteriaCharacter_Hobby.CriteriaCharacter_HobbyMetadata))]
    public partial class CriteriaCharacter_Hobby
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_Hobby class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_HobbyMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_HobbyMetadata()
            {
            }

            public int HobbyID { get; set; }

            public string HobbyName { get; set; }

            public EntityCollection<ProfileData_Hobby> ProfileData_Hobby { get; set; }

            public EntityCollection<SearchSettings_Hobby> SearchSettings_Hobby { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_HotFeatureMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_HotFeature class.
       [MetadataTypeAttribute(typeof(CriteriaCharacter_HotFeature.CriteriaCharacter_HotFeatureMetadata))]
    public partial class CriteriaCharacter_HotFeature
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_HotFeature class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_HotFeatureMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_HotFeatureMetadata()
            {
            }

            public int HotFeatureID { get; set; }

            public string HotFeatureName { get; set; }

            public EntityCollection<ProfileData_HotFeature> ProfileData_HotFeature { get; set; }

            public EntityCollection<SearchSettings_HotFeature> SearchSettings_HotFeature { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_HumorMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_Humor class.
       [MetadataTypeAttribute(typeof(CriteriaCharacter_Humor.CriteriaCharacter_HumorMetadata))]
    public partial class CriteriaCharacter_Humor
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_Humor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_HumorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_HumorMetadata()
            {
            }

            public int HumorID { get; set; }

            public string HumorName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_Humor> SearchSettings_Humor { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_PoliticalViewMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_PoliticalView class.
       [MetadataTypeAttribute(typeof(CriteriaCharacter_PoliticalView.CriteriaCharacter_PoliticalViewMetadata))]
    public partial class CriteriaCharacter_PoliticalView
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_PoliticalView class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_PoliticalViewMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_PoliticalViewMetadata()
            {
            }

            public int PoliticalViewID { get; set; }

            public string PoliticalViewName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_PoliticalView> SearchSettings_PoliticalView { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_ReligionMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_Religion class.
       [MetadataTypeAttribute(typeof(CriteriaCharacter_Religion.CriteriaCharacter_ReligionMetadata))]
    public partial class CriteriaCharacter_Religion
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_Religion class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_ReligionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_ReligionMetadata()
            {
            }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public int religionID { get; set; }

            public string religionName { get; set; }

            public EntityCollection<SearchSettings_Religion> SearchSettings_Religion { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_ReligiousAttendanceMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_ReligiousAttendance class.
       [MetadataTypeAttribute(typeof(CriteriaCharacter_ReligiousAttendance.CriteriaCharacter_ReligiousAttendanceMetadata))]
    public partial class CriteriaCharacter_ReligiousAttendance
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_ReligiousAttendance class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_ReligiousAttendanceMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_ReligiousAttendanceMetadata()
            {
            }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public int ReligiousAttendanceID { get; set; }

            public string ReligiousAttendanceName { get; set; }

            public EntityCollection<SearchSettings_ReligiousAttendance> SearchSettings_ReligiousAttendance { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_SignMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_Sign class.
       [MetadataTypeAttribute(typeof(CriteriaCharacter_Sign.CriteriaCharacter_SignMetadata))]
    public partial class CriteriaCharacter_Sign
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_Sign class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_SignMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_SignMetadata()
            {
            }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_Sign> SearchSettings_Sign { get; set; }

            public string SignBirthMonth { get; set; }

            public int SignID { get; set; }

            public string SignName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaCharacter_SmokesMetadata as the class
    // that carries additional metadata for the CriteriaCharacter_Smokes class.
       [MetadataTypeAttribute(typeof(CriteriaCharacter_Smokes.CriteriaCharacter_SmokesMetadata))]
    public partial class CriteriaCharacter_Smokes
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaCharacter_Smokes class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaCharacter_SmokesMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaCharacter_SmokesMetadata()
            {
            }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_Smokes> SearchSettings_Smokes { get; set; }

            public int SmokesID { get; set; }

            public string SmokesName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaLife_EducationLevelMetadata as the class
    // that carries additional metadata for the CriteriaLife_EducationLevel class.
       [MetadataTypeAttribute(typeof(CriteriaLife_EducationLevel.CriteriaLife_EducationLevelMetadata))]
    public partial class CriteriaLife_EducationLevel
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaLife_EducationLevel class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaLife_EducationLevelMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaLife_EducationLevelMetadata()
            {
            }

            public int EducationLevelID { get; set; }

            public string EducationLevelName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_EducationLevel> SearchSettings_EducationLevel { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaLife_EmploymentStatusMetadata as the class
    // that carries additional metadata for the CriteriaLife_EmploymentStatus class.
       [MetadataTypeAttribute(typeof(CriteriaLife_EmploymentStatus.CriteriaLife_EmploymentStatusMetadata))]
    public partial class CriteriaLife_EmploymentStatus
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaLife_EmploymentStatus class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaLife_EmploymentStatusMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaLife_EmploymentStatusMetadata()
            {
            }

            public int EmploymentSatusID { get; set; }

            public string EmploymentStatusName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_EmploymentStatus> SearchSettings_EmploymentStatus { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaLife_HaveKidsMetadata as the class
    // that carries additional metadata for the CriteriaLife_HaveKids class.
       [MetadataTypeAttribute(typeof(CriteriaLife_HaveKids.CriteriaLife_HaveKidsMetadata))]
    public partial class CriteriaLife_HaveKids
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaLife_HaveKids class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaLife_HaveKidsMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaLife_HaveKidsMetadata()
            {
            }

            public int HaveKidsId { get; set; }

            public string HaveKidsName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_HaveKids> SearchSettings_HaveKids { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaLife_IncomeLevelMetadata as the class
    // that carries additional metadata for the CriteriaLife_IncomeLevel class.
       [MetadataTypeAttribute(typeof(CriteriaLife_IncomeLevel.CriteriaLife_IncomeLevelMetadata))]
    public partial class CriteriaLife_IncomeLevel
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaLife_IncomeLevel class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaLife_IncomeLevelMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaLife_IncomeLevelMetadata()
            {
            }

            public int IncomeLevelID { get; set; }

            public string IncomeLevelName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_IncomeLevel> SearchSettings_IncomeLevel { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaLife_LivingSituationMetadata as the class
    // that carries additional metadata for the CriteriaLife_LivingSituation class.
       [MetadataTypeAttribute(typeof(CriteriaLife_LivingSituation.CriteriaLife_LivingSituationMetadata))]
    public partial class CriteriaLife_LivingSituation
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaLife_LivingSituation class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaLife_LivingSituationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaLife_LivingSituationMetadata()
            {
            }

            public int LivingSituationID { get; set; }

            public string LivingSituationName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_LivingStituation> SearchSettings_LivingStituation { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaLife_LookingForMetadata as the class
    // that carries additional metadata for the CriteriaLife_LookingFor class.
       [MetadataTypeAttribute(typeof(CriteriaLife_LookingFor.CriteriaLife_LookingForMetadata))]
    public partial class CriteriaLife_LookingFor
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaLife_LookingFor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaLife_LookingForMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaLife_LookingForMetadata()
            {
            }

            public int LookingForID { get; set; }

            public string LookingForName { get; set; }

            public EntityCollection<ProfileData_LookingFor> ProfileData_LookingFor { get; set; }

            public EntityCollection<SearchSettings_LookingFor> SearchSettings_LookingFor { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaLife_MaritalStatusMetadata as the class
    // that carries additional metadata for the CriteriaLife_MaritalStatus class.
       [MetadataTypeAttribute(typeof(CriteriaLife_MaritalStatus.CriteriaLife_MaritalStatusMetadata))]
    public partial class CriteriaLife_MaritalStatus
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaLife_MaritalStatus class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaLife_MaritalStatusMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaLife_MaritalStatusMetadata()
            {
            }

            public int MaritalStatusID { get; set; }

            public string MaritalStatusName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_MaritalStatus> SearchSettings_MaritalStatus { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaLife_ProfessionMetadata as the class
    // that carries additional metadata for the CriteriaLife_Profession class.
       [MetadataTypeAttribute(typeof(CriteriaLife_Profession.CriteriaLife_ProfessionMetadata))]
    public partial class CriteriaLife_Profession
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaLife_Profession class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaLife_ProfessionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaLife_ProfessionMetadata()
            {
            }

            public int ProfessionID { get; set; }

            public string ProfiessionName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_Profession> SearchSettings_Profession { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies CriteriaLife_WantsKidsMetadata as the class
    // that carries additional metadata for the CriteriaLife_WantsKids class.
       [MetadataTypeAttribute(typeof(CriteriaLife_WantsKids.CriteriaLife_WantsKidsMetadata))]
    public partial class CriteriaLife_WantsKids
    {

        // This class allows you to attach custom attributes to properties
        // of the CriteriaLife_WantsKids class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class CriteriaLife_WantsKidsMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CriteriaLife_WantsKidsMetadata()
            {
            }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<SearchSettings_WantKids> SearchSettings_WantKids { get; set; }

            public int WantsKidsID { get; set; }

            public string WantsKidsName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies databaseerrorMetadata as the class
    // that carries additional metadata for the databaseerror class.
       [MetadataTypeAttribute(typeof(databaseerror.databaseerrorMetadata))]
    public partial class databaseerror
    {

        // This class allows you to attach custom attributes to properties
        // of the databaseerror class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class databaseerrorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private databaseerrorMetadata()
            {
            }

            public byte[] ErrorDate { get; set; }

            public short ErrorID { get; set; }

            public string Exception { get; set; }

            public string PageName { get; set; }

            public string SqlStatement { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies emailerrorMetadata as the class
    // that carries additional metadata for the emailerror class.
       [MetadataTypeAttribute(typeof(emailerror.emailerrorMetadata))]
    public partial class emailerror
    {

        // This class allows you to attach custom attributes to properties
        // of the emailerror class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class emailerrorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private emailerrorMetadata()
            {
            }

            public string Body { get; set; }

            public int EmailErrorID { get; set; }

            public DateTime ErrorDate { get; set; }

            public string ExceptionError { get; set; }

            public string FromEmail { get; set; }

            public string Subject { get; set; }

            public string ToEmail { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies favoriteMetadata as the class
    // that carries additional metadata for the favorite class.
       [MetadataTypeAttribute(typeof(favorite.favoriteMetadata))]
    public partial class favorite
    {

        // This class allows you to attach custom attributes to properties
        // of the favorite class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class favoriteMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private favoriteMetadata()
            {
            }

            public Nullable<DateTime> FavoriteDate { get; set; }

            public string FavoriteID { get; set; }

            public Nullable<bool> FavoriteViewed { get; set; }

            public Nullable<DateTime> FavoriteViewedDate { get; set; }

            public Nullable<int> MutualFavorite { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public int RecordID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies FriendMetadata as the class
    // that carries additional metadata for the Friend class.
       [MetadataTypeAttribute(typeof(Friend.FriendMetadata))]
    public partial class Friend
    {

        // This class allows you to attach custom attributes to properties
        // of the Friend class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class FriendMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private FriendMetadata()
            {
            }

            public Nullable<DateTime> FriendDate { get; set; }

            public string FriendID { get; set; }

            public Nullable<bool> FriendViewed { get; set; }

            public Nullable<DateTime> FriendViewedDate { get; set; }

            public Nullable<int> MutualFriend { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public int RecordID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies genderMetadata as the class
    // that carries additional metadata for the gender class.
       [MetadataTypeAttribute(typeof(gender.genderMetadata))]
    public partial class gender
    {

        // This class allows you to attach custom attributes to properties
        // of the gender class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class genderMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private genderMetadata()
            {
            }

            public int GenderID { get; set; }

            public string GenderName { get; set; }

            public EntityCollection<ProfileData> ProfileDatas { get; set; }

            public EntityCollection<ProfileVisiblitySetting> ProfileVisiblitySettings { get; set; }

            public EntityCollection<SearchSettings_Genders> SearchSettings_Genders { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies HeightMetadata as the class
    // that carries additional metadata for the Height class.
       [MetadataTypeAttribute(typeof(Height.HeightMetadata))]
    public partial class Height
    {

        // This class allows you to attach custom attributes to properties
        // of the Height class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class HeightMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private HeightMetadata()
            {
            }

            public int HeightID { get; set; }

            public string HeightValue { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies HotlistMetadata as the class
    // that carries additional metadata for the Hotlist class.
       [MetadataTypeAttribute(typeof(Hotlist.HotlistMetadata))]
    public partial class Hotlist
    {

        // This class allows you to attach custom attributes to properties
        // of the Hotlist class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class HotlistMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private HotlistMetadata()
            {
            }

            public Nullable<DateTime> HotlistDate { get; set; }

            public string HotlistID { get; set; }

            public Nullable<bool> HotlistViewed { get; set; }

            public Nullable<DateTime> HotlistViewedDate { get; set; }

            public Nullable<int> MutualHotlist { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public int RecordID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies InterestMetadata as the class
    // that carries additional metadata for the Interest class.
       [MetadataTypeAttribute(typeof(Interest.InterestMetadata))]
    public partial class Interest
    {

        // This class allows you to attach custom attributes to properties
        // of the Interest class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class InterestMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private InterestMetadata()
            {
            }

            public Nullable<bool> DeletedByInterestID { get; set; }

            public Nullable<DateTime> DeletedByInterestIDDate { get; set; }

            public Nullable<bool> DeletedByProfileID { get; set; }

            public Nullable<DateTime> DeletedByProfileIDDate { get; set; }

            public Nullable<DateTime> InterestDate { get; set; }

            public string InterestID { get; set; }

            public Nullable<bool> IntrestViewed { get; set; }

            public Nullable<DateTime> IntrestViewedDate { get; set; }

            public int MutualInterest { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public int RecordID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies LikeMetadata as the class
    // that carries additional metadata for the Like class.
       [MetadataTypeAttribute(typeof(Like.LikeMetadata))]
    public partial class Like
    {

        // This class allows you to attach custom attributes to properties
        // of the Like class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class LikeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private LikeMetadata()
            {
            }

            public Nullable<bool> DeletedByLikeID { get; set; }

            public Nullable<DateTime> DeletedByLikeIDDate { get; set; }

            public Nullable<bool> DeletedByProfileID { get; set; }

            public Nullable<DateTime> DeletedByProfileIDDate { get; set; }

            public Nullable<DateTime> LikeDate { get; set; }

            public string LikeID { get; set; }

            public Nullable<bool> LikeViewed { get; set; }

            public Nullable<DateTime> LikeViewedDate { get; set; }

            public Nullable<int> MutuaLikes { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public int RecordID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MailboxblockMetadata as the class
    // that carries additional metadata for the Mailboxblock class.
       [MetadataTypeAttribute(typeof(Mailboxblock.MailboxblockMetadata))]
    public partial class Mailboxblock
    {

        // This class allows you to attach custom attributes to properties
        // of the Mailboxblock class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MailboxblockMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MailboxblockMetadata()
            {
            }

            public string BlockID { get; set; }

            public Nullable<bool> BlockRemoved { get; set; }

            public Nullable<DateTime> BlockRemovedDate { get; set; }

            public Nullable<DateTime> MailboxBlockDate { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public int RecordID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MailboxFolderMetadata as the class
    // that carries additional metadata for the MailboxFolder class.
       [MetadataTypeAttribute(typeof(MailboxFolder.MailboxFolderMetadata))]
    public partial class MailboxFolder
    {

        // This class allows you to attach custom attributes to properties
        // of the MailboxFolder class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MailboxFolderMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MailboxFolderMetadata()
            {
            }

            public Nullable<int> Active { get; set; }

            public int MailboxFolderID { get; set; }

            public MailboxFolderType MailboxFolderType { get; set; }

            public Nullable<int> MailboxFolderTypeID { get; set; }

            public string MailboxFolderTypeName { get; set; }

            public EntityCollection<MailboxMessagesFolder> MailboxMessagesFolders { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MailboxFolderTypeMetadata as the class
    // that carries additional metadata for the MailboxFolderType class.
       [MetadataTypeAttribute(typeof(MailboxFolderType.MailboxFolderTypeMetadata))]
    public partial class MailboxFolderType
    {

        // This class allows you to attach custom attributes to properties
        // of the MailboxFolderType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MailboxFolderTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MailboxFolderTypeMetadata()
            {
            }

            public string FolderType { get; set; }

            public EntityCollection<MailboxFolder> MailboxFolders { get; set; }

            public string MailboxFolderTypeDescription { get; set; }

            public int MailboxFolderTypeID { get; set; }

            public string MailboxFolderTypeName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MailboxMessageMetadata as the class
    // that carries additional metadata for the MailboxMessage class.
       [MetadataTypeAttribute(typeof(MailboxMessage.MailboxMessageMetadata))]
    public partial class MailboxMessage
    {

        // This class allows you to attach custom attributes to properties
        // of the MailboxMessage class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MailboxMessageMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MailboxMessageMetadata()
            {
            }

            public string Body { get; set; }

            public Nullable<DateTime> CreationDate { get; set; }

            public int MailboxMessageID { get; set; }

            public EntityCollection<MailboxMessagesFolder> MailboxMessagesFolders { get; set; }

            public string RecipientID { get; set; }

            public string SenderID { get; set; }

            public string Subject { get; set; }

            public Nullable<int> uniqueID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MailboxMessagesFolderMetadata as the class
    // that carries additional metadata for the MailboxMessagesFolder class.
       [MetadataTypeAttribute(typeof(MailboxMessagesFolder.MailboxMessagesFolderMetadata))]
    public partial class MailboxMessagesFolder
    {

        // This class allows you to attach custom attributes to properties
        // of the MailboxMessagesFolder class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MailboxMessagesFolderMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MailboxMessagesFolderMetadata()
            {
            }

            public MailboxFolder MailboxFolder { get; set; }

            public Nullable<int> MailboxFolderID { get; set; }

            public MailboxMessage MailboxMessage { get; set; }

            public Nullable<int> MailBoxMessageID { get; set; }

            public int MailboxMessagesFoldersID { get; set; }

            public Nullable<int> MessageDeleted { get; set; }

            public Nullable<int> MessageDraft { get; set; }

            public Nullable<int> MessageFlagged { get; set; }

            public Nullable<int> MessageRead { get; set; }

            public Nullable<int> MessageRecent { get; set; }

            public Nullable<int> MessageReplied { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies MembersInRoleMetadata as the class
    // that carries additional metadata for the MembersInRole class.
       [MetadataTypeAttribute(typeof(MembersInRole.MembersInRoleMetadata))]
    public partial class MembersInRole
    {

        // This class allows you to attach custom attributes to properties
        // of the MembersInRole class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class MembersInRoleMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private MembersInRoleMetadata()
            {
            }

            public Nullable<bool> Active { get; set; }

            public profile profile { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public int RecordID { get; set; }

            public Role Role { get; set; }

            public Nullable<DateTime> RoleExpireDate { get; set; }

            public Nullable<int> RoleID { get; set; }

            public Nullable<DateTime> RoleStartDate { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PhotoRejectionReasonMetadata as the class
    // that carries additional metadata for the PhotoRejectionReason class.
       [MetadataTypeAttribute(typeof(PhotoRejectionReason.PhotoRejectionReasonMetadata))]
    public partial class PhotoRejectionReason
    {

        // This class allows you to attach custom attributes to properties
        // of the PhotoRejectionReason class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PhotoRejectionReasonMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PhotoRejectionReasonMetadata()
            {
            }

            public string Description { get; set; }

            public int PhotoRejectionReasonID { get; set; }

            public EntityCollection<photo> photos { get; set; }

            public string UserMessage { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PhotoReviewStatuMetadata as the class
    // that carries additional metadata for the PhotoReviewStatu class.
       [MetadataTypeAttribute(typeof(PhotoReviewStatu.PhotoReviewStatuMetadata))]
    public partial class PhotoReviewStatu
    {

        // This class allows you to attach custom attributes to properties
        // of the PhotoReviewStatu class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PhotoReviewStatuMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PhotoReviewStatuMetadata()
            {
            }

            public int PhotoReviewStatusID { get; set; }

            public string PhotoReviewStatusValue { get; set; }

            public EntityCollection<photo> photos { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PhotoStatuMetadata as the class
    // that carries additional metadata for the PhotoStatu class.
       [MetadataTypeAttribute(typeof(PhotoStatu.PhotoStatuMetadata))]
    public partial class PhotoStatu
    {

        // This class allows you to attach custom attributes to properties
        // of the PhotoStatu class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PhotoStatuMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PhotoStatuMetadata()
            {
            }

            public string Description { get; set; }

            public EntityCollection<photo> photos { get; set; }

            public int PhotoStatusID { get; set; }

            public string PhotoStatusValue { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies PhotoTypeMetadata as the class
    // that carries additional metadata for the PhotoType class.
       [MetadataTypeAttribute(typeof(PhotoType.PhotoTypeMetadata))]
    public partial class PhotoType
    {

        // This class allows you to attach custom attributes to properties
        // of the PhotoType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class PhotoTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private PhotoTypeMetadata()
            {
            }

            public EntityCollection<photo> photos { get; set; }

            public string PhotoTypeDescription { get; set; }

            public int PhotoTypeID { get; set; }

            public string PhotoTypeName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProfileEmailUpdateFreqencyMetadata as the class
    // that carries additional metadata for the ProfileEmailUpdateFreqency class.
       [MetadataTypeAttribute(typeof(ProfileEmailUpdateFreqency.ProfileEmailUpdateFreqencyMetadata))]
    public partial class ProfileEmailUpdateFreqency
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileEmailUpdateFreqency class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileEmailUpdateFreqencyMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileEmailUpdateFreqencyMetadata()
            {
            }

            public Nullable<int> EmailUpdateFreqency { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public int RecordID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProfileGeoDataLoggerMetadata as the class
    // that carries additional metadata for the ProfileGeoDataLogger class.
       [MetadataTypeAttribute(typeof(ProfileGeoDataLogger.ProfileGeoDataLoggerMetadata))]
    public partial class ProfileGeoDataLogger
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileGeoDataLogger class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileGeoDataLoggerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileGeoDataLoggerMetadata()
            {
            }

            public string City { get; set; }

            public string Continent { get; set; }

            public string Country { get; set; }

            public string CountryName { get; set; }

            public Nullable<DateTime> CreationDate { get; set; }

            public int id { get; set; }

            public string IPaddress { get; set; }

            public Nullable<double> Lattitude { get; set; }

            public Nullable<double> Longitude { get; set; }

            public profile profile { get; set; }

            public string ProfileID { get; set; }

            public string RegionName { get; set; }

            public string SessionID { get; set; }

            public string UserAgent { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies profileOpenIDStoreMetadata as the class
    // that carries additional metadata for the profileOpenIDStore class.
       [MetadataTypeAttribute(typeof(profileOpenIDStore.profileOpenIDStoreMetadata))]
    public partial class profileOpenIDStore
    {

        // This class allows you to attach custom attributes to properties
        // of the profileOpenIDStore class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class profileOpenIDStoreMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private profileOpenIDStoreMetadata()
            {
            }

            public Nullable<bool> active { get; set; }

            public Nullable<DateTime> creationDate { get; set; }

            public int Id { get; set; }

            public string openidIdentifier { get; set; }

            public string openidProviderName { get; set; }

            public profile profile { get; set; }

            public string ProfileID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProfileRatingMetadata as the class
    // that carries additional metadata for the ProfileRating class.
       [MetadataTypeAttribute(typeof(ProfileRating.ProfileRatingMetadata))]
    public partial class ProfileRating
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileRating class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileRatingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileRatingMetadata()
            {
            }

            public double AverageRating { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public int ProfileRatingID { get; set; }

            public EntityCollection<ProfileRatingTracker> ProfileRatingTrackers { get; set; }

            public EntityCollection<ProfileRatingTracker> ProfileRatingTrackers1 { get; set; }

            public EntityCollection<ProfileRatingTracker> ProfileRatingTrackers2 { get; set; }

            public Rating Rating { get; set; }

            public Rating Rating1 { get; set; }

            public Nullable<int> RatingID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProfileRatingTrackerMetadata as the class
    // that carries additional metadata for the ProfileRatingTracker class.
       [MetadataTypeAttribute(typeof(ProfileRatingTracker.ProfileRatingTrackerMetadata))]
    public partial class ProfileRatingTracker
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileRatingTracker class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileRatingTrackerMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileRatingTrackerMetadata()
            {
            }

            public string ProfileID { get; set; }

            public ProfileRating ProfileRating { get; set; }

            public ProfileRating ProfileRating1 { get; set; }

            public ProfileRating ProfileRating2 { get; set; }

            public Nullable<int> ProfileRatingID { get; set; }

            public Nullable<DateTime> ProfileRatingTrackerDate { get; set; }

            public int ProfileRatingTrackerID { get; set; }

            public Nullable<double> RatingValue { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies profilestatusMetadata as the class
    // that carries additional metadata for the profilestatus class.
       [MetadataTypeAttribute(typeof(profilestatus.profilestatusMetadata))]
    public partial class profilestatus
    {

        // This class allows you to attach custom attributes to properties
        // of the profilestatus class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class profilestatusMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private profilestatusMetadata()
            {
            }

            public EntityCollection<profile> profiles { get; set; }

            public int ProfileStatusID { get; set; }

            public string ProfileStatusName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProfileViewMetadata as the class
    // that carries additional metadata for the ProfileView class.
       [MetadataTypeAttribute(typeof(ProfileView.ProfileViewMetadata))]
    public partial class ProfileView
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileView class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileViewMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileViewMetadata()
            {
            }

            public Nullable<bool> DeletedByProfileID { get; set; }

            public Nullable<DateTime> DeletedByProfileIDDate { get; set; }

            public Nullable<bool> DeletedByProfileViewerID { get; set; }

            public Nullable<DateTime> DeletedByProfileViewerIDDate { get; set; }

            public Nullable<int> MutualViews { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public Nullable<DateTime> ProfileViewDate { get; set; }

            public string ProfileViewerID { get; set; }

            public Nullable<bool> ProfileViewViewed { get; set; }

            public Nullable<DateTime> ProfileViewViewedDate { get; set; }

            public int RecordID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ProfileVisiblitySettingMetadata as the class
    // that carries additional metadata for the ProfileVisiblitySetting class.
       [MetadataTypeAttribute(typeof(ProfileVisiblitySetting.ProfileVisiblitySettingMetadata))]
    public partial class ProfileVisiblitySetting
    {

        // This class allows you to attach custom attributes to properties
        // of the ProfileVisiblitySetting class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ProfileVisiblitySettingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ProfileVisiblitySettingMetadata()
            {
            }

            public Nullable<int> AgeMaxVisibility { get; set; }

            public Nullable<int> AgeMinVisibility { get; set; }

            public Nullable<bool> ChatVisiblityToInterests { get; set; }

            public Nullable<bool> ChatVisiblityToLikes { get; set; }

            public Nullable<bool> ChatVisiblityToMatches { get; set; }

            public Nullable<bool> ChatVisiblityToPeeks { get; set; }

            public Nullable<bool> ChatVisiblityToSearch { get; set; }

            public Nullable<int> CountryID { get; set; }

            public gender gender { get; set; }

            public Nullable<int> GenderID { get; set; }

            public Nullable<DateTime> LastUpdateDate { get; set; }

            public Nullable<bool> MailChatRequest { get; set; }

            public Nullable<bool> MailIntrests { get; set; }

            public Nullable<bool> MailLikes { get; set; }

            public Nullable<bool> MailMatches { get; set; }

            public Nullable<bool> MailNews { get; set; }

            public Nullable<bool> MailPeeks { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public Nullable<bool> ProfileVisiblity { get; set; }

            public Nullable<bool> SaveOfflineChat { get; set; }

            public Nullable<bool> SteathPeeks { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RatingMetadata as the class
    // that carries additional metadata for the Rating class.
       [MetadataTypeAttribute(typeof(Rating.RatingMetadata))]
    public partial class Rating
    {

        // This class allows you to attach custom attributes to properties
        // of the Rating class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RatingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RatingMetadata()
            {
            }

            public EntityCollection<ProfileRating> ProfileRatings { get; set; }

            public EntityCollection<ProfileRating> ProfileRatings1 { get; set; }

            public string RatingDescription { get; set; }

            public int RatingID { get; set; }

            public Nullable<int> RatingMaxValue { get; set; }

            public Nullable<int> RatingWeight { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies RoleMetadata as the class
    // that carries additional metadata for the Role class.
       [MetadataTypeAttribute(typeof(Role.RoleMetadata))]
    public partial class Role
    {

        // This class allows you to attach custom attributes to properties
        // of the Role class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class RoleMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RoleMetadata()
            {
            }

            public EntityCollection<MembersInRole> MembersInRoles { get; set; }

            public int RoleID { get; set; }

            public string RoleName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_BodyTypesMetadata as the class
    // that carries additional metadata for the SearchSettings_BodyTypes class.
       [MetadataTypeAttribute(typeof(SearchSettings_BodyTypes.SearchSettings_BodyTypesMetadata))]
    public partial class SearchSettings_BodyTypes
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_BodyTypes class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_BodyTypesMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_BodyTypesMetadata()
            {
            }

            public Nullable<int> BodyTypesID { get; set; }

            public CriteriaAppearance_Bodytypes CriteriaAppearance_Bodytypes { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_BodyTypeID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_DietMetadata as the class
    // that carries additional metadata for the SearchSettings_Diet class.
       [MetadataTypeAttribute(typeof(SearchSettings_Diet.SearchSettings_DietMetadata))]
    public partial class SearchSettings_Diet
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Diet class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_DietMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_DietMetadata()
            {
            }

            public CriteriaCharacter_Diet CriteriaCharacter_Diet { get; set; }

            public Nullable<int> DietID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_DietID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_DrinksMetadata as the class
    // that carries additional metadata for the SearchSettings_Drinks class.
       [MetadataTypeAttribute(typeof(SearchSettings_Drinks.SearchSettings_DrinksMetadata))]
    public partial class SearchSettings_Drinks
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Drinks class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_DrinksMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_DrinksMetadata()
            {
            }

            public CriteriaCharacter_Drinks CriteriaCharacter_Drinks { get; set; }

            public Nullable<int> DrinksID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_DrinksID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_EducationLevelMetadata as the class
    // that carries additional metadata for the SearchSettings_EducationLevel class.
       [MetadataTypeAttribute(typeof(SearchSettings_EducationLevel.SearchSettings_EducationLevelMetadata))]
    public partial class SearchSettings_EducationLevel
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_EducationLevel class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_EducationLevelMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_EducationLevelMetadata()
            {
            }

            public CriteriaLife_EducationLevel CriteriaLife_EducationLevel { get; set; }

            public Nullable<int> EducationLevelID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_EducationLevelID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_EmploymentStatusMetadata as the class
    // that carries additional metadata for the SearchSettings_EmploymentStatus class.
       [MetadataTypeAttribute(typeof(SearchSettings_EmploymentStatus.SearchSettings_EmploymentStatusMetadata))]
    public partial class SearchSettings_EmploymentStatus
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_EmploymentStatus class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_EmploymentStatusMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_EmploymentStatusMetadata()
            {
            }

            public CriteriaLife_EmploymentStatus CriteriaLife_EmploymentStatus { get; set; }

            public Nullable<int> EmploymentStatusID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_EmploymentStatusID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_EthnicityMetadata as the class
    // that carries additional metadata for the SearchSettings_Ethnicity class.
       [MetadataTypeAttribute(typeof(SearchSettings_Ethnicity.SearchSettings_EthnicityMetadata))]
    public partial class SearchSettings_Ethnicity
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Ethnicity class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_EthnicityMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_EthnicityMetadata()
            {
            }

            public CriteriaAppearance_Ethnicity CriteriaAppearance_Ethnicity { get; set; }

            public Nullable<int> EthicityID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_EthnicitiesID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_ExerciseMetadata as the class
    // that carries additional metadata for the SearchSettings_Exercise class.
       [MetadataTypeAttribute(typeof(SearchSettings_Exercise.SearchSettings_ExerciseMetadata))]
    public partial class SearchSettings_Exercise
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Exercise class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_ExerciseMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_ExerciseMetadata()
            {
            }

            public CriteriaCharacter_Exercise CriteriaCharacter_Exercise { get; set; }

            public Nullable<int> ExerciseID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_ExerciseID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_EyeColorMetadata as the class
    // that carries additional metadata for the SearchSettings_EyeColor class.
       [MetadataTypeAttribute(typeof(SearchSettings_EyeColor.SearchSettings_EyeColorMetadata))]
    public partial class SearchSettings_EyeColor
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_EyeColor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_EyeColorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_EyeColorMetadata()
            {
            }

            public CriteriaAppearance_EyeColor CriteriaAppearance_EyeColor { get; set; }

            public Nullable<int> EyeColorID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_EyeColorID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_GendersMetadata as the class
    // that carries additional metadata for the SearchSettings_Genders class.
       [MetadataTypeAttribute(typeof(SearchSettings_Genders.SearchSettings_GendersMetadata))]
    public partial class SearchSettings_Genders
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Genders class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_GendersMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_GendersMetadata()
            {
            }

            public gender gender { get; set; }

            public Nullable<int> GenderID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_GenderID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_HairColorMetadata as the class
    // that carries additional metadata for the SearchSettings_HairColor class.
       [MetadataTypeAttribute(typeof(SearchSettings_HairColor.SearchSettings_HairColorMetadata))]
    public partial class SearchSettings_HairColor
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_HairColor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_HairColorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_HairColorMetadata()
            {
            }

            public CriteriaAppearance_HairColor CriteriaAppearance_HairColor { get; set; }

            public Nullable<int> HairColorID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_HairColorID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_HaveKidsMetadata as the class
    // that carries additional metadata for the SearchSettings_HaveKids class.
       [MetadataTypeAttribute(typeof(SearchSettings_HaveKids.SearchSettings_HaveKidsMetadata))]
    public partial class SearchSettings_HaveKids
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_HaveKids class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_HaveKidsMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_HaveKidsMetadata()
            {
            }

            public CriteriaLife_HaveKids CriteriaLife_HaveKids { get; set; }

            public Nullable<int> HaveKidsID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_HaveKidsID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_HobbyMetadata as the class
    // that carries additional metadata for the SearchSettings_Hobby class.
       [MetadataTypeAttribute(typeof(SearchSettings_Hobby.SearchSettings_HobbyMetadata))]
    public partial class SearchSettings_Hobby
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Hobby class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_HobbyMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_HobbyMetadata()
            {
            }

            public CriteriaCharacter_Hobby CriteriaCharacter_Hobby { get; set; }

            public Nullable<int> HobbyID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_HobbyID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_HotFeatureMetadata as the class
    // that carries additional metadata for the SearchSettings_HotFeature class.
       [MetadataTypeAttribute(typeof(SearchSettings_HotFeature.SearchSettings_HotFeatureMetadata))]
    public partial class SearchSettings_HotFeature
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_HotFeature class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_HotFeatureMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_HotFeatureMetadata()
            {
            }

            public CriteriaCharacter_HotFeature CriteriaCharacter_HotFeature { get; set; }

            public Nullable<int> HotFeatureID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_HotFeatureID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_HumorMetadata as the class
    // that carries additional metadata for the SearchSettings_Humor class.
       [MetadataTypeAttribute(typeof(SearchSettings_Humor.SearchSettings_HumorMetadata))]
    public partial class SearchSettings_Humor
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Humor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_HumorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_HumorMetadata()
            {
            }

            public CriteriaCharacter_Humor CriteriaCharacter_Humor { get; set; }

            public Nullable<int> HumorID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_HumorID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_IncomeLevelMetadata as the class
    // that carries additional metadata for the SearchSettings_IncomeLevel class.
       [MetadataTypeAttribute(typeof(SearchSettings_IncomeLevel.SearchSettings_IncomeLevelMetadata))]
    public partial class SearchSettings_IncomeLevel
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_IncomeLevel class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_IncomeLevelMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_IncomeLevelMetadata()
            {
            }

            public CriteriaLife_IncomeLevel CriteriaLife_IncomeLevel { get; set; }

            public Nullable<int> ImcomeLevelID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_IncomeLevelID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_LivingStituationMetadata as the class
    // that carries additional metadata for the SearchSettings_LivingStituation class.
       [MetadataTypeAttribute(typeof(SearchSettings_LivingStituation.SearchSettings_LivingStituationMetadata))]
    public partial class SearchSettings_LivingStituation
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_LivingStituation class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_LivingStituationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_LivingStituationMetadata()
            {
            }

            public CriteriaLife_LivingSituation CriteriaLife_LivingSituation { get; set; }

            public Nullable<int> LivingStituationID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_LivingStituationID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_LocationMetadata as the class
    // that carries additional metadata for the SearchSettings_Location class.
       [MetadataTypeAttribute(typeof(SearchSettings_Location.SearchSettings_LocationMetadata))]
    public partial class SearchSettings_Location
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Location class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_LocationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_LocationMetadata()
            {
            }

            public string City { get; set; }

            public Nullable<int> CountryID { get; set; }

            public string PostalCode { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_Location1 { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_LookingForMetadata as the class
    // that carries additional metadata for the SearchSettings_LookingFor class.
       [MetadataTypeAttribute(typeof(SearchSettings_LookingFor.SearchSettings_LookingForMetadata))]
    public partial class SearchSettings_LookingFor
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_LookingFor class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_LookingForMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_LookingForMetadata()
            {
            }

            public CriteriaLife_LookingFor CriteriaLife_LookingFor { get; set; }

            public Nullable<int> LookingForID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_LookingFor1 { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_MaritalStatusMetadata as the class
    // that carries additional metadata for the SearchSettings_MaritalStatus class.
       [MetadataTypeAttribute(typeof(SearchSettings_MaritalStatus.SearchSettings_MaritalStatusMetadata))]
    public partial class SearchSettings_MaritalStatus
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_MaritalStatus class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_MaritalStatusMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_MaritalStatusMetadata()
            {
            }

            public CriteriaLife_MaritalStatus CriteriaLife_MaritalStatus { get; set; }

            public Nullable<int> MaritalStatusID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_MaritalStatusID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_NigerianStateMetadata as the class
    // that carries additional metadata for the SearchSettings_NigerianState class.
       [MetadataTypeAttribute(typeof(SearchSettings_NigerianState.SearchSettings_NigerianStateMetadata))]
    public partial class SearchSettings_NigerianState
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_NigerianState class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_NigerianStateMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_NigerianStateMetadata()
            {
            }

            public Nullable<int> NigerianStateID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_NigerianStateID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_PoliticalViewMetadata as the class
    // that carries additional metadata for the SearchSettings_PoliticalView class.
       [MetadataTypeAttribute(typeof(SearchSettings_PoliticalView.SearchSettings_PoliticalViewMetadata))]
    public partial class SearchSettings_PoliticalView
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_PoliticalView class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_PoliticalViewMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_PoliticalViewMetadata()
            {
            }

            public CriteriaCharacter_PoliticalView CriteriaCharacter_PoliticalView { get; set; }

            public Nullable<int> PoliticalViewID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_PoliticalViewID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_ProfessionMetadata as the class
    // that carries additional metadata for the SearchSettings_Profession class.
       [MetadataTypeAttribute(typeof(SearchSettings_Profession.SearchSettings_ProfessionMetadata))]
    public partial class SearchSettings_Profession
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Profession class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_ProfessionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_ProfessionMetadata()
            {
            }

            public CriteriaLife_Profession CriteriaLife_Profession { get; set; }

            public Nullable<int> ProfessionID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_ProfessionID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_ReligionMetadata as the class
    // that carries additional metadata for the SearchSettings_Religion class.
       [MetadataTypeAttribute(typeof(SearchSettings_Religion.SearchSettings_ReligionMetadata))]
    public partial class SearchSettings_Religion
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Religion class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_ReligionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_ReligionMetadata()
            {
            }

            public CriteriaCharacter_Religion CriteriaCharacter_Religion { get; set; }

            public Nullable<int> ReligionID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_ReligionID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_ReligiousAttendanceMetadata as the class
    // that carries additional metadata for the SearchSettings_ReligiousAttendance class.
       [MetadataTypeAttribute(typeof(SearchSettings_ReligiousAttendance.SearchSettings_ReligiousAttendanceMetadata))]
    public partial class SearchSettings_ReligiousAttendance
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_ReligiousAttendance class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_ReligiousAttendanceMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_ReligiousAttendanceMetadata()
            {
            }

            public CriteriaCharacter_ReligiousAttendance CriteriaCharacter_ReligiousAttendance { get; set; }

            public Nullable<int> ReligiousAttendanceID { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public SearchSetting SearchSetting1 { get; set; }

            public int SearchSettings_ReligiousAttendanceID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_ShowMeMetadata as the class
    // that carries additional metadata for the SearchSettings_ShowMe class.
       [MetadataTypeAttribute(typeof(SearchSettings_ShowMe.SearchSettings_ShowMeMetadata))]
    public partial class SearchSettings_ShowMe
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_ShowMe class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_ShowMeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_ShowMeMetadata()
            {
            }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_ShowMeID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }

            public ShowMe ShowMe { get; set; }

            public Nullable<int> ShowMeID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_SignMetadata as the class
    // that carries additional metadata for the SearchSettings_Sign class.
       [MetadataTypeAttribute(typeof(SearchSettings_Sign.SearchSettings_SignMetadata))]
    public partial class SearchSettings_Sign
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Sign class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_SignMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_SignMetadata()
            {
            }

            public CriteriaCharacter_Sign CriteriaCharacter_Sign { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_SignID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }

            public Nullable<int> SignID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_SmokesMetadata as the class
    // that carries additional metadata for the SearchSettings_Smokes class.
       [MetadataTypeAttribute(typeof(SearchSettings_Smokes.SearchSettings_SmokesMetadata))]
    public partial class SearchSettings_Smokes
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Smokes class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_SmokesMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_SmokesMetadata()
            {
            }

            public CriteriaCharacter_Smokes CriteriaCharacter_Smokes { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_SmokesID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }

            public Nullable<int> SmokesID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_SortByTypeMetadata as the class
    // that carries additional metadata for the SearchSettings_SortByType class.
       [MetadataTypeAttribute(typeof(SearchSettings_SortByType.SearchSettings_SortByTypeMetadata))]
    public partial class SearchSettings_SortByType
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_SortByType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_SortByTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_SortByTypeMetadata()
            {
            }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_SortByType1 { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }

            public SortByType SortByType { get; set; }

            public Nullable<int> SortByTypeID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_TribeMetadata as the class
    // that carries additional metadata for the SearchSettings_Tribe class.
       [MetadataTypeAttribute(typeof(SearchSettings_Tribe.SearchSettings_TribeMetadata))]
    public partial class SearchSettings_Tribe
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_Tribe class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_TribeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_TribeMetadata()
            {
            }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_TribeID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }

            public Nullable<int> TribeID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SearchSettings_WantKidsMetadata as the class
    // that carries additional metadata for the SearchSettings_WantKids class.
       [MetadataTypeAttribute(typeof(SearchSettings_WantKids.SearchSettings_WantKidsMetadata))]
    public partial class SearchSettings_WantKids
    {

        // This class allows you to attach custom attributes to properties
        // of the SearchSettings_WantKids class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SearchSettings_WantKidsMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SearchSettings_WantKidsMetadata()
            {
            }

            public CriteriaLife_WantsKids CriteriaLife_WantsKids { get; set; }

            public SearchSetting SearchSetting { get; set; }

            public int SearchSettings_WantKidsID { get; set; }

            public Nullable<int> SearchSettingsID { get; set; }

            public Nullable<int> WantKidsID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SecurityQuestionMetadata as the class
    // that carries additional metadata for the SecurityQuestion class.
       [MetadataTypeAttribute(typeof(SecurityQuestion.SecurityQuestionMetadata))]
    public partial class SecurityQuestion
    {

        // This class allows you to attach custom attributes to properties
        // of the SecurityQuestion class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SecurityQuestionMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SecurityQuestionMetadata()
            {
            }

            public EntityCollection<profile> profiles { get; set; }

            public byte SecurityQuestionID { get; set; }

            public string SecurityQuestionText { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies ShowMeMetadata as the class
    // that carries additional metadata for the ShowMe class.
       [MetadataTypeAttribute(typeof(ShowMe.ShowMeMetadata))]
    public partial class ShowMe
    {

        // This class allows you to attach custom attributes to properties
        // of the ShowMe class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ShowMeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ShowMeMetadata()
            {
            }

            public EntityCollection<SearchSettings_ShowMe> SearchSettings_ShowMe { get; set; }

            public int ShowMeID { get; set; }

            public string ShowMeName { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SortByTypeMetadata as the class
    // that carries additional metadata for the SortByType class.
       [MetadataTypeAttribute(typeof(SortByType.SortByTypeMetadata))]
    public partial class SortByType
    {

        // This class allows you to attach custom attributes to properties
        // of the SortByType class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SortByTypeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SortByTypeMetadata()
            {
            }

            public EntityCollection<SearchSettings_SortByType> SearchSettings_SortByType { get; set; }

            public string SortByName { get; set; }

            public int SortByTypeID { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SystemPageSettingMetadata as the class
    // that carries additional metadata for the SystemPageSetting class.
       [MetadataTypeAttribute(typeof(SystemPageSetting.SystemPageSettingMetadata))]
    public partial class SystemPageSetting
    {

        // This class allows you to attach custom attributes to properties
        // of the SystemPageSetting class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SystemPageSettingMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SystemPageSettingMetadata()
            {
            }

            public string BodyCssSyleName { get; set; }

            public string Description { get; set; }

            public Nullable<int> HitCount { get; set; }

            public Nullable<bool> IsMasterPage { get; set; }

            public string Path { get; set; }

            public string Titile { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies User_LogtimeMetadata as the class
    // that carries additional metadata for the User_Logtime class.
       [MetadataTypeAttribute(typeof(User_Logtime.User_LogtimeMetadata))]
    public partial class User_Logtime
    {

        // This class allows you to attach custom attributes to properties
        // of the User_Logtime class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class User_LogtimeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private User_LogtimeMetadata()
            {
            }

            public int Id { get; set; }

            public Nullable<DateTime> LoginTime { get; set; }

            public Nullable<DateTime> LogoutTime { get; set; }

            public byte Offline { get; set; }

            public ProfileData ProfileData { get; set; }

            public string ProfileID { get; set; }

            public string SessionID { get; set; }
        }
    }
}
