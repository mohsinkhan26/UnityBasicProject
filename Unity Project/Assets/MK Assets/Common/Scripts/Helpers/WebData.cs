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
            using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(_url))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("GetTextFromURL-Error: <color=red>" + unityWebRequest.error + "</color>\n");
                    _callback?.Invoke(false, unityWebRequest.error);
                }
                else
                {
                    // Show results as text
                    Debug.Log("GetTextFromURL-Result: " + unityWebRequest.downloadHandler.text + "\n");

                    // Or retrieve results as binary data
                    //byte[] results = www.downloadHandler.data;

                    _callback?.Invoke(true, unityWebRequest.downloadHandler.text);
                }
            }
        }

        /// <summary>
        /// Gets the text from URL.
        /// </summary>
        /// <param name="mono">Monobehavior</param>
        /// <param name="unityWebRequest">UnityWebRequest.</param>
        /// <param name="callback">Callback.</param>
        public static void GetTextFromPostRequest(this MonoBehaviour _mono, string _url, WWWForm _form,
            Action<bool, string> _callback = null)
        {
            _mono.StartCoroutine(_mono.GetTextFromPostRequestCoroutine(_url, _form, _callback));
        }

        /// <summary>
        /// Gets the text from URL coroutine.
        /// </summary>
        /// <returns>The text from URL coroutine.</returns>
        /// <param name="mono">Mono.</param>
        /// <param name="unityWebRequest">UnityWebRequest.</param>
        /// <param name="callback">Callback.</param>
        private static IEnumerator GetTextFromPostRequestCoroutine(this MonoBehaviour _mono, string _url,
            WWWForm _form, Action<bool, string> _callback)
        {
            Debug.Log("GetTextFromPostRequest-URL: <color=blue>" + _url + "</color>\n");

            using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(_url, _form))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("GetTextFromPostRequest-Error: <color=red>" + unityWebRequest.error + "</color>\n");
                    _callback?.Invoke(false, unityWebRequest.result.ToString());
                }
                else if (unityWebRequest.downloadHandler.text.Contains("403 Forbidden")
                         || unityWebRequest.downloadHandler.text.Contains("\"data\":{\"status\":403}"))
                {
                    Debug.Log("GetTextFromPostRequest-Access Error: <color=red>" +
                              unityWebRequest.downloadHandler.text + "</color>\n");
                    _callback?.Invoke(false, unityWebRequest.downloadHandler.text); // handle this data carefully
                    //_callback(false, _unityWebRequest.error);
                }
                else
                {
                    // Show results as text
                    Debug.Log("GetTextFromPostRequest-Result: " + unityWebRequest.downloadHandler.text + "\n");

                    // Or retrieve results as binary data
                    //byte[] results = www.downloadHandler.data;

                    _callback?.Invoke(true, unityWebRequest.downloadHandler.text);
                }
            }
        }
    }
}