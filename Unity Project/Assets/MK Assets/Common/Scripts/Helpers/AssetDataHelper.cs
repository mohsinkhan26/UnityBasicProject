#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using MK.AssetData;

namespace MK.Common.Helpers
{
    public static class AssetDataHelper
    {
        // don't save data in this class, its just a helper class
        public const string DATABASE_PATH_PROJECT_CONFIGURATIONS =
            "Assets/MK Assets/Common/Scripts/Asset Data/ProjectConfiguration Data/ProjectConfigurations.asset";

#if UNITY_EDITOR
        public static ProjectConfiguration GetProjectConfigurationData()
        {
            return GetDataFile<ProjectConfiguration>(DATABASE_PATH_PROJECT_CONFIGURATIONS);
        }

        #region General

        static T GetDataFile<T>(string _assetPath) where T : ScriptableObject
        {
            return (T) AssetDatabase.LoadAssetAtPath(_assetPath, typeof(T));
        }

        #endregion General

#endif
    }
}
