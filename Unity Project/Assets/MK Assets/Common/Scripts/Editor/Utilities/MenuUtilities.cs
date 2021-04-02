/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using UnityEditor;
using UnityEngine;

namespace MK.Common.Utilities
{
    public static class MenuUtilities
    {
        #region Scene Specific
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
        #endregion Scene Specific

        #region General
        public static void LoadData<T>(ref T _object, string _assetPath) where T : ScriptableObject
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(_assetPath);

            if (_object == null)
                CreateData(ref _object, _assetPath);

            // select the .asset fiile
            EditorGUIUtility.PingObject(_object);
            // focus the project window
            EditorUtility.FocusProjectWindow();
        }

        public static void CreateData<T>(ref T _object, string _assetPath) where T : ScriptableObject
        {
            _object = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(_object, _assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        #endregion General

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
