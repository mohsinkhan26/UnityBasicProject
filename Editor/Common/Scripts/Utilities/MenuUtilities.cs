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
    public static class MenuUtilities
    {
        #region Scene Specific

        [MenuItem("Tools/MK Assets/My Portfolio", false, 50)]
        public static void CheckMyPortfolio()
        {
            Application.OpenURL("http://mohsinkhan26.github.io/");
        }

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

        #endregion Scene Specific

        #region Module Specific

        /* usage
        static LoginData loginData;
        [MenuItem("Tools/Game/Login Data", false, 20)]
        public static void LoginDataFile()
        {
            loginData = AssetDataHelper.GetLoginData();
            LoadData(ref loginData, AssetDataHelper.DATABASE_PATH_LOGIN_DATA);
        }*/

        #endregion Module Specific
    }
}
