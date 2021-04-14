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
        private static IEnumerator GetTextFromURLCoroutine(this MonoBehaviour _mono, string _url,
            Action<bool, string> _callback)
        {
            Debug.Log("GetTextFromURL-URL: <color=blue>" + _url + "</color>\n");
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(_url);
            yield return unityWebRequest.SendWebRequest();

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

        /// <summary>
        /// Gets the text from URL.
        /// </summary>
        /// <param name="mono">Monobehavior</param>
        /// <param name="unityWebRequest">UnityWebRequest.</param>
        /// <param name="callback">Callback.</param>
        public static void GetTextFromPostRequest(this MonoBehaviour _mono, UnityWebRequest _unityWebRequest,
            Action<bool, string> _callback = null)
        {
            _mono.StartCoroutine(_mono.GetTextFromPostRequestCoroutine(_unityWebRequest, _callback));
        }

        /// <summary>
        /// Gets the text from URL coroutine.
        /// </summary>
        /// <returns>The text from URL coroutine.</returns>
        /// <param name="mono">Mono.</param>
        /// <param name="unityWebRequest">UnityWebRequest.</param>
        /// <param name="callback">Callback.</param>
        private static IEnumerator GetTextFromPostRequestCoroutine(this MonoBehaviour _mono,
            UnityWebRequest _unityWebRequest, Action<bool, string> _callback)
        {
            Debug.Log("GetTextFromPostRequest-URL: <color=blue>" + _unityWebRequest.url + "</color>\n");
            yield return _unityWebRequest.SendWebRequest();

            if (_unityWebRequest.isNetworkError)
            {
                Debug.Log("GetTextFromPostRequest-Error: <color=red>" + _unityWebRequest.error + "</color>\n");
                if (_callback != null)
                    _callback(false, _unityWebRequest.error);
            }
            else if (_unityWebRequest.downloadHandler.text.Contains("403 Forbidden")
                     || _unityWebRequest.downloadHandler.text.Contains("\"data\":{\"status\":403}"))
            {
                Debug.Log("GetTextFromPostRequest-Access Error: <color=red>" + _unityWebRequest.downloadHandler.text +
                          "</color>\n");
                if (_callback != null) // handle this data carefully
                    _callback(false, _unityWebRequest.downloadHandler.text);
                //_callback(false, _unityWebRequest.error);
            }
            else
            {
                // Show results as text
                Debug.Log("GetTextFromPostRequest-Result: " + _unityWebRequest.downloadHandler.text + "\n");

                // Or retrieve results as binary data
                //byte[] results = www.downloadHandler.data;

                if (_callback != null)
                    _callback(true, _unityWebRequest.downloadHandler.text);
            }
        }
    }
}
