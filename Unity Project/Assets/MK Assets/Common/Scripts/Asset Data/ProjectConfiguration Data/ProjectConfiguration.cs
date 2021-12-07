/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using UnityEngine;

namespace MK.AssetData
{
    [CreateAssetMenu(fileName = "ProjectConfigurations", menuName = "Game/Project Configurations", order = 1)]
    public class ProjectConfiguration : ProjectConfigurationFirebase
    {
        #region General

        [Header("General")] public bool isProductionBuild;

        /// <summary>
        /// The enable logging.
        /// </summary>
        public bool enableLogging;

        #endregion General

        #region Game Settings

        [Header("Game Settings")] public string language;

        #endregion Game Settings
    }
}