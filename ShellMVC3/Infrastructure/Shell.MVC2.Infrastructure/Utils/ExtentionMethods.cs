using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Text;

using System.Web.Mvc;


using Akismet.NET;
using System.Configuration;


namespace Shell.MVC2.Infrastructure
{

    //9-5-2012 olawal added extention for getting the descrption of an enum, this is cached
    public static class Extensions
    {


        public static List<SelectListItem> AddFirstItemToSelectListItems(List<SelectListItem> list, string Item)
        {
            // List<SelectListItem> _list = list.ToList();
            list.Insert(0, new SelectListItem() { Value = "-1", Text = Item });
            return list;
        }


        public static SelectList AddFirstItemToSelectList(SelectList list)
        {
            List<SelectListItem> _list = list.ToList();
            _list.Insert(0, new SelectListItem() { Value = "-1", Text = "This Is First Item" });
            return new SelectList((IEnumerable<SelectListItem>)_list, "Value", "Text");
        } 



        /// <summary>
        /// This class add some extenssion methods for the <see cref="Akismet.NET.Comment"/>.
        /// </summary>
      
            /// <summary>
            /// Check if the input akismet comment is spam or not.
            /// </summary>
            /// <param name="comment">The input comment.</param>
            /// <returns>True for a spam comment, false otherwise.</returns>
            public static Boolean IsSpam(Comment comment)
            {
                Validator validator = new Validator(ConfigurationManager.AppSettings["AKISMET_KEY"]);
                return validator.IsSpam(comment);
            }


            /// <summary>
            /// Check if the input akismet comment is spam or not.
            /// </summary>
            /// <param name="comment">The input comment.</param>
            /// <returns>True for a spam comment, false otherwise.</returns>
            public static Boolean MessageContainsBannedChars(string value)
            {

                string stringToCheck = value;
                string[] stringArray = { "yahoo", "gmail", "@", ".com",".net","hotmail","msn" };
                string[] revArray = Array.ConvertAll<string, string>(stringArray, delegate(string s) { return s.ToUpper(); });

                string[] combined = stringArray.Concat(revArray).ToArray();

                if (combined.Any(stringToCheck.Contains))  
                // Process... 
                        return true;
              

                return false;
             }

        //linq conditinal where helpers stuff
        public static IQueryable<TSource> WhereIf<TSource>(
        this IQueryable<TSource> source, bool condition,Expression<Func<TSource, bool>> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }

        public static IEnumerable<TSource> WhereIf<TSource>(
    this IEnumerable<TSource> source, bool condition,
    Func<TSource, bool> predicate)
        {
            if (condition)
                return source.Where(predicate);
            else
                return source;
        }
        //end
 
        // calculate gate
        public static int CalculateAge(DateTime BirthDate)
        {
            DateTime Now = DateTime.Today;

            int years = Now.Year - BirthDate.Year;

            if (Now.Month < BirthDate.Month || (Now.Month == BirthDate.Month && Now.Day < BirthDate.Day))
            {
                --years;
            }

            return years;
        }


        //converts inches to feet for display purposes 
        public static string ToFeetInches(double inches)
        {
            string value1 = ((int)inches / 12).ToString() + "ft";
            string value2 = (inches % 12).ToString() + "'";
            return value1 +" " +value2;
        }


        //
        // Calculations ??
        //public static string Chop(string s, int length)
       
        //Move this to extentin methods done !
        public static string Chop(string s, int length)
        {
            //handle null string
            if (s == null) return "";

            /// <summary>
            /// Returns part of a string up to the specified number of characters, while maintaining full words
            /// </summary>
            /// <param name="s"></param>
            /// <param name="length">Maximum characters to be returned</param>
            /// <returns>String</returns>
            if (String.IsNullOrEmpty(s))
                throw new ArgumentNullException(s);
            var words = s.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();

            foreach (var word in words.Where(word => (sb.ToString().Length + word.Length) <= length))
            {
                sb.Append(word + " ");
            }
            return sb.ToString().TrimEnd(' ') + "...";
        }

        //Move this to extentin methods done !
        public static string ReduceStringLength(string s, int length)
        {
            /// <summary>
            /// Returns part of a string up to the specified number of characters, while maintaining full words
            /// </summary>
            /// <param name="s"></param>
            /// <param name="length">Maximum characters to be returned</param>
            /// <returns>String</returns>
            if (String.IsNullOrEmpty(s))
                return s;
              //  throw new ArgumentNullException(s);

            

            //remove spaces first
            s= s.Replace(" ", "");
            s = s.Trim();
            //handle case where we dont have to do anythim
            if (s.Length <= length)
                return s;
            int charstoremove = s.Length - length;
            s = s.Remove(11,charstoremove -1);  
            return s;


            

        }

        //this function is only called if the user has no databse settings for what they are looking for which should in be int searchsettings table
        public static int GetLookingForGenderID(int MygenderID)
        {

            if (MygenderID == 1)
            {
                return 2;
            }

            return 1;
        }

        //this function is only called if the user has no databse settings for what they are looking for which should in be int searchsettings table
        public static string ConvertGenderID(int intgenderID)
        {

            if (intgenderID == 1)
            {
                return "Male";
            }

            return "Female";
        }

        public static int ConvertGenderName(string genderName)
        {

            if (genderName.ToUpper()  == "MALE")
            {
                return 1;
            }

            return 2;
        }

        public static string NormalizeScreenName(string ScreenName)
        {
              if (String.IsNullOrEmpty(ScreenName))
                return ScreenName;

              return ScreenName.Trim().Replace(" ", "");
        }

        public static List<SelectListItem> ToSelectList<T>(
         this IEnumerable<T> enumerable,
         Func<T, string> text,
         Func<T, string> value,
         string defaultOption
         )
        {
            var items = enumerable.Select(x => new SelectListItem
            {
                Text = text(x),
                Value = value(x).ToString(),
                Selected = false
            }).ToList();

            //items.Insert(0, new SelectListItem
            //{
            //    Text = defaultOption,
            //    Value = "-1",
            //    Selected = true
            //});

            return items;
        } 



        public class HttpContextFactory
        {
            private static HttpContextBase m_context;
                public static HttpContextBase Current {
                    get { if (m_context != null)    return m_context; 
                    if (HttpContext.Current == null)        
                        throw new InvalidOperationException("HttpContext is not available");
                    return new HttpContextWrapper(HttpContext.Current);
                }
       } 
            
            public static void SetCurrentContext(HttpContextBase context)
                { 
                    m_context = context;
                } 
        }


        /// <summary>
        /// Function to get byte array from a file
        /// </summary>
        /// <param name="_FileName">File name to get byte array</param>
        /// <returns>Byte Array</returns>
        public static  byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;

            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                // get total byte length of the file
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                // read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                // close file reader
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return _Buffer;
        }

    }

}

  


