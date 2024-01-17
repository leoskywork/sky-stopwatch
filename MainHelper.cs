using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;

namespace SkyStopwatch
{
    class MainHelper
    {
        public static void PrintScreen(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");

            using (Bitmap bitPic = new Bitmap(width: Screen.PrimaryScreen.Bounds.Width, height: Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics gra = Graphics.FromImage(bitPic))
                {
                    gra.CopyFromScreen(new Point(0, 0), new Point(0, 0), bitPic.Size);
                    //bitPic.Save("D:\\screen.bmp");
                    bitPic.Save(path);
                    //bitPic.Dispose();
                    //gra.Dispose();
                }
            }
        }

        public static string PrintScreenAsTempFile()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            string exeDirectory = Path.GetDirectoryName(exePath);
            string subFolder = Path.Combine(exeDirectory, "tmp");

            string fileName = "ocr-screen-shot-" + DateTime.Now.ToString("yyyy-MMdd-HHmmss-fff") + ".bmp";
            string path = Path.Combine(subFolder, fileName);
            
            if(!Directory.Exists(subFolder))
            {
                Directory.CreateDirectory(subFolder);
            }

            if (Directory.GetFiles(subFolder).Length > 100)
            {
                Directory.Delete(subFolder, true);
                Directory.CreateDirectory(subFolder);
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            PrintScreen(path);

            return path;
        }


        public static string ReadImageAsText(string imgPath)
        {
            const string language = "eng"; //chi_sim;
            const string tessdataFolder = @"C:\Dev\VS2022\SkyStopwatch\Tesseract-OCR\tessdata\";

            using (var engine = new Tesseract.TesseractEngine(tessdataFolder, language, Tesseract.EngineMode.Default))
            {
                using (var img = Tesseract.Pix.LoadFromFile(imgPath))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }

        }

        public static DateTime FindTime(string data)
        {
            string[] lines = data.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            //hh:mm:ss
            string regexPattern = @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$";

            foreach (string line in lines)
            {
                if(line.IndexOf(':') > 0)
                {
                    var timePart = line.Substring(line.IndexOf(":") + 1).Trim();

                    if (Regex.IsMatch(timePart, regexPattern))
                    {
                        string timePartAdjust = timePart.Replace("00", "12");

                        DateTime textTime = DateTime.ParseExact(timePartAdjust, "hh:mm:ss", CultureInfo.InvariantCulture);

                        return textTime.AddSeconds(30);
                    }

                }


                //"mﬁmra : 00:28:27"
                //{
                //    MatchCollection parts = Regex.Matches(line, regexPattern);

                //    if (parts.Count >= 1)
                //    {
                //        string timePart = parts[parts.Count - 1].Value;
                //    }
                //}
            }

            return DateTime.MinValue;
        }

    }
}
