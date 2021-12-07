/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System.Text;
using UnityEngine;

namespace MK.AssetData
{
    public class ProjectConfigurationFirebase : ScriptableObject
    {
        #region Firebase

        // Reference: https://firebase.google.com/docs/database/unity/start 

        [Header("Firebase Specific")]
        [Tooltip("Editor Database URL and StorageBucket link will be created with this")]
        /// <summary>
        /// The firebase project identifier.
        /// access it from https://console.firebase.google.com/project/[YOUR_OWN_PROJECT_ID]/settings/general/[YOUR_PROJECT_PLATFORM]:[YOUR_PROJECT_IDENTIFIER/YOUR_PROJECT_PACKAGE_NAME(com.google.firebase.test)]
        /// </summary>
        [SerializeField]
        string firebaseProjectID;

        public string FirebaseProjectID
        {
            get { return firebaseProjectID; } // to be sure it can't be modified from the code
        }

        /// <summary>
        /// Gets the firebase editor database URL.
        /// </summary>
        /// <value>The firebase editor database URL.</value>
        public string FirebaseEditorDatabaseURL
        {
            get
            {
                return new StringBuilder("https://").Append(firebaseProjectID)
                    .Append(".firebaseio.com/")
                    // .Append("-default-rtdb.firebaseio.com/")     // one of these works
                    .ToString();
            }
        }

        /// <summary>
        /// Don't create, if it already available. Use adminsdk email
        /// Create a service account and get the p12 file associated with it
        /// access it from https://console.cloud.google.com/iam-admin/serviceaccounts/project?project=[YOUR_OWN_PROJECT_ID]
        /// OR https://console.firebase.google.com/project/[YOUR_OWN_PROJECT_ID]/settings/serviceaccounts/adminsdk
        /// OR https://console.firebase.google.com/project/[YOUR_OWN_PROJECT_ID]/settings/cloudmessaging/[YOUR_PROJECT_PLATFORM]:[YOUR_PROJECT_IDENTIFIER/YOUR_PROJECT_PACKAGE_NAME(com.google.firebase.test)]
        /// </summary>
        [SerializeField] string firebaseEditorServiceAccountEmail;

        public string FirebaseEditorServiceAccountEmail
        {
            get { return firebaseEditorServiceAccountEmail; } // to be sure it can't be modified from the code
        }

        /// <summary>
        /// Add the p12 file to "Editor Default Resources"
        /// Create from https://console.cloud.google.com/iam-admin/serviceaccounts/project?project=[YOUR_OWN_PROJECT_ID]
        /// Under Keys, generate P12 file. The name of the firebase editor p12 file.
        /// </summary>
        [SerializeField] string firebaseEditorP12FileName;

        public string FirebaseEditorP12FileName
        {
            get { return firebaseEditorP12FileName; } // to be sure it can't be modified from the code
        }

        /// <summary>
        /// The firebase editor p12 password.
        /// Make sure to record the password after creating the service account as it will not be reachable again
        /// </summary>
        [SerializeField] string firebaseEditorP12Password = "notasecret";

        public string FirebaseEditorP12Password
        {
            get { return firebaseEditorP12Password; } // to be sure it can't be modified from the code
        }

        /// <summary>
        /// When you enable Google sign-in on https://console.firebase.google.com/project/[YOUR_OWN_PROJECT_ID]/authentication/providers
        /// Then you will find this value in the dropdown of the popup
        /// OR https://console.developers.google.com/apis/credentials?pli=1&project=[YOUR_OWN_PROJECT_ID]&authuser=0
        /// OR Copy this value from the google-service.json file, oauth_client with type == 3
        /// </summary>
        [SerializeField] string firebaseWebClientID;

        public string FirebaseWebClientID
        {
            get { return firebaseWebClientID; } // to be sure it can't be modified from the code
        }

        [SerializeField] string firebaseWebClientSecret;

        public string FirebaseWebClientSecret
        {
            get { return firebaseWebClientSecret; } // to be sure it can't be modified from the code
        }

        /// <summary>
        /// Gets the firebase storage bucket.
        /// access it from https://console.firebase.google.com/project/[YOUR_OWN_PROJECT_ID]/storage/[YOUR_OWN_PROJECT_ID].appspot.com/files 
        /// </summary>
        /// <value>The firebase storage bucket.</value>
        public string FirebaseStorageBucket
        {
            get { return new StringBuilder("gs://").Append(firebaseProjectID).Append(".appspot.com/").ToString(); }
        }

        #endregion Firebase

        #region Error Messages

        [Header("Firebase Error Messages")] // just add main message from Inspector
        public string firebaseError_OnLogin_NoUserFound;

        public string firebaseError_OnCreateUser_AlreadyUserExist;

        #endregion Error Messages

#if UNITY_EDITOR

        #region testing

        // the UserIDs given in Firebase Authentication are one next to the given number, means yxvkuxxu1@mdx.ip correspond to testUser0@hku.hk
        [Header("For Testing Purpose in Editor")]
        public string testFirebaseUserID_ForEditor;

        public string testEmail_ForEditor;
        public string testPassword_ForEditor;
        public string testDisplayName;

        #endregion Testing

#endif
    }
}