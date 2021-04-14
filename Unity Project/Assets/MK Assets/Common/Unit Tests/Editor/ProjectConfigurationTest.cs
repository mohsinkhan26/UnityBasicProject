using NUnit.Framework;
/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using UnityEngine;
using MK.Common.Helpers;
using MK.Common.Extensions;

namespace MK.AssetData
{
    public class ProjectConfigurationTest
    {
        readonly ProjectConfiguration projectConfiguration = AssetDataHelper.GetProjectConfigurationData();

        [Test]
        public void ProjectConfiguration_AssetFileExists()
        {
            Assert.IsNotNull(projectConfiguration, "Asset file don't exist on specified path");
        }

        [Test]
        public void ProjectConfiguration_SupportedPlatforms()
        {
            Assert.True(Application.platform.ToString().CaseSensitiveEquals(RuntimePlatform.Android.ToString()) ||
                        Application.platform.ToString().CaseSensitiveEquals(RuntimePlatform.IPhonePlayer.ToString()) ||
                        Application.platform.ToString().CaseSensitiveEquals(RuntimePlatform.WindowsEditor.ToString()) ||
                        Application.platform.ToString().CaseSensitiveEquals(RuntimePlatform.OSXEditor.ToString()),
                "Current runtime platform is not supported");
        }

        [Test]
        public void ProjectConfiguration_FirebaseProjectID_IsNotEmpty()
        {
            Assert.IsNotEmpty(projectConfiguration.firebaseProjectID, "Firebase ProjectID field is empty");
        }

        [Test]
        public void ProjectConfiguration_FirebaseEditorServiceAccountEmail_IsNotEmpty()
        {
            Assert.IsNotEmpty(projectConfiguration.firebaseEditorServiceAccountEmail,
                "Firebase EditorServiceAccountEmail field is empty");
        }

        [Test]
        public void ProjectConfiguration_FirebaseEditorP12FileName_IsNotEmpty()
        {
            Assert.IsNotEmpty(projectConfiguration.firebaseEditorP12FileName, "Firebase P12 FileName field is empty");
        }

        [Test]
        public void ProjectConfiguration_FirebaseEditorP12Password_IsNotEmpty()
        {
            Assert.IsNotEmpty(projectConfiguration.firebaseEditorP12Password, "Firebase P12 Password field is empty");
        }

        [Test]
        public void ProjectConfiguration_IsProductionBuild()
        {
            //Assert.IsTrue(projectConfiguration.isProductionBuild, "Production Build is unchecked, so Development Build settings are set");
            Assert.True(projectConfiguration.isProductionBuild,
                "Production Build is unchecked, so Development Build settings are set");

            //Assert.IsFalse(projectConfiguration.enableLogging, "Logging is unchecked, so Development Build settings are set");
            Assert.False(projectConfiguration.enableLogging,
                "Logging is unchecked, so Development Build settings are set");
        }

        [Test]
        public void ProjectConfiguration_IsLoggingEnabled()
        {
            Assert.IsTrue(projectConfiguration.enableLogging, "Logger Disabled");
        }
    }
}
