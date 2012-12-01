using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

using System.Text;
using System.Security.Cryptography;



public class Encryption
{



	//----------------------------------------------------------------
	// Converted from C# to VB .NET using CSharpToVBConverter(1.2).
	// Developed by: Kamal Patel (http://www.KamalPatel.net) 
	//----------------------------------------------------------------
	public static string EncryptionKey = "12608454@gflgF*3dg";
	public static byte[] Key = {
		121,
		217,
		19,
		11,
		24,
		26,
		85,
		45,
		114,
		184,
		27,
		162,
		37,
		112,
		222,
		209,
		21,
		24,
		175,
		144,
		133,
		234,
		196,
		29,
		24,
		27,
		17,
		218,
		131,
		236,
		53,
		209
	};
	public static byte[] IV = {
		146,
		64,
		191,
		111,
		23,
		3,
		113,
		119,
		231,
		121,
		251,
		112,
		79,
		32,
		114,
		156

	};

	//from website 
	// http://aspnet.4guysfromrolla.com/articles/101205-1.aspx
	public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
	{
		//Make sure length and numberOfNonAlphanumericCharacters are valid....
		//... checks omitted for brevity ... see live demo for full code ...


		while (true) {
			int i = 0;
			int nonANcount = 0;
			byte[] buffer1 = new byte[length];

			//chPassword contains the password's characters as it's built up
			char[] chPassword = new char[length];

			//chPunctionations contains the list of legal non-alphanumeric characters
			char[] chPunctuations = "!@@$%^^*()_-+=[{]};:>|./?".ToCharArray();

			//Get a cryptographically strong series of bytes
			System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
			rng.GetBytes(buffer1);

			for (i = 0; i <= length - 1; i++) {
				//Convert each byte into its representative character
				int rndChr = (buffer1[i] % 87);
				if ((rndChr < 10)) {
					chPassword[i] = Convert.ToChar(Convert.ToUInt16(48 + rndChr));
				} else {
					if ((rndChr < 36)) {
						chPassword[i] = Convert.ToChar(Convert.ToUInt16((65 + rndChr) - 10));
					} else {
						if ((rndChr < 62)) {
							chPassword[i] = Convert.ToChar(Convert.ToUInt16((97 + rndChr) - 36));
						} else {
							chPassword[i] = chPunctuations[rndChr - 62];
							nonANcount += 1;
						}
					}
				}
			}

			if (nonANcount < numberOfNonAlphanumericCharacters) {
				Random rndNumber = new Random();
				for (i = 0; i <= (numberOfNonAlphanumericCharacters - nonANcount) - 1; i++) {
					int passwordPos = 0;
					do {
						passwordPos = rndNumber.Next(0, length);
					} while (!char.IsLetterOrDigit(chPassword[passwordPos]));
					chPassword[passwordPos] = chPunctuations[rndNumber.Next(0, chPunctuations.Length)];
				}
			}

			return new string(chPassword);
		}
		return null;
	}

	//the salt will be the screen name/login name since we use uniqye screen names for this application
	public static string EncodePasswordWithSalt(string pwd, string salt)
	{
		//put the salt as upper case to elimitae case sensitivity for the user name
		string saltAndPwd = string.Concat(pwd,  (salt.ToUpper()));
		byte[] byteSaltAndPwd = Encoding.UTF8.GetBytes(saltAndPwd);
		SHA1Managed Hash = new SHA1Managed();
		byte[] bytePasswordHashed = Hash.ComputeHash(byteSaltAndPwd);
		return Convert.ToBase64String(bytePasswordHashed);
	}

	//This function just encodes a string
	public static string EncodeString(string strValue)
	{
		//put the salt as upper case to elimitae case sensitivity for the user name

		byte[] byteValue = Encoding.UTF8.GetBytes(strValue);
		SHA1Managed Hash = new SHA1Managed();
		byte[] byteValueHashed = Hash.ComputeHash(byteValue);
		return Convert.ToBase64String(byteValueHashed);
	}

