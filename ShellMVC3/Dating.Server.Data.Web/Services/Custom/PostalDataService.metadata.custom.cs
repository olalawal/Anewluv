

namespace Dating.Server.Data.Models
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using System.Runtime.Serialization;



    #region "Custom MetatData for Stored procedures that do not return valid entity types"

    //MetaData For REturning the list of cities
    //<MetadataTypeAttribute(GetType(CityList.CityListMetadata))> _
    //Partial Public Class CityList
    //    Friend NotInheritable Class CityListMetadata
    //        <Key()> _
    //        Public Property City As String
    //    End Class
    //End Class


    //The MetadataTypeAttribute identifies CityList as the class Clistlist
    // that carries additional metadata for theClistlist class.
    //*** note that the KEY atrribute is required and forcers the data to be returned based on that PK
    //*** ie it is a unqite contraint. Which means if you have multiple values with the same PK only some will be retunred
    //** update 8/19/2010 applied t he key atttribute on both feilds to make sure all records are returned as the composite key will
    //** always be somewhat unque
      [MetadataTypeAttribute(typeof(CityList.CityListMetadata))]
    public partial class CityList
    {

        internal sealed class CityListMetadata
        {

            private CityListMetadata()
                : base()
            {
            }


            [Key()]
            [DataMember]
            public string City
            {
                get { return m_City; }
                set { m_City = value; }
            }

            private string m_City;


            [Key()]
            [DataMember]
            public string State_Province
            {
                get { return m_State_Province; }
                set { m_State_Province = value; }
            }

            private string m_State_Province;
            //Public Property Longitude() As String
            //    Get
            //        Return m_Longitude
            //    End Get
            //    Set(ByVal value As String)
            //        m_Longitude = value
            //    End Set
            //End Property
            //Private m_Longitude As String

            //Public Property Latitude() As String
            //    Get
            //        Return m_Latitude
            //    End Get
            //    Set(ByVal value As String)
            //        m_Latitude = value
            //    End Set
            //End Property
            //Private m_Latitude As String

            //Public Property PostalCode() As String
            //    Get
            //        Return m_PostalCode
            //    End Get
            //    Set(ByVal value As String)
            //        m_PostalCode = value
            //    End Set
            //End Property
            //Private m_PostalCode As String

        }





    }


      [MetadataTypeAttribute(typeof(PostalCodeList.PostalCodeListMetadata))]
    public partial class PostalCodeList
    {

        internal sealed class PostalCodeListMetadata
        {

            private PostalCodeListMetadata()
                : base()
            {
            }




            [Key()]
            [DataMember]
            public string PostalCode
            {
                get { return m_PostalCode; }
                set { m_PostalCode = value; }
            }

      
            private string m_PostalCode;




        }





    }


      [MetadataTypeAttribute(typeof(GpsData.GpsDataMetadata))]
    public partial class GpsData
    {

        internal sealed class GpsDataMetadata
        {

            private GpsDataMetadata()
                : base()
            {
            }


            //       Public Property RecordID() As Integer
            //    Get
            //        Return m_RecordID
            //    End Get
            //    Set(ByVal value As Integer)
            //        m_RecordID = value
            //    End Set
            //End Property
            //Private m_RecordID As Integer

            [Key()]          
            public float  longitude
            {
                get { return m_Longitude; }
                set { m_Longitude = value; }
            }

            private float m_Longitude;

        
            public float  lattitude
            {
                get { return m_lattitude; }
                set { m_lattitude = value; }
            }
            private float  m_lattitude;

       
            public string State_Province
            {
                get { return m_State_Province; }
                set { m_State_Province = value; }
            }

            private string m_State_Province;


        }





    }
    #endregion

    //=======================================================
    //Service provided by Telerik (www.telerik.com)
    //Conversion powered by NRefactory.
    //Twitter: @telerik, @toddanglin
    //Facebook: facebook.com/telerik
    //=======================================================

    //6/8/2011 - enum types for some stuff

    /// <summary>
    /// Enum values used to index array.
    /// </summary>
    public enum ProfileStatusTypes
    {
        PeekedAtMe,
        PeekedAtThisMember,
        LikesMe,
        LikedThisMember,
        BLockedMe,
        BlockedThisMember,
        InterestSentToMe,
        InterestSentToThisMember,
        Max
    }


    /// <summary>
    /// Contains array of elements indexed by enums.
    /// </summary>
    public class ProfileStatuses
    {
        /// <summary>
        /// Contains one element per enum.
        /// </summary>
        public static bool[] _array = new bool[(int)ProfileStatusTypes.Max];
    }




}
