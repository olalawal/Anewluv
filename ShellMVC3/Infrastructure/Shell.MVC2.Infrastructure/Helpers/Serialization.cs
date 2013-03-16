using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization.Json;
using System.IO;


public class Serialization
{

    public static string datetimetojson(System.DateTime myDate)
    {


        try
        {
            return getserializedstring(myDate);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        //return "Date was in an invlaid format";
        //Dim dateTime__1 As New DateTime(myDate)
        // output the original information.  
        //OutputDate(myDate)
        // manually change the kind of the datetime.  
        //dateTime__1 = DateTime.SpecifyKind(myDate, DateTimeKind.Utc)
        // output the new information. OutputDate(dateTime__1) 
        // Console.ReadLine()
    }

    private static string getserializedstring(DateTime dt)
    {

        string result = null;
        DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(DateTime));

        using (MemoryStream ms = new MemoryStream())
        {
            dcjs.WriteObject(ms, dt);
            ms.Position = 0;
            using (StreamReader reader = new StreamReader(ms))
            {
                result = reader.ReadToEnd();
            }
        }
        return result;
    }
}
