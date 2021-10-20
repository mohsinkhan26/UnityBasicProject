/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using MK.Common.Utilities;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using Newtonsoft.Json; // Git: com.unity.nuget.newtonsoft-json

namespace MK.Common.Helpers
{
    public sealed class ResourceHelper : Singleton<ResourceHelper>
    {
        private static Dictionary<string, UnityEngine.Object> CachedResources =
            new Dictionary<string, UnityEngine.Object>();

        #region Load Asynchronous

        private static IEnumerator LoadAsyncInternal(string _path, Action<UnityWebRequest> _onLoad)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(LocalToURLPath(_path)))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                _onLoad(webRequest);
            }
        }

        private static IEnumerator LoadAsyncInternal(string _path, Action<string> _onLoad)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(LocalToURLPath(_path));
            yield return webRequest.SendWebRequest();
            _onLoad(webRequest.downloadHandler.text);
        }

        public static T LoadCached<T>(string _path) where T : UnityEngine.Object
        {
            if (!CachedResources.ContainsKey(_path))
                CachedResources.Add(_path, Resources.Load<T>(_path));
            return CachedResources[_path] as T;
        }

        public static void LoadAsync(string _path, Action<UnityWebRequest> _onLoad)
        {
            Instance.StartCoroutine(LoadAsyncInternal(_path, _onLoad));
        }

        public static void LoadFromStreamingAssetsAsync(string _file, Action<UnityWebRequest> _onLoad)
        {
            LoadAsync(GetStreamingAssetsPath(_file), _onLoad);
        }

        public static void LoadFromPersistentDataAsync(string _file, Action<UnityWebRequest> _onLoad)
        {
            LoadAsync(GetPersistentDataPath(_file), _onLoad);
        }

        public static void LoadAsync(string _path, Action<string> _onLoad)
        {
            Instance.StartCoroutine(LoadAsyncInternal(_path, _onLoad));
        }

        public static void LoadFromStreamingAssetsAsync(string _file, Action<string> _onLoad)
        {
            LoadAsync(GetStreamingAssetsPath(_file), _onLoad);
        }

        public static void LoadFromPersistentDataAsync(string _file, Action<string> _onLoad)
        {
            LoadAsync(GetPersistentDataPath(_file), _onLoad);
        }

        #endregion Load Asynchronous

        #region Read

        public static byte[] ReadBytes(string _path)
        {
            // except txt, for all other files like PDF, doc, etc
            return File.ReadAllBytes(_path);
        }

        public static string Read(string _path, bool _encrypted = false)
        {
            if (_encrypted)
                return DecryptText(File.ReadAllText(_path));
            else
                return File.ReadAllText(_path);
        }

        public static T Read<T>(string _path, bool _encrypted = false)
        {
            return DecodeObject<T>(Read(_path, _encrypted));
        }

        public static byte[] ReadBytesFromPersistentData(string _file, string _extension)
        {
            return ReadBytesFromPersistentData(_file + _extension);
        }

        public static byte[] ReadBytesFromPersistentData(string _file)
        {
            // except txt, for all other files like PDF, doc, etc
            return ReadBytes(GetPersistentDataPath(_file));
        }

        public static string ReadFromPersistentData(string _file, bool _encrypted = false)
        {
            return Read(GetPersistentDataPath(_file), _encrypted);
        }

        public static T ReadFromPersistentData<T>(string _file, bool _encrypted = false)
        {
            return Read<T>(GetPersistentDataPath(_file), _encrypted);
        }

        public static byte[] ReadBytesFromStreamingAssets(string _file)
        {
            // except txt, for all other files like PDF, doc, etc
            return ReadBytes(GetStreamingAssetsPath(_file));
        }

        public static string ReadFromStreamingAssets(string _file, bool _encrypted = false)
        {
            return Read(GetStreamingAssetsPath(_file), _encrypted);
        }

        public static T ReadFromStreamingAssets<T>(string _file, bool _encrypted = false)
        {
            return Read<T>(GetStreamingAssetsPath(_file), _encrypted);
        }

        public static void ReadAsync(string _path, Action<string> _onRead, bool _encrypted = false)
        {
            if (_encrypted)
            {
                LoadAsync(_path,
                    (UnityWebRequest webRequest) => { _onRead(DecryptText(webRequest.downloadHandler.text)); });
            }
            else
            {
                LoadAsync(_path, (UnityWebRequest webRequest) => { _onRead(webRequest.downloadHandler.text); });
            }
        }

        public static void ReadAsync<T>(string _path, Action<T> _onRead, bool _encrypted = false)
        {
            ReadAsync(_path, (string _content) => { _onRead(DecodeObject<T>(_content)); }, _encrypted);
        }

        public static void ReadFromPersistentDataAsync(string _file, Action<string> _onRead, bool _encrypted = false)
        {
            ReadAsync(GetPersistentDataPath(_file), _onRead, _encrypted);
        }

        public static void ReadFromPersistentDataAsync<T>(string _file, Action<T> _onRead, bool _encrypted = false)
        {
            ReadAsync<T>(GetPersistentDataPath(_file), _onRead, _encrypted);
        }

        public static void ReadFromStreamingAssetsAsync(string _file, Action<string> _onRead, bool _encrypted = false)
        {
            ReadAsync(GetStreamingAssetsPath(_file), _onRead, _encrypted);
        }

        public static void ReadFromStreamingAssetsAsync<T>(string _file, Action<T> _onRead, bool _encrypted = false)
        {
            ReadAsync<T>(GetStreamingAssetsPath(_file), _onRead, _encrypted);
        }

        #endregion Read

        #region Save

        public static void Save(string _path, string _contents, bool _encrypted = false,
            Action<bool, bool, string> _onDone = null)
        {
            //Thread timer = new Thread(() =>
            //{
            CreateDirectoryIfNotExist(_path);
            if (_encrypted)
                File.WriteAllText(_path, EncryptText(_contents));
            else
                File.WriteAllText(_path, _contents);
            if (_onDone != null)
                _onDone(true, Exists(_path), "");

            //    Thread.CurrentThread.Abort();
            //});
            //timer.IsBackground = true;
            //timer.Start();
        }

        public static void Save(string _path, byte[] _bytesArray, Action<bool, bool, string> _onDone = null)
        {
            // except txt, for all other files like PDF, doc, etc
            //Thread timer = new Thread(() =>
            //{
            CreateDirectoryIfNotExist(_path);
            File.WriteAllBytes(_path, _bytesArray);
            if (_onDone != null)
                _onDone(true, Exists(_path), "");

            //    Thread.CurrentThread.Abort();
            //});
            //timer.IsBackground = true;
            //timer.Start();
        }

        public static void Save(string _path, object _contents, bool _encrypted = false,
            Action<bool, bool, string> _onDone = null)
        {
            Save(_path, EncodeObject(_contents), _encrypted, _onDone);
        }

        public static void SaveToPersistentData(string _file, string _contents, bool _encrypted = false,
            Action<bool, bool, string> _onDone = null)
        {
            Save(GetPersistentDataPath(_file), _contents, _encrypted, _onDone);
        }

        public static void SaveToPersistentData(string _file, object _contents, bool _encrypted = false,
            Action<bool, bool, string> _onDone = null)
        {
            Save(GetPersistentDataPath(_file), _contents, _encrypted, _onDone);
        }

        //public static void SaveToStreamingAssets(string _file, string _contents, bool _encrypted = false, Action<bool, bool, string> _onDone = null)
        //{ // writing to StreamingAssets on runtime is not allowed
        //    Save(GetStreamingAssetsPath(_file), _contents, _encrypted, _onDone);
        //}

        //public static void SaveToStreamingAssets(string _file, object _contents, bool _encrypted = false, Action<bool, bool, string> _onDone = null)
        //{ // writing to StreamingAssets on runtime is not allowed
        //    Save(GetStreamingAssetsPath(_file), _contents, _encrypted, _onDone);
        //}

        public static void CreateDirectoryIfNotExist(string _filePath)
        {
            string[] directories = _filePath.Split('/');
            int length = directories.Length;
            StringBuilder path = new StringBuilder();
            for (int i = 0; i < length; ++i)
            {
                path.Remove(0, path.Length);
                path.Append(_filePath.Substring(0, _filePath.IndexOf(directories[i])));
                if (Directory.Exists(path.ToString()))
                {
                    path.Append(directories[i]);
                }
                else if (path.Length > 1)
                {
                    Directory.CreateDirectory(path.ToString());
                }
            }
        }

        #endregion Save

        #region Exists

        public static bool Exists(string _path)
        {
            return File.Exists(_path);
        }

        public static bool ExistsInPersistentData(string _file, string _extension)
        {
            return ExistsInPersistentData(_file + _extension);
        }

        public static bool ExistsInPersistentData(string _file)
        {
            return Exists(GetPersistentDataPath(_file));
        }

        public static bool ExistsInStreamingAssets(string _file)
        {
            return Exists(GetStreamingAssetsPath(_file));
        }

        public static bool ExistsInStreamingAssets(string _file, string _extension)
        {
            return ExistsInStreamingAssets(_file + _extension);
        }

        public static void ExistsInStreamingAssetsAsync(string _file, string _extension, Action<bool> _isFileExists)
        {
            ExistsInStreamingAssetsAsync(_file + _extension, _isFileExists);
        }

        public static void ExistsInStreamingAssetsAsync(string _file, Action<bool> _isFileExists)
        {
            // specially for Android
            LoadFromStreamingAssetsAsync(_file,
                (_result) =>
                {
                    _isFileExists(string.IsNullOrEmpty(_result.error) && _result.isDone && _result.downloadedBytes > 0);
                });
        }

        public static void ExistsInPersistentDataAsync(string _file, Action<bool> _isFileExists)
        {
            // specially for Android
            LoadFromPersistentDataAsync(_file,
                (_result) =>
                {
                    _isFileExists(string.IsNullOrEmpty(_result.error) && _result.isDone && _result.downloadedBytes > 0);
                });
        }

        public static void ExistsInPersistentDataAsync(string _file, string _extension, Action<bool> _isFileExists)
        {
            ExistsInPersistentDataAsync(_file + _extension, _isFileExists);
        }

        #endregion Exists

        #region Delete

        public static void Delete(string _path)
        {
            File.Delete(_path);
        }

        public static void DeleteFromPersistentData(string _file)
        {
            Delete(GetPersistentDataPath(_file));
        }

        //public static void DeleteFromStreamingAssets(string _file)
        //{ // writing to StreamingAssets on runtime is not allowed
        //    Delete(GetStreamingAssetsPath(_file));
        //}

        #endregion Delete

        #region Get Path

        public static string GetPersistentDataPath(string _fileName, string _fileExtension)
        {
            string filePath = _fileName;
            //Debug.Log("<color=yellow>ResourceHelper-GetPersistentDataPath-</color>Name: " + _fileExtension + "   Condition: " + (filePath.IndexOf(_fileExtension) > -1 && filePath.IndexOf(_fileExtension) == (filePath.Length - _fileExtension.Length))+ "\nPath: " + filePath);
            if (filePath.IndexOf(_fileExtension) > -1 &&
                filePath.IndexOf(_fileExtension) == (filePath.Length - _fileExtension.Length))
            {
                // extension is already in the path
                filePath = GetPersistentDataPath(_fileName);
            }
            else
                filePath = GetPersistentDataPath(_fileName + _fileExtension);

            Debug.Log("<color=red>ResourceHelper-GetPersistentDataPath-</color>Name: " + _fileName + "\nPath: " +
                      filePath);
            return filePath;
        }

        public static string GetPersistentDataPath(string _file)
        {
#if UNITY_EDITOR
            Debug.Log("Unity Editor: " + Path.Combine(Application.persistentDataPath, _file));
            return Path.Combine(Application.persistentDataPath, _file);

#elif UNITY_ANDROID
            Debug.Log("Unity Android: " + (Path.Combine(Application.persistentDataPath, _file)));
            return Path.Combine(Application.persistentDataPath, _file);

#elif UNITY_IPHONE
            Debug.Log("Unity iPhone: " + Application.persistentDataPath);
            return Path.Combine(Application.persistentDataPath, _file);

#else
            Debug.Log("Any other platform: " + Path.Combine(Application.persistentDataPath, _file));
            return Path.Combine(Application.persistentDataPath, _file);
#endif
        }

        public static string GetStreamingAssetsPath(string _file)
        {
#if UNITY_EDITOR
            Debug.Log("Unity Editor: " + Application.dataPath + "/StreamingAssets");
            return Path.Combine(Application.dataPath + "/StreamingAssets", _file); // preferred way, but same as below
            //Debug.Log ("Unity Editor: " + Application.streamingAssetsPath);
            //return Path.Combine (Application.streamingAssetsPath, _file);

#elif UNITY_IPHONE
            Debug.Log("Unity iPhone: " + (Application.dataPath + "/Raw"));
            return Path.Combine(Application.dataPath + "/Raw", _file);

#elif UNITY_ANDROID
            Debug.Log("Unity Android: " + ("jar:file://" + Application.dataPath + "!/assets/"));
            return Path.Combine("jar:file://" + Application.dataPath + "!/assets/", _file);

#else
            Debug.Log("Any other platform: " + Application.streamingAssetsPath);
            return Path.Combine (Application.streamingAssetsPath, _file);
#endif
        }

        public static string GetStreamingAssetsPath(string _fileName, string _fileExtension)
        {
            string filePath = _fileName;
            //Debug.Log("<color=yellow>ResourceHelper-GetStreamingAssetsPath-</color>Name: " + _fileExtension + "   Condition: " + (filePath.IndexOf(_fileExtension) > -1 && filePath.IndexOf(_fileExtension) == (filePath.Length - _fileExtension.Length))+ "\nPath: " + filePath);
            if (filePath.IndexOf(_fileExtension) > -1 &&
                filePath.IndexOf(_fileExtension) == (filePath.Length - _fileExtension.Length))
            {
                // extension is already in the path
                filePath = GetStreamingAssetsPath(_fileName);
            }
            else
                filePath = GetStreamingAssetsPath(_fileName + _fileExtension);

            Debug.Log("<color=red>ResourceHelper-GetStreamingAssetsPath-</color>Name: " + _fileName + "\nPath: " +
                      filePath);
            return filePath;
        }

        public static string LocalToURLPath(string _path)
        {
            if (!_path.Contains("file://"))
                _path = "file://" + _path;
            return _path;
        }

        #endregion Get Path

        #region Encode and Decode Object

        public static T DecodeObject<T>(string _encoded)
        {
            // all files which are encrypted, must have to use this function to deserialize
            try
            {
                return _encoded.FromJSON<T>();
            }
            catch (JsonException _jEx)
            {
                // TODO: this need to fix, to ignore invisible characters from the start of JSON
                try
                {
                    return _encoded.Substring(_encoded.IndexOf('{')).FromJSON<T>();
                }
                catch (Exception _ex)
                {
                    // TODO: this need to fix, to ignore invisible characters from the start of JSON
                    Debug.LogError("JSON Parse Exception: " + _jEx.Message + "\n\nException: " + _ex.Message + "\n\n");
                    return _encoded.Substring(_encoded.IndexOf('[')).FromJSON<T>();
                }
                finally
                {
                    Debug.LogError("JSON Parse Exception: " + _jEx.Message + "\n\n");
                }
            }
            catch (Exception _ex)
            {
                // TODO: this need to fix, to ignore invisible characters from the start of JSON
                Debug.LogError("Exception: " + _ex.Message + "\n\n");
                return _encoded.Substring(_encoded.IndexOf('{')).FromJSON<T>();
            }
        }

        public static string EncodeObject(object _data)
        {
            return _data.ToJSON();
        }

        #endregion Encode and Decode Object

        #region FILE READ and WRITE

        public static void CopyFileFromStreamingToPersistentPath(string _functionCaller, string _fileName,
            string _fileExtension,
            bool _encrypted = false, Action<bool, bool, string> _onDone = null)
        {
            CopyFileFromStreamingToPersistentPath(_functionCaller, _fileName + _fileExtension, _encrypted, _onDone);
        }

        public static void CopyFileFromStreamingToPersistentPath(string _functionCaller, string _fileName,
            bool _encrypted = false, Action<bool, bool, string> _onDone = null)
        {
            try
            {
                if (ExistsInPersistentData(_fileName))
                {
                    if (_onDone != null) _onDone(true, true, "");
                }
                else
                {
                    // file not exists in persistent data, save it there
                    Debug.Log(_functionCaller + ": Copied " + _fileName +
                              " from streamingAssets to persistent data path");
                    // reading from local file should be Asynchronous call
                    ReadFromStreamingAssetsAsync(_fileName,
                        (readData) => { SaveToPersistentData(_fileName, readData, _encrypted, _onDone); }, false);
                }
            }
            catch (Exception _ex)
            {
                Debug.Log("CopyFileFromStreamingToPersistentPath-" + _functionCaller +
                          ": Exception occured on Copying " + _fileName + ": " + _ex.Message);
            }
        }

        #endregion FILE READ and WRITE

        #region ENCRYPT and DECRYPT

        // this is random string to DODGE which makes unbreakable data, just don't use numbers and capital letters in it
        const string PRE_STRING = "garrajdakgofoqamkficatauv";
        const string POST_STRING = "pugqyhnsdfinbamkkjxhuilsfdsf";

        public static string EncryptText(string _data)
        {
            //Debug.LogError ("EncryptText-Pre: " + PRE_STRING.Length + "   Post: " + POST_STRING.Length + "   Data: " + _data.Length);
            return (PRE_STRING + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_data.Trim())) +
                    POST_STRING);
            //return System.Convert.ToBase64String (System.Text.Encoding.UTF8.GetBytes (_data));
        }

        public static string DecryptText(string _data)
        {
            int _actualDataLength = _data.Length - POST_STRING.Length - PRE_STRING.Length;
            //Debug.LogError ("DecryptText-Pre: " + PRE_STRING.Length + "   Post: " + POST_STRING.Length + "   Data: " + _data.Length + "  => " + PRE_STRING.Length + "<=>" 
            //	+ _actualDataLength + "\n" + _data.Substring (PRE_STRING.Length, _actualDataLength));
            //return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(_data.Substring(PRE_STRING.Length, _actualDataLength)));
            return System.Text.Encoding.UTF8
                .GetString(System.Convert.FromBase64String(_data.Substring(PRE_STRING.Length, _actualDataLength)))
                .Trim();
            //string decryptedText = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(_data.Substring(PRE_STRING.Length, _actualDataLength))).Trim();
            //return decryptedText.Substring(decryptedText.IndexOf('{')); // TODO: this need to fix, to ignore invisible characters from the start of JSON
            //return new string(decryptedText.Where(c => !char.IsControl(c)).ToArray()); // Reference: https://stackoverflow.com/questions/15259275/removing-hidden-characters-from-within-strings
            //return new string(decryptedText.Where(c => char.IsLetterOrDigit(c)).ToArray()); // Reference: https://stackoverflow.com/questions/15259275/removing-hidden-characters-from-within-strings
            //return System.Text.Encoding.UTF8.GetString (System.Convert.FromBase64String (_data));
        }

        /// <summary>
        /// Calculates the SHA1 hash.
        /// </summary>
        /// <returns>The SHA1 hash.</returns>
        /// <param name="_data">Data.</param>
        public static string CalculateSHA1Hash(string _data)
        {
            // step 1, calculate MD5 hash from input
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(_data));
            // step 2, convert byte array to hex string
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        /// <summary>
        /// Determines MD5 whether the passed _data is valid and saved data is not modified.
        /// </summary>
        /// <returns><c>true</c> if the MD5 is valid and not modified; otherwise, <c>false</c>.</returns>
        /// <param name="_key">_key.</param>
        /// <param name="_data">_data.</param>
        public static bool IsValidSHA1Code(string _key, string _data)
        {
            string _savedMD5 = PlayerPrefs.GetString(_key, "");
            string _currentMD5 = CalculateSHA1Hash(_data);
            //Debug.LogError ("IsValidMD5Code-Saved: " + _savedMD5 + "   Current: " + _currentMD5 + "\nKey: " + _key + "   Data: " + _data);
            if (_savedMD5.Length < 2)
            {
                // if it is running first time
                PlayerPrefs.SetString(_key, _currentMD5);
                return true;
            }
            else if (_savedMD5.Length > 2 && !_savedMD5.Equals(_currentMD5))
            {
                // if the user has modified the data
                Debug.LogError("IsValidMD5Code-Saved is NOT EQUAL to Current: " + _currentMD5);
                PlayerPrefs.SetString(_key, _currentMD5);
                return false;
            }

            return true;
        }

        #endregion ENCRYPT and DECRYPT
    }
}