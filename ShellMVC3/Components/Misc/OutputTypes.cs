using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misc
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
   
    public class OutPutTypes
    {

        private const string outfile = "c:\\outputfile.txt";

        public static void OutPutTestsToScreen()
        {
           // TestList.TestsToRun();
        }


        public static void OutputTestToFile()
        {
            StreamWriter writer = null;
            TextWriter standardOutput = Console.Out;



            try
            {
                Console.WriteLine("Please wait .. running selected tests and outputing to file");
                writer = new StreamWriter(outfile);
                Console.SetOut(writer);
                // Console.SetIn(New StreamReader(outfile))


               // TestList.TestsToRun();

                Console.WriteLine();
                //Console.WriteLine("Press <ENTER> to terminate client.")
                // Console.ReadLine()



               // Console.WriteLine(Constants.vbLf + "Done");

                //run the first set of tests 



                writer.Close();
                // Recover the standard output stream so that a  
                // completion message can be displayed.
                Console.SetOut(standardOutput);
                Console.WriteLine("WCF external services tests has completed the processing of outputfile");





            }
            catch (IOException e)
            {
                TextWriter errorWriter = Console.Error;
                errorWriter.WriteLine(e.Message);
                errorWriter.WriteLine(outfile);
            }



        }
    }

}
