﻿/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

namespace MK.Common.Helpers
{
    public static class WebData
    {
        /// <summary>
        /// Gets the text from URL.
        /// </summary>
        /// <param name="mono">Monobehavior</param>
        /// <param name="url">URL.</param>
        /// <param name="callback">Callback.</param>
        public static void GetTextFromURL(this MonoBehaviour _mono, string _url, Action<bool, string> _callback = null)
        {
            _mono.StartCoroutine(_mono.GetTextFromURLCoroutine(_url, _callback));
        }

        /// <summary>
        /// Gets the text from URL coroutine.
        /// </summary>
        /// <returns>The text from URL coroutine.</returns>
        /// <param name="mono">Mono.</param>
        /// <param name="url">URL.</param>
        /// <param name="callback">Callback.</param>
        private static IEnumerator GetTextFromURLCoroutine(this MonoBehaviour _mono, string _url, Action<bool, string> _callback)
        {
            Debug.Log("GetTextFromURL-URL: <color=blue>" + _url + "</color>\n");
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(_url);
            yield return unityWebRequest.Send();

            if (unityWebRequest.isNetworkError)
            {
                Debug.Log("GetTextFromURL-Error: <color=red>" + unityWebRequest.error + "</color>\n");
                if (_callback != null)
                    _callback(false, unityWebRequest.error);
            }
            else
            {
                // Show results as text
                Debug.Log("GetTextFromURL-Result: " + unityWebRequest.downloadHandler.text + "\n");

                // Or retrieve results as binary data
                //byte[] results = www.downloadHandler.data;

                if (_callback != null)
                    _callback(true, unityWebRequest.downloadHandler.text);
            }
        }
    }
}
