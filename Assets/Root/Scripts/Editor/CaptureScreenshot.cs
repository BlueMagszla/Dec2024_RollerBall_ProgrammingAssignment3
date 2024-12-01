using System;
using System.IO;
using UnityEngine;
using UnityEditor;

namespace RollerBall
{
    public static class CaptureScreenshot
    {
        private const string CaptureScreenshotDirectory = "Capture Screenshot/";
        private const string CaptureScreenshotMenuDirectory = Const.MenuItem.ToolsMenu + CaptureScreenshotDirectory;

        private const string Supersize1 = "SuperSize 1 ";
        private const string Supersize2 = "SuperSize 2 ";
        private const string Supersize3 = "SuperSize 3 ";
        private const string Supersize4 = "SuperSize 4 ";

        private const string Shift = Const.MenuItem.Shift;
        private const string F1 = Const.MenuItem.F1;
        private const string F2 = Const.MenuItem.F2;
        private const string F3 = Const.MenuItem.F3;
        private const string F4 = Const.MenuItem.F4;

        private const string Capture1 = CaptureScreenshotMenuDirectory + Supersize1 + Shift + F1;
        private const string Capture2 = CaptureScreenshotMenuDirectory + Supersize2 + Shift + F2;
        private const string Capture3 = CaptureScreenshotMenuDirectory + Supersize3 + Shift + F3;
        private const string Capture4 = CaptureScreenshotMenuDirectory + Supersize4 + Shift + F4;

        public static void TakeScreenshot(int superSize = 1, bool printPathInConsole = true)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            // TODO: improve
            string filename = DateTime.Now.ToString();
            filename = filename.Replace('\\', '_');
            filename = filename.Replace('/', '_');
            filename = filename.Replace('.', '_');
            filename = filename.Replace(':', '_');
            filename = filename.Replace(';', '_');
            filename = filename.Replace(' ', '_');

            filename = $"{filename}.png";
            path = Path.Combine(path, filename);
            ScreenCapture.CaptureScreenshot(path, superSize);

            if (printPathInConsole)
            {
                Debug.Log(path);
            }
        }

        #region Screenshot Hotkeys F1-F4
        [MenuItem(Capture1)]
        public static void TakeScreenshotSuperSize1()
        {
            TakeScreenshot(1);
        }

        [MenuItem(Capture2)]
        public static void TakeScreenshotSuperSize2()
        {
            TakeScreenshot(2);
        }

        [MenuItem(Capture3)]
        public static void TakeScreenshotSuperSize3()
        {
            TakeScreenshot(3);
        }

        [MenuItem(Capture4)]
        public static void TakeScreenshotSuperSize4()
        {
            TakeScreenshot(4);
        }
        #endregion
    }
}