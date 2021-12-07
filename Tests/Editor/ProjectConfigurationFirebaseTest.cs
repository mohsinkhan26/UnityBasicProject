/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using NUnit.Framework;
using MK.Common.Helpers;

namespace MK.AssetData
{
    public class ProjectConfigurationFirebaseTest
    {
        readonly ProjectConfiguration projectConfiguration = AssetDataHelper.GetProjectConfigurationData();

        [Test]
        public void ProjectConfiguration_FirebaseProjectID_IsNotEmpty()
        {
            Assert.IsNotEmpty(projectConfiguration.FirebaseProjectID, "Firebase ProjectID field is empty");
        }

        [Test]
        public void ProjectConfiguration_FirebaseEditorServiceAccountEmail_IsNotEmpty()
        {
            Assert.IsNotEmpty(projectConfiguration.FirebaseEditorServiceAccountEmail,
                "Firebase EditorServiceAccountEmail field is empty");
        }

        [Test]
        public void ProjectConfiguration_FirebaseEditorP12FileName_IsNotEmpty()
        {
            Assert.IsNotEmpty(projectConfiguration.FirebaseEditorP12FileName, "Firebase P12 FileName field is empty");
        }

        [Test]
        public void ProjectConfiguration_FirebaseEditorP12Password_IsNotEmpty()
        {
            Assert.IsNotEmpty(projectConfiguration.FirebaseEditorP12Password, "Firebase P12 Password field is empty");
        }
    }
}