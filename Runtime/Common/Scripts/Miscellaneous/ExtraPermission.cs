/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using UnityEngine;

namespace MK.Common.Miscellaneous
{
    // Reference: https://github.com/yasirkula/UnityNativeCamera.git
    // Reference: https://github.com/yasirkula/UnityNativeGallery.git
    public class ExtraPermission : MonoBehaviour
    {
        #region Permissions

        /*private bool cameraPermission = true;
        private bool galleryPermission = true;
        public void GetCameraPermission(Action _onSuccess, Action _onFailure)
        {
#if !UNITY_EDITOR
            if (cameraPermission)
                NativeCamera.RequestPermission();
            if (NativeCamera.CheckPermission() == NativeCamera.Permission.Granted)
#endif
            {
                if (_onSuccess != null) _onSuccess.Invoke();
            }
#if !UNITY_EDITOR
            else
            {
                this.LogException("NativeCamera-User didn't gave permission to save the files on device");
                NativeCamera.OpenSettings();

                if (_onSuccess != null) _onSuccess.Invoke();

// #if UNITY_IPHONE
//                 string url = OpenSettingsBindings.GetSettingsURL();
//                 //Debug.Log("The settings url is:" + url);
//                 Application.OpenURL(url);
// #else
//                 NativeCamera.RequestPermission();
// #endif
            }
#endif
        }

        public void GetGalleryPermission(Action _onSuccess, Action _onFailure)
        {
#if !UNITY_EDITOR
            if (galleryPermission)
                NativeGallery.RequestPermission(NativeGallery.PermissionType.Write);
            if (NativeGallery.CheckPermission(NativeGallery.PermissionType.Write) == NativeGallery.Permission.Granted)
#endif
            {
                if (_onSuccess != null) _onSuccess.Invoke();
            }
#if !UNITY_EDITOR
            else
            {
                this.LogException("NativeGallery-User didn't gave permission to save the files on device");
                NativeGallery.OpenSettings();
                if (_onFailure != null) _onFailure.Invoke();

// #if UNITY_IPHONE
//                 string url = OpenSettingsBindings.GetSettingsURL();
//                 //Debug.Log("The settings url is:" + url);
//                 Application.OpenURL(url);
// #else
//                 NativeGallery.RequestPermission();
// #endif
            }
#endif
        }*/

        #endregion Permissions
    }
}
