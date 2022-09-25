using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SwasiHealthCare.Helper
{
    public class Encryption
    {
        #region Public Methods
        /// <summary>
        /// Please dont use this method. Decryption will not be possbile. If One way encryption required, please use it
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string Encrypt(string Value)
        {
            try
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(Value);
                string Encrypted = string.Empty;

                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Constants.EncryptKey));

                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);

                        Encrypted = Convert.ToBase64String(results, 0, results.Length);
                    }
                }

                return Encrypted;
            }
            catch (Exception)
            {
                return Value;
            }
        }

        /// <summary>
        /// Decryption will not possbile, due to Key not available
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string Decrypt(string Value)
        {
            try
            {
                byte[] data = Convert.FromBase64String(Value);
                string Decrypted = string.Empty;

                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Constants.EncryptKey));

                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);

                        Decrypted = UTF8Encoding.UTF8.GetString(results);
                    }
                }

                return Decrypted;
            }
            catch (Exception)
            {
                return Value;
            }
        }
        #endregion

        /// <summary>
        /// Use this method for encryption. Key will be available in Constant Page. Non-Linear process has been included.
        /// </summary>
        /// <param name="toencrypt"></param>
        /// <param name="key"></param>
        /// <param name="usehashing"></param>
        /// <returns></returns>
        public static string Encrypt(string toencrypt, string key, bool usehashing = true)
        {
            byte[] keyArray;

            // If hashing use get hash code regards to your key
            if (usehashing)
            {
                using (var hashmd5 = new MD5CryptoServiceProvider())
                {
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                }
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            // set the secret key for the tripleDES algorithm
            // mode of operation. there are other 4 modes.
            // We choose ECB(Electronic code Book)
            // padding mode(if any extra byte added)
            using (var tdes = new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            using (var transform = tdes.CreateEncryptor())
            {
                try
                {
                    var toEncryptArray = Encoding.UTF8.GetBytes(toencrypt);

                    // transform the specified region of bytes array to resultArray
                    var resultArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    // Return the encrypted data into unreadable string format
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Use this method for decryption. Key will be available in Constant Page. Non-Linear process has been included.
        /// </summary>
        /// <param name="todecrypt"></param>
        /// <param name="key"></param>
        /// <param name="usehashing"></param>
        /// <returns></returns>
        public static string Decrypt(string todecrypt, string key, bool usehashing = true)
        {
            byte[] toEncryptArray;

            // get the byte code of the string
            try
            {
                toEncryptArray = Convert.FromBase64String(todecrypt.Replace(" ", "+")); // The replace happens only when spaces exist in the string (hence not a Base64 string in the first place).
            }
            catch (Exception)
            {
                return string.Empty;
            }

            byte[] keyArray;

            if (usehashing)
            {
                // if hashing was used get the hash code with regards to your key
                using (var hashmd5 = new MD5CryptoServiceProvider())
                {
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                }
            }
            else
            {
                // if hashing was not implemented get the byte code of the key
                keyArray = Encoding.UTF8.GetBytes(key);
            }

            // set the secret key for the tripleDES algorithm
            // mode of operation. there are other 4 modes. 
            // We choose ECB(Electronic code Book)
            // padding mode(if any extra byte added)
            using (var tdes = new TripleDESCryptoServiceProvider { Key = keyArray, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            using (var transform = tdes.CreateDecryptor())
            {
                try
                {
                    var resultArray = transform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    // return the Clear decrypted TEXT
                    return Encoding.UTF8.GetString(resultArray);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }
    }
}
