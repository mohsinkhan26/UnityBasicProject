/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System.Linq;
using UnityEditor;
using UnityEngine;
using MK.Common;
using MK.Common.Helpers;

namespace MK.AssetData
{
    public static class ProjectConfigurationEditor
    {
        static ProjectConfiguration projectConfiguration;

        static void LoadConfiguration()
        {
            projectConfiguration = AssetDataHelper.GetProjectConfigurationData();
            Selection.activeObject =
                AssetDatabase.LoadMainAssetAtPath(AssetDataHelper.DATABASE_PATH_PROJECT_CONFIGURATIONS);

            if (projectConfiguration == null)
                CreateConfiguration();

            // select the .asset file
            EditorGUIUtility.PingObject(projectConfiguration);
            // focus the project window
            EditorUtility.FocusProjectWindow();
        }

        static void CreateConfiguration()
        {
            projectConfiguration = ScriptableObject.CreateInstance<ProjectConfiguration>();
            AssetDatabase.CreateAsset(projectConfiguration, AssetDataHelper.DATABASE_PATH_PROJECT_CONFIGURATIONS);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #region Logging

        [MenuItem("Tools/Game/Toggle Logging", false, 40)]
        public static void ToggleLogging()
        {
            LoadConfiguration();

            projectConfiguration.enableLogging = !projectConfiguration.enableLogging;
            EditorUtility.SetDirty(projectConfiguration);
            // to make it effect in project
            // TODO: copy paste the following line in GameManager to make it effect in build
            GameLogger.IsLogging = projectConfiguration.enableLogging;
        }

        [MenuItem("Tools/Game/Enable Logging", false, 40)]
        public static void EnableLogging()
        {
            LoadConfiguration();

            projectConfiguration.enableLogging = true;
            EditorUtility.SetDirty(projectConfiguration);
            // to make it effect in project
            // TODO: copy paste the following line in GameManager to make it effect in build
            GameLogger.IsLogging = projectConfiguration.enableLogging;
        }

        [MenuItem("Tools/Game/Disable Logging", false, 40)]
        public static void DisableLogging()
        {
            LoadConfiguration();

            projectConfiguration.enableLogging = false;
            EditorUtility.SetDirty(projectConfiguration);
            // to make it effect in project
            // TODO: copy paste the following line in GameManager to make it effect in build
            GameLogger.IsLogging = projectConfiguration.enableLogging;
        }

        #endregion Logging

        #region Build

        [MenuItem("Tools/Game/Make Production Build", false, 40)]
        public static void MakeProductionBuild()
        {
            BuildSettings(true);
        }

        [MenuItem("Tools/Game/Make Development Build", false, 40)]
        public static void MakeDevelopmentBuild()
        {
            BuildSettings(false);
        }

        static void BuildSettings(bool _isProduction)
        {
            LoadConfiguration();

            projectConfiguration.isProductionBuild = _isProduction;
            projectConfiguration.enableLogging = !_isProduction;
            EditorUtility.SetDirty(projectConfiguration);
            //AssetDataHelper.GetLoginData().ClearToken();
            //AssetDataHelper.GetLoginData().SaveAssetFile();
            MakeBuild();
        }

        static void MakeBuild()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = GetAllScenes();
            buildPlayerOptions.locationPathName =
                EditorUserBuildSettings.GetBuildLocation(EditorUserBuildSettings.activeBuildTarget);
            buildPlayerOptions.target = EditorUserBuildSettings.activeBuildTarget;
            buildPlayerOptions.options = BuildOptions.None;
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }

        //[MenuItem("Tools/Game/Scene Paths", false, 8)] // just for testing
        static string[] GetAllScenes()
        {
            EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
            int sceneCount = scenes.Count(s => s.enabled);
            string[] scenePaths = new string[sceneCount];
            int index = 0;
            for (int i = 0; i < scenes.Length; ++i)
            {
                if (scenes[i].enabled)
                {
                    scenePaths[index] = scenes[i].path;
                    //Debug.Log("Scene Path: " + scenePaths[index] + "   Count: " + sceneCount + "   Index: " + index);
                    ++index;
                }
            }

            return scenePaths;
        }

        #endregion Build
    }
}