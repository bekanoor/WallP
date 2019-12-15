using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WallP
{
    class Path
    {
        private string pathOne = AppDomain.CurrentDomain.BaseDirectory;
        private string pathTwo = "Pic";
        public void Up()
        {
            string path = System.IO.Path.Combine(pathOne, pathTwo);

            var dir = new DirectoryInfo(path);
            var files = new List<string>(); //list for file name 
            var pattern = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".png") || s.EndsWith(".jpg") || s.EndsWith(".jpeg"));

            if (!Directory.Exists(path)) // check path of exist
            {
                Close();
            }

            while (true)
            {
                foreach (var file in pattern) // extract all the files and get them on the list 
                {
                    SetBackgroud(file); // get the full path to the file and get it on the list 
                    Thread.Sleep(300000);
                }
            }
        }

        private void Close()
        {
            throw new NotImplementedException();
        }

        public static void SetBackgroud(string fileName) // set backgroud
        {
            int result = 0;
            if (File.Exists(fileName))
            {
                StringBuilder s = new StringBuilder(fileName);
                result = SystemParametersInfo(UAction.SPI_SETDESKWALLPAPER, 0, s, 0x2);
            }
        }

        public enum UAction
        {
            SPI_SETDESKWALLPAPER = 0x0014,
            SPI_GETDESKWALLPAPER = 0x0073,
        }

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(UAction uAction, int uParam, StringBuilder lpvParam, int fuWinIni);
    }
}