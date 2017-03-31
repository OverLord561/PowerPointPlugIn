using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;

namespace ManagerLib
{
    public class Manager
    {
        private static string userRoot = "HKEY_CURRENT_USER";
        private static string subkey = "WindowsService";
        private static string keyName = userRoot + "\\" + subkey;

        
        private const int Keysize = 256;        
        private const int DerivationIterations = 1000;
        private const string passPhrase = "Have a nice day";




        public static void ChangeStatus(string message)
        {
            SetDataToRegistry("Status", message);
        }

        public static void SetDataToRegistry(string key, string value)
        {
            value =  Encrypt(value);            
            Registry.SetValue(keyName, key, value);
        }


        public static string GetDataFromRegistry(string key)
        {
            return Registry.GetValue(keyName, key, "NotFound").ToString();
        }

        //Discover if Word and Excel are installed
        private bool CheckPowerPointAssociation()
        {
            var keyWORD = Registry.ClassesRoot.OpenSubKey("Word.Application", false);
            var keyPP = Registry.ClassesRoot.OpenSubKey("PowerPoint.Application", false);
            if (keyWORD != null || keyPP != null)
            {
                keyWORD.Close();
                keyPP.Close();

                return true;
            }
            else
            {
                return false;
            }
        }


        public static IEnumerable<Microsoft.Office.Interop.PowerPoint.Shape> CopyShape(IEnumerable<Microsoft.Office.Interop.PowerPoint.Shape> allShapes, Presentation currentPresentation, int indexOfSlide = 0)
        {

            // Globals.ThisAddIn.Application.ActiveWindow.View..Slide = new System.EventHandler(panel_Click);
            //String selected = listView1.SelectedItems[0].SubItems[0].Text;
            string TestFileName = "Shapes\\China_map_PPT_16-9" + ".ppt";

            var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), TestFileName);// source file path

            //open custom presentation from local storage

            Microsoft.Office.Interop.PowerPoint.Application pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();
            Presentation pptPresentation = pptApplication.Presentations.Open(pathToFile, MsoTriState.msoTrue, MsoTriState.msoFalse, MsoTriState.msoFalse);

            List<Microsoft.Office.Interop.PowerPoint.Shape> sh = new List<Microsoft.Office.Interop.PowerPoint.Shape>();


            foreach (Slide slide in pptPresentation.Slides)
            {
                // Get sizes of copied ppt file 
                float width = pptPresentation.PageSetup.SlideWidth;
                float height = pptPresentation.PageSetup.SlideHeight;
                slide.Copy();



                //Set sizes to users presentation
                currentPresentation.PageSetup.SlideWidth = width;
                currentPresentation.PageSetup.SlideHeight = height;

                //Insert copied ppt file under active slide             
                currentPresentation.Slides.Paste(indexOfSlide + 1);


                //  GetAllShapes(slide.Shapes,  currentPresentation,   allShapes, indexOfSlide);
            }
            pptApplication.Quit();

            return sh;
        }


        private static List<Microsoft.Office.Interop.PowerPoint.Shape> GetAllShapes(Microsoft.Office.Interop.PowerPoint.Shapes shapes, Presentation currentPresentation, IEnumerable<Microsoft.Office.Interop.PowerPoint.Shape> allShapes, int indexOfSlide = 0)
        {

            List<Microsoft.Office.Interop.PowerPoint.Shape> sh = allShapes.ToList(); ;


            foreach (Microsoft.Office.Interop.PowerPoint.Shape shape in shapes)
            {

                sh.Add(shape);

                //just for double ckick
                shape.Left = currentPresentation.PageSetup.SlideWidth / 2 - shape.Width / 2;
                shape.Top = currentPresentation.PageSetup.SlideHeight / 2 - shape.Height / 2;

                shape.Copy();

                currentPresentation.Slides[indexOfSlide].Shapes.Paste();

            }
            return sh;
        }

        public async static Task<bool> CheckIfUserExists(Dictionary<string, string> data, string URL)
        {
            bool isRegistered = false;

            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            HttpClient client = new HttpClient(handler);
            var content = new FormUrlEncodedContent(data);
            var response = await client.PostAsync(URL, content);
            //  var responseString = await response.Content.ReadAsStringAsync(); // returns page 
            Uri uri = new Uri(URL);
            IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
            foreach (Cookie cookie in responseCookies)
            {
                isRegistered = true;
            }

            return isRegistered;
        }

        public static bool CheckIfRefreshData()
        {
            bool res = false;

            try
            {
                string initialDate = Decrypt(GetDataFromRegistry("InitialRun"));
                string lastRun = Decrypt(GetDataFromRegistry("LastRun"));

                DateTime start = DateTime.Parse(initialDate);
                DateTime end = DateTime.Parse(lastRun);

              
                if ((end - start).TotalDays >= 20)
                {
                    res = true;
                }
            }
            catch
            {
                res = true;
            }
            
            return res;
        }

        public static string Encrypt(string plainText)
        {
           
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }



}

