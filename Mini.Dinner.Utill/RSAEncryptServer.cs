using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mini.Dinner.Utill
{
    public class RSAEncryptServer
    {
        private static string PrivateKey = @"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";
        public static string PublicKey = @"MIICCgKCAgEAu7EmTWv8ALa8FH6G9s9B8Yh9w9l4zLG03Ej7/pAgmLiXxgUUFDub
mmd5xVuyBcFWcY4FkpElQK3L7aRD+FI7tzR2mWCH45FsGH57kQIo4lWpdN8OJlwJ
FV5JODIoCHvYXcvIjgBVeCEwBZrVrdGx8+QViAlfuLWbHVDFO5rLD2YmqlUrRJ9l
Ql9JaGit09aC1y1He3n6YoMWNShVwdS6mP9DXl/Hl4Fhs+ugPFj0VZiHcJ5rvGQJ
UPubaalD9ca69J3QGOdmtwezobNSidtNOFqpSDLUHVU+O0KIXS2k48CSApGt/X0c
0wKHo+drZ8i//KUCp1/aaWSvbKScBvesHQOKPxTeL1SG9xD5p+VyH+QhQPNSpboZ
0GnFbvB3Wj7ACLEvASL3X2u4OF7Zm+wSFLyq4UN/RDm556hlN3RKFaJJHcSFbnZc
eUnqziLM+YIngvijGfD6oB5/+79+QueCupssr7P9z9bXK59/MEyMmDYPt5Q/zHuv
lTnOPa6tXtZq2kA/FttTdwKr0npc/RsRB6sUz4VJB26lBw98zd1TPFov3xBz/i4d
T6fFeAqBVAGEM5x2ADiKCIUwi+iLta8iZK9R+PtVAr0MPZyvVOhymuGFS/hLXiW0
EX6R25eVGDrXg21oLjhaTtjcRG80tWI4HdZCUnX+AD7O2j0YXVk3yMcCAwEAAQ==";
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public static string ParametDecrypt(string cipher)
        {
            var rsa = RSA.Create();
            byte[] cipherbytes;
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(PrivateKey), out _);
            //ToXmlString(rsa, true);
            cipherbytes = rsa.Decrypt(Convert.FromBase64String(cipher), RSAEncryptionPadding.Pkcs1);

            return Encoding.UTF8.GetString(cipherbytes);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="publickey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ParametEncrypt(string content)
        {
            var rsa = RSA.Create();
            byte[] cipherbytes;
            rsa.ImportRSAPublicKey(Convert.FromBase64String(PublicKey), out _);
            //ToXmlString(rsa,false);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(cipherbytes);
        }
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plaintext"></param>
        /// <returns></returns>
        public static string ParametEncryptMore(string plaintext)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(PublicKey), out int bytesRead);
            //ToXmlString(rsa, false);
            int keySize = rsa.KeySize / 8;
            int bufferSize = keySize - 11;
            byte[] buffer = new byte[bufferSize];
            MemoryStream msInput = new MemoryStream(Encoding.UTF8.GetBytes(plaintext));
            MemoryStream msOutput = new MemoryStream();
            int readLen = msInput.Read(buffer, 0, bufferSize);
            while (readLen > 0)
            {
                byte[] dataToEnc = new byte[readLen];
                Array.Copy(buffer, 0, dataToEnc, 0, readLen);
                byte[] encData = rsa.Encrypt(dataToEnc, RSAEncryptionPadding.Pkcs1);
                msOutput.Write(encData, 0, encData.Length);
                readLen = msInput.Read(buffer, 0, bufferSize);
            }
            msInput.Close();
            byte[] result = msOutput.ToArray();    //得到加密结果
            msOutput.Close();
            rsa.Clear();
            return Convert.ToBase64String(result);
        }
        public static string ParametDecryptMore(string ciphertext)
        {
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(PrivateKey), out int bytesRead);
            //ToXmlString(rsa, true);
            int keySize = rsa.KeySize / 8;
            byte[] buffer = new byte[keySize];
            MemoryStream msInput = new MemoryStream(Convert.FromBase64String(ciphertext));
            MemoryStream msOutput = new MemoryStream();
            int readLen = msInput.Read(buffer, 0, keySize);
            while (readLen > 0)
            {
                byte[] dataToDec = new byte[readLen];
                Array.Copy(buffer, 0, dataToDec, 0, readLen);
                byte[] decData = rsa.Decrypt(dataToDec, RSAEncryptionPadding.Pkcs1);
                msOutput.Write(decData, 0, decData.Length);
                readLen = msInput.Read(buffer, 0, keySize);
            }
            msInput.Close();
            byte[] result = msOutput.ToArray();    //得到解密结果
            msOutput.Close();
            rsa.Clear();
            return Encoding.UTF8.GetString(result);
        }


        #region FromXmlString

        public static void FromXmlString(RSA rsa, string xmlString)
        {
            RSAParameters parameters = new RSAParameters();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            if (xmlDoc.DocumentElement.Name.Equals("RSAKeyValue"))
            {
                foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Modulus":
                            parameters.Modulus = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText);
                            break;
                        case "Exponent":
                            parameters.Exponent = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText);
                            break;
                        case "P":
                            parameters.P = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText);
                            break;
                        case "Q":
                            parameters.Q = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText);
                            break;
                        case "DP":
                            parameters.DP = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText);
                            break;
                        case "DQ":
                            parameters.DQ = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText);
                            break;
                        case "InverseQ":
                            parameters.InverseQ = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText);
                            break;
                        case "D":
                            parameters.D = string.IsNullOrEmpty(node.InnerText) ? null : Convert.FromBase64String(node.InnerText);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                throw new Exception("Invalid XML RSA key.");
            }

            rsa.ImportParameters(parameters);
        }

        public static string ToXmlString(RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                  parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                  parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
                  parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
                  parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
                  parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
                  parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
                  parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
                  parameters.D != null ? Convert.ToBase64String(parameters.D) : null);
        }

        #endregion
    }
}
