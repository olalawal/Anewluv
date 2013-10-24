using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure;

namespace Anewluv.Domain.Data
{
    public class generatedlists
    {

    public static  List<age> ageslist()
        {

              List<age> tmplist = new List<age>();
                    // Loop over the int List and modify it.
                    //insert the first one as ANY
                    tmplist.Add(new age { ageindex = "0", agevalue = "Any" });

                    for (int i = 18; i < 100; i++)
                    {

                        var CurrentAge = new age { ageindex = i.ToString(), agevalue = i.ToString() };
                        tmplist.Add(CurrentAge);
                    }
                    return tmplist;
           
 

        }


        public static List<metricheight> metricheights()
        {

            List<metricheight> templist = new List<metricheight>();
            // Loop over the int List and modify it.
            //insert the first one as ANY
            templist.Add(new metricheight { heightindex = "0", heightvalue = "Any" });

            for (int i = 48; i < 89; i++)
            {

                var CurrentHeight = new metricheight { heightindex = i.ToString(), heightvalue = Extensions.ToFeetInches(i) };
                templist.Add(CurrentHeight);

            }

            return templist;
        
        }



    }
}
