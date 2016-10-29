using System;
using System.Net.Mime;
using System.Windows.Forms;

namespace PhotoManager.Utils
{

    // Wait methods for WebElementExtension and other use
    public static class Wait
    {
        public static void WaitSeconds(int seconds)
        {
            WaitMiliSeconds(seconds*1000);
        }

        public static void WaitMiliSeconds(int miliSeconds)
        {
            DateTime start = DateTime.Now;
            while ((DateTime.Now - start).TotalMilliseconds < miliSeconds)
            {
                Application.DoEvents();
            }
        }
    }
}
