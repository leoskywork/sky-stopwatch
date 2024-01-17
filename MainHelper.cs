using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;


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


        //调用tesseract实现OCR识别
        public static string ReadImageAsText(string imgPath)
        {
            using (var engine = new Tesseract.TesseractEngine("tessdata", "chi_sim", Tesseract.EngineMode.Default))
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

    }
}
