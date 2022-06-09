/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace MK.Common.Helpers
{
    public static class WebDownloadFile
    {
        /// <summary>
        /// Gets file from URL.
        /// </summary>
        /// <param name="_mono">_mono.</param>
        /// <param name="_url">Downloads the file from this _url.</param>
        /// <param name="_callback">Returns file in _callback, in success case</param>
        /// <param name="_progressMessage">_progress message.</param>
        public static void GetFileFromURL(this MonoBehaviour _mono, string _url, string _pathWithName,
            Action<bool, string> _callback, Action<string> _progressMessage = null)
        {
            _mono.StartCoroutine(_mono.GetFileFromURLCoroutine(_url, _pathWithName, _callback, _progressMessage));
        }

        /// <summary>
        /// Gets file from URL coroutine.
        /// </summary>
        /// <returns>The file from URL coroutine.</returns>
        /// <param name="_mono">_mono.</param>
        /// <param name="_url">Downloads the file from this _url.</param>
        /// <param name="_callback">Returns file in _callback, in success case</param>
        /// <param name="_progressMessage">_progress message.</param>
        private static IEnumerator GetFileFromURLCoroutine(this MonoBehaviour _mono, string _url, string _pathWithName,
            Action<bool, string> _callback, Action<string> _progressMessage = null)
        {
            Debug.Log("GetFileFromURLCoroutine-URL: <color=green>" + _url + "</color>\n");
            using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(_url))
            {
                while (!unityWebRequest.isDone)
                {
                    _progressMessage?.Invoke("Status: Downloading file... " +
                                             String.Format("{0:N}%", (unityWebRequest.downloadProgress * 100)));
                    yield return unityWebRequest.SendWebRequest();
                }

                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("GetFileFromURLCoroutine-Error: <color=red>" + unityWebRequest.result +
                              "</color>\nSaveFile: " + false);
                    _callback?.Invoke(false, unityWebRequest.result.ToString());
                }
                else
                {
                    System.IO.File.WriteAllBytes(_pathWithName, unityWebRequest.downloadHandler.data);
                    _callback?.Invoke(true, "");
                    Debug.Log("WebDownloadFile-GetFileFromURLCoroutine-THREAD----DONE-FilePath: <color=cyan>" +
                              _pathWithName + "\nBytes: " + unityWebRequest.downloadedBytes
                              + "\nBytesLength: " + unityWebRequest.downloadHandler.data.Length + "</color>");
                }
            }
        }
    }
}