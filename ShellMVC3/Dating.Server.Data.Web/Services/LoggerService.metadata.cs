
namespace Dating.Server.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies ELMAH_ErrorMetadata as the class
    // that carries additional metadata for the ELMAH_Error class.
    [MetadataTypeAttribute(typeof(ELMAH_Error.ELMAH_ErrorMetadata))]
    public partial class ELMAH_Error
    {

        // This class allows you to attach custom attributes to properties
        // of the ELMAH_Error class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class ELMAH_ErrorMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private ELMAH_ErrorMetadata()
            {
            }

            public string AllXml { get; set; }

            public string Application { get; set; }

            public Guid ErrorId { get; set; }

            public string Host { get; set; }

            public string Message { get; set; }

            public int Sequence { get; set; }

            public string Source { get; set; }

            public int StatusCode { get; set; }

            public DateTime TimeUtc { get; set; }

            public string Type { get; set; }

            public string User { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies GeoDataLogMetadata as the class
    // that carries additional metadata for the GeoDataLog class.
    [MetadataTypeAttribute(typeof(GeoDataLog.GeoDataLogMetadata))]
    public partial class GeoDataLog
    {

        // This class allows you to attach custom attributes to properties
        // of the GeoDataLog class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class GeoDataLogMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private GeoDataLogMetadata()
            {
            }

            public string City { get; set; }

            public string Continent { get; set; }

            public string CountryCode { get; set; }

            public string CountryName { get; set; }

            public Nullable<DateTime> CreationDate { get; set; }

            public int id { get; set; }

            public string IPaddress { get; set; }

            public Nullable<double> Lattitude { get; set; }

            public Nullable<double> Longitude { get; set; }

            public string RegionName { get; set; }

            public string SessionID { get; set; }

            public string UserAgent { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies SpamMessagesLogMetadata as the class
    // that carries additional metadata for the SpamMessagesLog class.
    [MetadataTypeAttribute(typeof(SpamMessagesLog.SpamMessagesLogMetadata))]
    public partial class SpamMessagesLog
    {

        // This class allows you to attach custom attributes to properties
        // of the SpamMessagesLog class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class SpamMessagesLogMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private SpamMessagesLogMetadata()
            {
            }

            public string BlockedBy { get; set; }

            public Nullable<DateTime> Creationdate { get; set; }

            public int id { get; set; }

            public string MessageBody { get; set; }

            public string Reason { get; set; }

            public string RecipientID { get; set; }

            public string SenderId { get; set; }

            public string SessionID { get; set; }

            public string Subject { get; set; }
        }
    }
}
