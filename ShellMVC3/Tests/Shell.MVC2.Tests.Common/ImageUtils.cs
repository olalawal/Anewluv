using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Shell.MVC2.Tests.Common
{
    public class ImageUtils
    {

        //converts a png image to base64
        public static string SmallImageTestdata()
        {



            //"E:\publish\Dev\Scripts\development-bundle\demos\droppable\images"
            //added 12/15/2011 add image stuff
            //grab and image and then convert it to base 64
            byte[] imageBytes = null;
            FileStream fs = new FileStream("E:\\publish\\Dev\\Scripts\\development-bundle\\demos\\droppable\\images\\high_tatras2.jpg", FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);

            try
            {
                long size = reader.BaseStream.Length;
                imageBytes = new byte[Convert.ToInt32(size - 1) + 1];
                for (long i = 0; i <= size - 1; i++)
                {
                    imageBytes[Convert.ToInt32(i)] = reader.ReadByte();
                }
            }
            finally
            {
                reader.Close();
                fs.Close();
            }

            //
            // Convert the images bytes into its equivalent string representation
            // encoded with base 64 digit.
            //
           string bytearray =  Convert.ToBase64String(imageBytes);

           return bytearray;



        }
        //converts a bmp image to base64
        public static string LargeImageTestdata()
        {
            //added 12/15/2011 add image stuff
            //grab and image and then convert it to base 64
            byte[] imageBytes = null;
            FileStream fs = new FileStream("C:\\Lighthouse.jpg", FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);

            try
            {
                long size = reader.BaseStream.Length;
                imageBytes = new byte[Convert.ToInt32(size - 1) + 1];
                for (long i = 0; i <= size - 1; i++)
                {
                    imageBytes[Convert.ToInt32(i)] = reader.ReadByte();
                }
            }
            finally
            {
                reader.Close();
                fs.Close();
            }

            //
            // Convert the images bytes into its equivalent string representation
            // encoded with base 64 digit.
            //
            string  bytearray =  Convert.ToBase64String(imageBytes);

            return bytearray;

        }

    }
}
