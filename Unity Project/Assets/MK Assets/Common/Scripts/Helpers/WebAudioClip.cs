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
    public static class WebAudioClip
    {
        /// <summary>
        /// Gets the audio clip from URL.
        /// </summary>
        /// <param name="_mono">_mono.</param>
        /// <param name="_url">Downloads the file from this _url.</param>
        /// <param name="_callback">Returns AudioClip in _callback, in success case</param>
        // / <param name="_error">Returns message in _error, in error case</param>
        /// <param name="_saveFile">If set to <c>true</c> _save file.</param>
        /// <param name="_filePathWithName">_file path with name.</param>
        /// <param name="_progressMessage">_progress message.</param>
        public static void GetAudioClipFromURL(this MonoBehaviour _mono, string _url,
            Action<AudioClip> _callback, Action<string> _error, AudioType _audioType = AudioType.OGGVORBIS,
            bool _saveFile = false, string _filePathWithName = "",
            Action<string> _progressMessage = null)
        {
            _mono.StartCoroutine(_mono.GetAudioClipFromURLCoroutine(_url, _callback, _error, _audioType,
                _saveFile, _filePathWithName, _progressMessage));
        }

        /// <summary>
        /// Gets the audio clip from URL coroutine.
        /// </summary>
        /// <returns>The audio clip from URL coroutine.</returns>
        /// <param name="_mono">_mono.</param>
        /// <param name="_url">Downloads the file from this _url.</param>
        /// <param name="_callback">Returns AudioClip in _callback, in success case</param>
        // / <param name="_error">Returns message in _error, in error case</param>
        /// <param name="_saveFile">If set to <c>true</c> _save file.</param>
        /// <param name="_filePathWithName">_file path with name.</param>
        /// <param name="_progressMessage">_progress message.</param>
        private static IEnumerator GetAudioClipFromURLCoroutine(this MonoBehaviour _mono, string _url,
            Action<AudioClip> _callback, Action<string> _error, AudioType _audioType, bool _saveFile,
            string _filePathWithName, Action<string> _progressMessage = null)
        {
            Debug.Log("GetAudioClipFromURLCoroutine-URL: <color=green>" + _url + "</color>\n");
            using (UnityWebRequest unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(_url, _audioType))
            {
                while (!unityWebRequest.isDone)
                {
                    if (_progressMessage != null)
                        _progressMessage("Status: Downloading Audio file... " +
                                         String.Format("{0:N}%", (unityWebRequest.downloadProgress * 100)));
                    yield return unityWebRequest.SendWebRequest();
                }

                if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log("GetAudioClipFromURLCoroutine-Error: <color=red>" + unityWebRequest.result +
                              "</color>\nSaveFile: " + _saveFile);
                    if (_error != null)
                        _error(unityWebRequest.result.ToString());
                }
                else
                {
                    if (_callback != null)
                        _callback(DownloadHandlerAudioClip.GetContent(unityWebRequest));
                    if (_saveFile && !string.IsNullOrEmpty(_filePathWithName) &&
                        (_filePathWithName.Contains("\\") || _filePathWithName.Contains("/")))
                    {
                        Debug.Log("WebAudioClip-GetAudioClipFromURLCoroutine-THREAD----START-FilePath: <color=cyan>" +
                                  _filePathWithName + "\nBytesLength: " + unityWebRequest.downloadedBytes + "</color>");
                        System.IO.File.WriteAllBytes(_filePathWithName, unityWebRequest.downloadHandler.data);
                        Debug.Log("WebAudioClip-GetAudioClipFromURLCoroutine-THREAD----DONE-FilePath: <color=cyan>" +
                                  _filePathWithName + "\nBytesLength: " + unityWebRequest.downloadHandler.data.Length +
                                  "</color>");
                        //System.Threading.Thread timer = new System.Threading.Thread (() =>
                        //{
                        //	System.IO.File.WriteAllBytes (_filePathWithName, www.bytes);
                        //	Debug.Log ("WebAudioClip-GetAudioClipFromURLCoroutine-THREAD----DONE-FilePath: <color=cyan>" + _filePathWithName + "\nBytesLength: " + www.bytes.Length + "</color>");
                        //	System.Threading.Thread.CurrentThread.Abort ();
                        //});
                        //timer.IsBackground = true;
                        //timer.Start ();
                    }

                    //System.IO.File.WriteAllBytes (pathToAudioData + "/" + "xy.ogg", www.bytes);
                    //_callback (www.GetAudioClip (true));
                }
            }
        }
    }
}