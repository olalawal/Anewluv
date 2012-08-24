using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;




using Microsoft.VisualBasic;

using System.Collections;

using System.Data;
using System.Diagnostics;
using System.IO;


using System.Security.Cryptography;

using Security.Cryptography;


using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shell.MVC2.Tests
{
  




    [TestClass]
    public class UnitTestEncryption
    {

  public static string EncryptionKey = "12608454@gflgF*3dg";
  public static byte[] Key = { 121, 217, 19, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 21, 24, 175, 144, 133, 234, 196, 29, 24, 27, 17, 218, 131, 236, 53, 209 };
  public static byte[] IV = { 146, 64, 191, 111, 23, 3, 113, 119, 231, 121, 251, 112, 79, 32, 114, 156 };

        [TestMethod]
        public void TestEncryptAndDecrption()
        {
         Assert.AreEqual(testencryptdecrpt("kayode02"), "kayode02");
         Assert.AreEqual(testencryptdecrpt("ccccccccccc"), "ccccccccccc");
         Assert.AreEqual(testencryptdecrpt("1232323231"), "1232323231");
         Assert.AreEqual(testencryptdecrpt("momdad2003213!~@"), "momdad2003213!~@");
        }



     

      

        //using a hidden string to decryt here in case db is cracked they still won't have the rest of the key
        public static string Encrypt(string decryptedString, string password)
        {
            // decryptedString = decryptedString & "12608454@gflgf*3dg"
            string encryptedString = string.Empty;

            byte[] decryptedBytes = UTF8Encoding.UTF8.GetBytes(decryptedString);
            byte[] saltBytes = Encoding.UTF8.GetBytes(password);

            using (AesManaged aes = new AesManaged())
            {
                Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, saltBytes);

                aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                aes.Key = rfc.GetBytes(Convert.ToInt32(aes.KeySize / 8));
                aes.IV = rfc.GetBytes(Convert.ToInt32(aes.BlockSize / 8));

                using (ICryptoTransform encryptTransform = aes.CreateEncryptor())
                {
                    using (MemoryStream encryptedStream = new MemoryStream())
                    {
                        using (CryptoStream encryptor = new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write))
                        {
                            encryptor.Write(decryptedBytes, 0, decryptedBytes.Length);
                            encryptor.Flush();
                            encryptor.Close();

                            byte[] encryptedBytes = encryptedStream.ToArray();
                            encryptedString = Convert.ToBase64String(encryptedBytes);
                        }
                    }
                }

            }

            return encryptedString;
        }


        public static string Decrypt(string encryptedString, string password)
        {


            string decryptedString = string.Empty;

            byte[] encryptedBytes = Convert.FromBase64String(encryptedString);
            byte[] saltBytes = Encoding.UTF8.GetBytes(password);

            //added try catch to handle where the password is too short
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, saltBytes);

                    aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
                    aes.KeySize = aes.LegalKeySizes[0].MaxSize;
                    aes.Key = rfc.GetBytes(Convert.ToInt32(aes.KeySize / 8));
                    aes.IV = rfc.GetBytes(Convert.ToInt32(aes.BlockSize / 8));

                    using (ICryptoTransform decryptTransform = aes.CreateDecryptor())
                    {
                        using (MemoryStream decryptedStream = new MemoryStream())
                        {
                            using (CryptoStream decryptor = new CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write))
                            {
                                decryptor.Write(encryptedBytes, 0, encryptedBytes.Length);
                                decryptor.Flush();
                                decryptor.Close();

                                byte[] decryptedBytes = decryptedStream.ToArray();
                                decryptedString = UTF8Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return decryptedString;
            }


            return decryptedString;
        }



        //ENCcryption scheme used after 8/2/2011 - to allow for salts always being at least 8 bytes




        public static string encryptString(string dataString)
        {


            dynamic aes = new AesManaged();
            ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
            string encryptedString = performCryptoAndEncoding(dataString, encryptor);



            return encryptedString;


        }

        public static string decryptString(string dataString)
        {

            dynamic aes = new AesManaged();
            // Decryption Code        


            ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
            string decryptedString = performCryptoAndEncoding(dataString, decryptor);
            return decryptedString;


        }

        public static string performCryptoAndEncoding(string dataToEncryptOrDecrypt, ICryptoTransform transform)
        {

            byte[] encodedData = encodeStringToBytes(dataToEncryptOrDecrypt);



            MemoryStream dataStream = new MemoryStream();


            CryptoStream encryptionStream = new CryptoStream(dataStream, transform, CryptoStreamMode.Write);
            encryptionStream.Write(encodedData, 0, encodedData.Length);


            encryptionStream.FlushFinalBlock();
            dataStream.Position = 0;

            byte[] transformedBytes = dataStream.ToArray();
            dataStream.Read(transformedBytes, 0, transformedBytes.Length);

            encryptionStream.Close();
            dataStream.Close();



            //insteand of normal encoding we transformed them encyprt them as base 64 
            //string transformedAndReencodedData = encodeBytesToString(transformedBytes);
            // http://blogs.msdn.com/b/shawnfa/archive/2005/11/10/491431.aspx?PageIndex=3#comments
            // byte[] encrypted = transformedBytes.ToArray();

            return Convert.ToBase64String(transformedBytes);




            //return transformedAndReencodedData;

        }


        public static string performCryptoAndDecoding(string dataToEncryptOrDecrypt, ICryptoTransform transform)
        {

            //byte[] encodedData = encodeStringToBytes(dataToEncryptOrDecrypt);

            byte[] encodedData = Convert.FromBase64String(dataToEncryptOrDecrypt);


            MemoryStream dataStream = new MemoryStream();



            CryptoStream encryptionStream = new CryptoStream(dataStream, transform, CryptoStreamMode.Write);
            encryptionStream.Write(encodedData, 0, encodedData.Length);


            encryptionStream.FlushFinalBlock();
            dataStream.Position = 0;

            byte[] transformedBytes = dataStream.ToArray();
            dataStream.Read(transformedBytes, 0, transformedBytes.Length);

            encryptionStream.Close();
            dataStream.Close();





            //insteand of normal encoding we transformed them encyprt them as base 64 
            string transformedAndReencodedData = encodeBytesToString(transformedBytes);
            // http://blogs.msdn.com/b/shawnfa/archive/2005/11/10/491431.aspx?PageIndex=3#comments
            // byte[] encrypted = transformedBytes.ToArray();

            //return transformedBytes;




            return transformedAndReencodedData;

        }



        public static string testencryptdecrpt(string dataString)
        {

            SymmetricEncryptionState encryptionState = null;
            string encryptedString;
            using (SymmetricAlgorithm aes = new AesManaged().EnableLogging())
            {

                //  dynamic aes = new AesManaged();
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                encryptedString = performCryptoAndEncoding(dataString, encryptor);
                encryptionState = aes.GetLastEncryptionState();

            }

            //dynamic aes = new AesManaged();
            // Decryption Code   

            //first thing convert it from base 64 
            //http://blogs.msdn.com/b/shawnfa/archive/2005/11/10/491431.aspx?PageIndex=3#comments





            using (SymmetricAlgorithm aes = new AesManaged().EnableDecryptionVerification(encryptionState))
            {

                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                string decryptedString = performCryptoAndDecoding(encryptedString, decryptor);
                return decryptedString;
            }

        }



        public static byte[] encodeStringToBytes(string dataToEncode)
        {
            //byte[] rawData = Convert.FromBase64String(dataToEncode);
            // byte[] rawData = Convert.FromBase64String(dataToEncode.Replace("'","+"));



            // byte[] rawData = Convert.FromBase64String(dataToEncode);
            // return rawData;

            byte[] encodedData = Encoding.Unicode.GetBytes(dataToEncode);
            return encodedData;
        }

        public static string encodeBytesToString(byte[] dataToEncode)
        {



            string encodedData = Encoding.Unicode.GetString(dataToEncode, 0, dataToEncode.Length);
            return encodedData;


        }




    }
}
