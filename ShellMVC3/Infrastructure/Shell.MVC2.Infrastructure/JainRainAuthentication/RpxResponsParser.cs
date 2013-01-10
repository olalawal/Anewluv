using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;



namespace Shell.MVC2.Infastructure.JanRainAuthentication
{
    public enum RpxReponseStatus
    {
        Ok,
        Fail
    }

    public class RpxResponseParser
    {
        #region Fields

        readonly XElement _response;

        #endregion

        #region Properties

        public RpxReponseStatus Status { get; set; }

        #endregion

        #region Constructor

        public RpxResponseParser(XmlNode response)
        {
            _response = ToXElement(response);
            Status = ParseStatus();
        }

        #endregion

        #region Public Methods

        //public LogOnModel BuildUser()
        //{
        //    return new LogOnModel
        //    {
        //        UserName = GetUserName(),
        //        OpenIDidentifer   = GetOpenId(),
        //        ScreenName  = GetDisplayName(),
        //        ProfileID= GetEmail()

        //    };
        //}

        ////TO DO use this for regathering registration data
        //public RegisterModel RegisterUser()
        //{

        //    return new RegisterModel 
        //    {
                
        //    };

        //}

        public string GetEmail()
        {
            var email = _response.Descendants("profile").Select(p => (string)p.Element("email")).SingleOrDefault();
            return email;
        }

        public string GetDisplayName()
        {
            var displayName = _response.Descendants("profile").Select(p => (string)p.Element("displayName")).SingleOrDefault();
            return displayName;
        }

        public string GetUserName()
        {
            var userName = _response.Descendants("profile").Select(p => (string)p.Element("preferredUsername")).SingleOrDefault();
            return userName;
        }

        public string GetOpenId()
        {
            var userName = _response.Descendants("profile").Select(p => (string)p.Element("identifier")).SingleOrDefault();
            return userName;
        }

        #endregion

        #region Private Methods

        private static XElement ToXElement(XmlNode xml)
        {
            return XElement.Parse(xml.OuterXml);
        }

        private RpxReponseStatus ParseStatus()
        {
            var status = (string)_response.Attribute("stat");
            return (RpxReponseStatus)Enum.Parse(typeof(RpxReponseStatus), status, true /*ignoreCase*/);
        }
        


        #endregion
    }
}