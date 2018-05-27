using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using QRCoder;
using W = Microsoft.Office.Interop.Word;

using System.Windows.Forms;
using Exam.DBTableAdapters;
using Exam.Properties;
using Rsx.Dumb;


namespace Exam
{
    public static class Tools
    {
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private static QRCodeGenerator qrGenerator = new QRCodeGenerator();

        public static Image CreateQRCode(string identifier, int size)
        {
            QRCodeData qrCode = qrGenerator.CreateQrCode(identifier, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(qrCode);
            Image img = code.GetGraphic(size);
            return img;
        }

        //no lo uso
        public enum Options { A = 1, B, C, D, E };

        public static char[] Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        private static readonly Random GetRandom = new Random();

        public static void StripAnswer(ref string provided)
        {
         provided = provided.ToUpper();
         provided = provided.Replace('-', ' ');
         provided = provided.Replace('*', ' ');
         provided = provided.Replace('_', ' ');
         provided = provided.Replace('/', ' ');
         provided = provided.Replace('+', ' ');
         provided = provided.Replace('\\', ' ');
         provided = provided.Replace(" ", null);
         provided = provided.Trim();
            
        }

        public static IEnumerable<T> RandomizeStrings<T>(IEnumerable<T> arr)
        {
            List<KeyValuePair<int, T>> list = new List<KeyValuePair<int, T>>();
            // Add all strings from array
            // Add new random int each time
            int max = arr.Count();
            foreach (T s in arr)
            {
                list.Add(new KeyValuePair<int, T>(Tools.GetRandom.Next(1, max), s));
            }
            // Sort the list by the random number
            var sorted = from item in list
                         orderby item.Key
                         select item;
            // Allocate new string array
            IList<T> result = new List<T>(arr.Count());
            // Copy values to array
            int index = 0;
            foreach (KeyValuePair<int, T> pair in sorted)
            {
                result.Add(pair.Value);
                index++;
            }
            // Return copied array
            return result;
        }

        public static void MakePDF(ref W.Document doc)
        {
            object fileName = doc.FullName.Replace(".docx", ".pdf");

            object type = W.WdExportFormat.wdExportFormatPDF;
            doc.SaveAs2(ref fileName, ref type);
        }
    }
}