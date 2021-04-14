/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using UnityEditor;
using MK.Common.Helpers;
using MK.AssetData;

/// <summary>
/// Only create editor/shortcut in project settings which are only one
/// </summary>
namespace MK.Common.Utilities
{
    [CustomEditor(typeof(ProjectConfiguration))]
    public class ProjectConfiguration_Editor : Editor
    {
        [SettingsProvider]
        internal static SettingsProvider CreateProjectConfigurationProvider()
        {
            var assetPath = AssetDatabase.GetAssetPath(AssetDataHelper.GetProjectConfigurationData());

            var keywords = SettingsProvider.GetSearchKeywordsFromPath(assetPath);
            return AssetSettingsProvider.CreateProviderFromAssetPath(
                "Project/App-Project Configuration", assetPath, keywords);
        }
    }
}