	//using a hidden string to decryt here in case db is cracked they still won't have the rest of the key
	public static string Encrypt(string decryptedString, string password)
	{
		// decryptedString = decryptedString & "12608454@gflgf*3dg"
		string encryptedString = string.Empty;

		byte[] decryptedBytes = UTF8Encoding.UTF8.GetBytes(decryptedString);
		byte[] saltBytes = Encoding.UTF8.GetBytes(password);

		using (AesManaged aes = new AesManaged()) {
			Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, saltBytes);

			aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
			aes.KeySize = aes.LegalKeySizes[0].MaxSize;
			aes.Key = rfc.GetBytes(Convert.ToInt32(aes.KeySize / 8));
			aes.IV = rfc.GetBytes(Convert.ToInt32(aes.BlockSize / 8));

			using (ICryptoTransform encryptTransform = aes.CreateEncryptor()) {
				using (MemoryStream encryptedStream = new MemoryStream()) {
					using (CryptoStream encryptor = new CryptoStream(encryptedStream, encryptTransform, CryptoStreamMode.Write)) {
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
		try {
			using (AesManaged aes = new AesManaged()) {
				Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, saltBytes);

				aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
				aes.KeySize = aes.LegalKeySizes[0].MaxSize;
				aes.Key = rfc.GetBytes(Convert.ToInt32(aes.KeySize / 8));
				aes.IV = rfc.GetBytes(Convert.ToInt32(aes.BlockSize / 8));

				using (ICryptoTransform decryptTransform = aes.CreateDecryptor()) {
					using (MemoryStream decryptedStream = new MemoryStream()) {
						using (CryptoStream decryptor = new CryptoStream(decryptedStream, decryptTransform, CryptoStreamMode.Write)) {
							decryptor.Write(encryptedBytes, 0, encryptedBytes.Length);
							decryptor.Flush();
							decryptor.Close();

							byte[] decryptedBytes = decryptedStream.ToArray();
							decryptedString = UTF8Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
						}
					}
				}

			}
		} catch (Exception ex) {
			return decryptedString;
		}


		return decryptedString;
	}



	//ENCcryption scheme used after 8/6/2011 - to allow for salts always being at least 8 bytes
	//users will have to change passwords



	public static string encryptString(string dataString)
	{

		using (AesManaged Aes = new AesManaged()) {

			//  dynamic aes = new AesManaged();
			ICryptoTransform encryptor = Aes.CreateEncryptor(Key, IV);
			return performCryptoAndEncoding(dataString, encryptor);


		}


	}


	public static string decryptString(string encryptedString)
	{

		using (AesManaged Aes = new AesManaged()) {

			ICryptoTransform decryptor = Aes.CreateDecryptor(Key, IV);
			return performCryptoAndDecoding(encryptedString, decryptor);

		}


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



		//insteand of normal saving the  encoded function as a noremal string of bytes we  them as base 64 so as not to lose padding
		//string transformedAndReencodedData = encodeBytesToString(transformedBytes);
		// http://blogs.msdn.com/b/shawnfa/archive/2005/11/10/491431.aspx?PageIndex=3#comments
		// byte[] encrypted = transformedBytes.ToArray();

		return Convert.ToBase64String(transformedBytes);




		//return transformedAndReencodedData;

	}


	public static string performCryptoAndDecoding(string dataToEncryptOrDecrypt, ICryptoTransform transform)
	{

		//byte[] encodedData = encodeStringToBytes(dataToEncryptOrDecrypt);

		try {
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

		} catch (Exception ex) {
			return "ErrorDecoding";
		}


	}





	public static byte[] encodeStringToBytes(string dataToEncode)
	{


		byte[] encodedData = Encoding.Unicode.GetBytes(dataToEncode);
		return encodedData;
	}

	public static string encodeBytesToString(byte[] dataToEncode)
	{


		string encodedData = Encoding.Unicode.GetString(dataToEncode, 0, dataToEncode.Length);
		return encodedData;


	}



}


