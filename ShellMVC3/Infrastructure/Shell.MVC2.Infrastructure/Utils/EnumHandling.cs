using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Specialized;

namespace Shell.MVC2.Infrastructure
{

    public static class EnumExtensionMethods
    {
        //object to cache values that have been retrived already
        static StringDictionary _enumDescriptions = new StringDictionary();

        public static string ToDescription(this Enum en)
        //ext method 
        {
            Type type = en.GetType();
            string key = type.ToString() + "___" + en.ToString();
            if (_enumDescriptions[key] == null)
            {
                MemberInfo[] memInfo = type.GetMember(en.ToString());
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                        _enumDescriptions[key] = ((DescriptionAttribute)attrs[0]).Description;
                    // return ((DescriptionAttribute)attrs[0]).Description;
                    return _enumDescriptions[key];
                }
                // return en.ToString();
                _enumDescriptions[key] = en.ToString();
            }
            return _enumDescriptions[key];
        }



    }

    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    } 

//   public class EnumUtils
//    {

//        private string GetEnumDescription(Enum value)
//        {
//            // Get the Description attribute value for the enum value
//            FieldInfo fi = value.GetType().GetField(value.ToString());
//            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
//            if (attributes.Length > 0)
//            {
//                return attributes[0].Description;
//            }
//            else
//            {
//                return value.ToString();
//            }
//        }

//        //object to cache values that have been retrived already
//        static StringDictionary _enumDescriptions = new StringDictionary();
//        public class EnumDescriptionAttribute : Attribute 
//        {
 
//        private string _text = "";
//        /// <summary> 
//        /// Text describing the enum value 
//        /// </summary> 
//        public string Text 
//        { 
//            get { return this._text; } 
//        }
//        /// <summary> 
//        /// Instantiates the EnumDescriptionAttribute object 
//        /// </summary> 
//        /// <param name=”text”>Description of the enum value</param> 
//        public EnumDescriptionAttribute(string text) 
//        { 
//            _text = text; 
//        }
 
//    } 
//        public static string GetEnumDescription<EnumType>(EnumType @enum) 
//{
 
//    Type enumType = @enum.GetType(); 
//    string key = enumType.ToString() + "___" +  @enum.ToString(); 
//    if (_enumDescriptions[key] == null) 
//    { 
//        FieldInfo info = enumType.GetField(@enum.ToString()); 
//        if (info != null) 
//        { 
//            EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])info.GetCustomAttributes(typeof(EnumDescriptionAttribute), false); 
//            if (attributes != null && attributes.Length > 0) 
//            { 
//                _enumDescriptions[key] = attributes[0].Text; 
//                return _enumDescriptions[key]; 
//            } 
//        } 
//        _enumDescriptions[key] = @enum.ToString(); 
//    } 
//    return _enumDescriptions[key];
 
//}



//    }

  
}
