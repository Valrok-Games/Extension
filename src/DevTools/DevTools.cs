using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Valrok.Extension.DevTools
{
    public static class DevTools
    {

#if UNITY_EDITOR
        [MenuItem("Valrok/DevTools/Screenshots/Take Screenshot")]
        static void Screenshot()
        {
            string fileName = Application.persistentDataPath + "_" + DateTime.Now + "_" + "_Screenshot.png";
            ScreenCapture.CaptureScreenshot(fileName);
        }
#endif
    }
}