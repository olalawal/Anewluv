using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace Shell.MVC2.Infrastructure.Utils
{
    class NullSafe
    {
        #region "Null Safe"

        public static string NullSafeString(object arg, string returnIfEmpty = "")
        {
            string returnValue = null;

            if ((object.ReferenceEquals(arg, DBNull.Value)) || (arg == null) || (object.ReferenceEquals(arg, string.Empty)))
            {
                returnValue = returnIfEmpty;
            }
            else
            {
                try
                {
                    //CStr() doesn't work with Guid's, so change to use ToString().
                    returnValue = Convert.ToString(arg).Trim();
                }
                catch
                {
                    returnValue = returnIfEmpty;
                }

            }

            return returnValue;
        }

        //public static DateTime NullSafeDateTime(object arg)
        //{
        //    DateTime? returnValue = null;



        //    if ((object.ReferenceEquals(arg, DBNull.Value)) || (arg == null) || (object.ReferenceEquals(arg, string.Empty)))
        //    {
        //        returnValue = System.DateTime.MinValue;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            //CStr() doesn't work with Guid's, so change to use ToString().
        //            returnValue = Convert.ToString(arg).Trim();
        //        }
        //        catch
        //        {
        //            returnValue = System.DateTime.MinValue;
        //        }

        //    }

        //    return returnValue;
        //}
        public static int NullSafeInteger(object arg, int returnIfEmpty = 0)
        {
            int returnValue = 0;

            if ((object.ReferenceEquals(arg, DBNull.Value)) || (arg == null) || (object.ReferenceEquals(arg, string.Empty)))
            {
                returnValue = returnIfEmpty;
            }
            else
            {
                try
                {
                    returnValue = Convert.ToInt32(arg);
                }
                catch
                {
                    returnValue = returnIfEmpty;
                }
            }

            return returnValue;
        }

        public static long NullSafeLong(object arg, long returnIfEmpty = 0)
        {
            long returnValue = 0;

            if ((object.ReferenceEquals(arg, DBNull.Value)) || (arg == null) || (object.ReferenceEquals(arg, string.Empty)))
            {
                returnValue = returnIfEmpty;
            }
            else
            {
                try
                {
                    returnValue = Convert.ToInt64(arg);
                }
                catch
                {
                    returnValue = returnIfEmpty;
                }
            }

            return returnValue;
        }

        public static double NullSafeDouble(object arg, double returnIfEmpty = 0)
        {
            double returnValue = 0;

            if ((object.ReferenceEquals(arg, DBNull.Value)) || (arg == null) || (object.ReferenceEquals(arg, string.Empty)))
            {
                returnValue = returnIfEmpty;
            }
            else
            {
                try
                {
                    returnValue = Convert.ToDouble(arg);
                }
                catch
                {
                    returnValue = returnIfEmpty;
                }
            }

            return returnValue;
        }

        public static decimal NullSafeDecimal(object arg, decimal returnIfEmpty = 0)
        {
            decimal returnValue = default(decimal);

            if ((object.ReferenceEquals(arg, DBNull.Value)) || (arg == null) || (object.ReferenceEquals(arg, string.Empty)))
            {
                returnValue = returnIfEmpty;
            }
            else
            {
                try
                {
                    returnValue = Convert.ToDecimal(arg);
                }
                catch
                {
                    returnValue = returnIfEmpty;
                }
            }

            return returnValue;
        }

        public static short NullSafeShort(object arg, short returnIfEmpty = 0)
        {
            short returnValue = 0;

            if ((object.ReferenceEquals(arg, DBNull.Value)) || (arg == null) || (object.ReferenceEquals(arg, string.Empty)))
            {
                returnValue = returnIfEmpty;
            }
            else
            {
                try
                {
                    returnValue = Convert.ToInt16(arg);
                }
                catch
                {
                    returnValue = returnIfEmpty;
                }
            }

            return returnValue;
        }

        public static bool NullSafeBoolean(object arg)
        {
            bool returnValue = false;

            if ((object.ReferenceEquals(arg, DBNull.Value)) || (arg == null) || (object.ReferenceEquals(arg, string.Empty)))
            {
                returnValue = false;
            }
            else
            {
                try
                {
                    returnValue = Convert.ToBoolean(arg);
                }
                catch
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        public static Guid NullSafeGuidString(object arg)
        {
            Guid? returnValue = default(Guid);

            if ((object.ReferenceEquals(arg, DBNull.Value)) || (arg == null) || (object.ReferenceEquals(arg, string.Empty)))
            {
                returnValue = null;
            }
            else
            {
                try
                {
                    returnValue = new Guid(Convert.ToString(arg));
                }
                catch
                {
                    returnValue = null;
                }
            }

            return returnValue.GetValueOrDefault();
        }

        public static void SafeDispose(IDisposable o)
        {
            if (o != null)
            {
                o.Dispose();
            }
        }

        public static bool HasData(string str)
        {
            if (str != null && str.Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       

        public static bool HasData(Array a)
        {
            if (a == null || a.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool HasData(IEnumerable c)
        {
            if (c == null)
            {
                return false;
            }
            else
            {
                IEnumerator e = default(IEnumerator);
                e = c.GetEnumerator();

                if (e.Current == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion

    }
}
