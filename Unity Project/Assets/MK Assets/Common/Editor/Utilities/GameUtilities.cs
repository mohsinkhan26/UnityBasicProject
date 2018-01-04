﻿/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using UnityEditor;
using UnityEngine;

namespace MK.Common.Utilities
{
    public static class GameUtilities
    {
        [MenuItem("Tools/MK Assets/Check All Plugins", false, 50)]
        public static void CheckAllPlugins()
        {
            Application.OpenURL("https://assetstore.unity.com/publishers/28971");
        }
        
        [MenuItem("Tools/MK Assets/GitHub Profile", false, 50)]
        public static void CheckGitHubProfile()
        {
            Application.OpenURL("https://github.com/mohsinkhan26/");
        }

        [MenuItem("Tools/MK Assets/BitBucket Profile", false, 50)]
        public static void CheckBitBucketProfile()
        {
            Application.OpenURL("https://bitbucket.org/mohsinkhan26/");
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