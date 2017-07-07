﻿/* 
 * Author : Mohsin Khan
 * Website : http://mohsinkhan.000webhostapp.com/
*/
using UnityEditor;
using UnityEngine;

namespace MK.Common.Miscellaneous
{
    public static class GameUtilities
    {
        [MenuItem("Tools/MK Assets/Check All Plugins", false, 50)]
        public static void CheckAllPlugins()
        {
            Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:28971/");
        }

        [MenuItem("Tools/MK Assets/OpenScene/Sample Scene")]
        public static void OpenSampleScene()
        {
            OpenScene("Sample");
        }

        [MenuItem("Tools/MK Assets/OpenScene/Mono Direct - Demo Scene")]
        public static void OpenMonoDirectDemoScene()
        {
            OpenScene("Mono Direct/MonoDirectDemo");
        }

        static void OpenScene(string name)
        {
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/MK Assets/" + name + ".unity");
            }
        }
    }
}
