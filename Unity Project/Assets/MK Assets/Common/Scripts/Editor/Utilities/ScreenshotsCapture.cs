/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using UnityEditor;
using UnityEngine;

namespace MK.Common.Utilities
{
    public class ScreenshotsCapture
    {
        [MenuItem("Tools/Capture Sceenshot #s")]
        static void CaptureSceenshot()
        {
            ScreenCapture.CaptureScreenshot(System.DateTime.Now.ToString("ddMMyyHHmmss") + ".png");
        }
    }
}
