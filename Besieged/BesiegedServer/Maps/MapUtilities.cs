using Framework.Map;
using Framework.Utilities.Xml;
using System;
using System.IO;
using System.Text;
using Utilities;

namespace BesiegedServer.Maps
{
    public static class MapUtilities
    {
        public static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        public static void SaveToFile(string serializedMap)
        {
            GameMap map = serializedMap.FromXml<GameMap>();
            string tempFileName = map.Name + '-' + map.Author + '-' + DateTime.Today.ToShortDateString().ToString().Replace('/', '-');

            //save the file to the filesystem with the temporary name
            using (FileStream fileWriter = File.Open(tempFileName, FileMode.OpenOrCreate))
            {
                using (StreamWriter sw = new StreamWriter(fileWriter))
                {
                    sw.WriteLine(serializedMap);
                }
            }

            FileInfo fileInfo = new FileInfo(tempFileName);
            long t = fileInfo.Length;
            int lg = Convert.ToInt32(t);
            byte[] file = new byte[lg];
            string MD5Hash = string.Empty;
            byte[] hashBytes = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(file);
            MD5Hash = ByteArrayToString(hashBytes);

            File.Copy(tempFileName, MD5Hash);
            File.Delete(tempFileName); //delete the old file
            try
            {
                //move file into Large Storage Attachment folder and store as hash value xxx\xxx\xxx\xxxxxxxxxxxxxxxxxxxxxxx
                if (!Directory.Exists(MD5Hash.Substring(0, 3) + "\\" + MD5Hash.Substring(3, 3) + "\\" + MD5Hash.Substring(6, 3)))
                {
                    Directory.CreateDirectory( MD5Hash.Substring(0, 3) + "\\" + MD5Hash.Substring(3, 3) + "\\" + MD5Hash.Substring(6, 3));
                }
                if (!File.Exists( MD5Hash.Substring(0, 3) + "\\" + MD5Hash.Substring(3, 3) + "\\" + MD5Hash.Substring(6, 3) + "\\" + MD5Hash.Substring(9)))
                {
                    File.Move(MD5Hash, MD5Hash.Substring(0, 3) + "\\" + MD5Hash.Substring(3, 3) + "\\" + MD5Hash.Substring(6, 3) + "\\" + MD5Hash.Substring(9));

                    //args.Item.MoveTo( MD5Hash.Substring(0, 3) + "\\" + MD5Hash.Substring(3, 3) + "\\" + MD5Hash.Substring(6, 3) + "\\" + MD5Hash.Substring(9));
                }
                if(File.Exists(MD5Hash))
                {
                    File.Delete(MD5Hash);
                }
            }
            catch(Exception ex)
            {
                ConsoleLogger.Push("Error Saving File : " + MD5Hash + " - " + ex.Message);
                ErrorLogger.Push(ex);
            }
        }
    }
}