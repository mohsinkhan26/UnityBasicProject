/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using UnityEngine;
using Newtonsoft.Json; // Git: com.unity.nuget.newtonsoft-json

namespace MK.Common.Utilities
{
    public static class Utility
    {
        #region JSON conversions

        static JsonSerializerSettings jsonSerializerSettings;

        static JsonSerializerSettings JsonSerializerSettings
        {
            get
            {
                if (jsonSerializerSettings == null)
                    jsonSerializerSettings = new JsonSerializerSettings
                    {
                        //TypeNameHandling = TypeNameHandling.All, // it will add $type in the start of every object
                        Formatting = Formatting.None, // it will not add indentation
                        NullValueHandling = NullValueHandling.Include,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                return jsonSerializerSettings;
            }
        }

        //public static object ToDictionaryObject(this object _object, string _objectID)
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    dict.Add(_objectID, _object);
        //    return ((object)dict);
        //}

        public static Dictionary<string, object> ToDictionary(this object _object, string _objectID)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add(_objectID, _object);
            return dict;
        }

        /// <summary>
        /// To the object. If objectID not fund, then returns the defaul object of T type
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="_json">Json.</param>
        /// <param name="_objectID">Object identifier.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T PickSpecificObjectFromDictionary<T>(this string _json, string _objectID)
        {
            Dictionary<string, T> dict = FromJSON<Dictionary<string, T>>(_json);
            if (dict.ContainsKey(_objectID))
            {
                return dict[_objectID];
            }

            return default(T);
        }

        //public static string ToJSONDictionaryObject(this object _object, string _objectID)
        //{
        //    return JsonConvert.SerializeObject(_object.ToDictionaryObject(_objectID), JsonSerializerSettings);
        //}

        public static string ToJSONDictionary(this object _object, string _objectID)
        {
            return JsonConvert.SerializeObject(_object.ToDictionary(_objectID), JsonSerializerSettings);
        }

        public static string ToJSON(this object _object)
        {
            return JsonConvert.SerializeObject(_object, JsonSerializerSettings);
        }

        public static T FromJSON<T>(this string _json)
        {
            return JsonConvert.DeserializeObject<T>(_json, JsonSerializerSettings);
        }

        #endregion JSON conversions

        #region Unix Millisecond Timer

        public static ulong TimeInTicks(this ulong _timeInMilliseconds)
        {
            // 1 milliseconds == 10,000 ticks
            return _timeInMilliseconds / TimeSpan.TicksPerMillisecond;
            //return _timeInMilliseconds / 10000;
        }

        public static ulong TimeInSeconds(this ulong _timeInMilliseconds)
        {
            return _timeInMilliseconds / 1000;
        }

        public const string DATE_TIME_FORMAT = "yyyyMMddHHmmss";
        static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static ulong GetCurrentUnixTimestampMilliseconds()
        {
            return (ulong) (DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public static ulong GetUnixTimestampMilliseconds(DateTime? _dateTime)
        {
            // for current/local time pass DateTime.Now
            return (ulong) ((DateTime) _dateTime - UnixEpoch).TotalMilliseconds;
        }

        public static DateTime DateTimeFromUnixTimestampMilliseconds(ulong _milliseconds)
        {
            return UnixEpoch.AddMilliseconds(_milliseconds);
        }

        public static string DateTimeInFormat(ulong _milliseconds, string _format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTimeFromUnixTimestampMilliseconds(_milliseconds).ToString(_format);
        }

        public static string DateTimeCurrentInFormat(string _format = "yyyy-MM-dd HH:mm:ss")
        {
            return DateTimeFromUnixTimestampMilliseconds(GetCurrentUnixTimestampMilliseconds()).ToString(_format);
        }

        //public static ulong GetCurrentUnixTimestampSeconds()
        //{
        //    return (ulong)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
        //}

        //public static DateTime DateTimeFromUnixTimestampSeconds(ulong _seconds)
        //{
        //    return UnixEpoch.AddSeconds(_seconds);
        //}

        /// <summary>
        /// Gets the time from now. If return value is negative, then the time is from Future, otherwise it was from Past
        /// </summary>
        /// <returns>The time from now.</returns>
        /// <param name="_time">Time.</param>
        public static double GetTimeFromNow(ulong _time)
        {
            //Debug.Log("GetTimeFromNow: " + _timeFromPast + "\nUnixTime: " + GameUtils.FromUnixTime(_timeFromPast)
            //+ "\nNow: " + DateTime.Now.ToUniversalTime() + "\nReturn: " + ((long) (DateTime.Now.ToUniversalTime() - FromUnixTime(_timeFromPast)).TotalSeconds) );
            return ((DateTime.Now.ToUniversalTime() - DateTimeFromUnixTimestampMilliseconds(_time)).TotalSeconds);
        }

        public static ulong GetTimeDifferenceInSeconds(ulong _fromTime, ulong _toTime)
        {
            ulong _diff = _fromTime - _toTime;
            return (_diff / 1000); // converting milliseconds to seconds
        }

        public static string ConvertTime(double _seconds, bool _showMinSecWhenDays = false, bool _showMinutes = true,
            bool _showSeconds = true)
        {
            // this overload method don't care about the time whether it is in future or past
            bool timeFromPast = false;
            return ConvertTime(_seconds, ref timeFromPast, _showMinSecWhenDays, _showMinutes, _showSeconds);
        }

        public static string ConvertTime(double _seconds, ref bool _timeFromPast, bool _showMinSecWhenDays = false,
            bool _showMinutes = true, bool _showSeconds = true)
        {
            if (_seconds > 0) // past time
                _timeFromPast = true;
            else
            {
                // future time
                _timeFromPast = false;
                _seconds *= -1;
            }

            // if seconds are visible, then minutes should also be visible
            _showMinutes = _showSeconds ? _showSeconds : _showMinutes;

            string ToReturn = string.Empty;
            TimeSpan t = TimeSpan.FromSeconds(_seconds);

            bool Days = false;
            if (t.Days > 0)
            {
                ToReturn += string.Format((t.Days < 10) ? "{0}d " : "{0:D2}d ", t.Days);
                Days = true;
            }

            if (t.Hours > 0)
            {
                ToReturn += string.Format((t.Hours < 10) ? "{0}h " : "{0:D2}h ", t.Hours);
            }

            //if (t.Minutes > 0 && !Days)
            if (t.Minutes > 0 && _showMinutes && (!Days || _showMinSecWhenDays))
            {
                ToReturn += string.Format((t.Minutes < 10) ? "{0}m " : "{0:D2}m ", t.Minutes);
            }

            //if (t.Seconds > 0 && !Days && _showSeconds)
            if (t.Seconds > 0 && _showSeconds && (!Days || _showMinSecWhenDays))
            {
                ToReturn += string.Format((t.Seconds < 10) ? "{0}s" : "{0:D2}s", t.Seconds);
            }

            return ToReturn = ToReturn == string.Empty ? "0s" : ToReturn;
        }

        public static TimeSpan DateTimeInTimeSpanFromNow(DateTime _futureDateTime)
        {
            // if the time is in past, then negative value is return as Ticks
            DateTime dateTimeNow = DateTime.Now;
            DateTime dateTime = new DateTime(dateTimeNow.Year, dateTimeNow.Month, dateTimeNow.Day)
                .AddHours(dateTimeNow.Hour)
                .AddMinutes(dateTimeNow.Minute);
            return _futureDateTime.Subtract(dateTime);
        }

        public static double AddDays(double _milliseconds, int _daysToAdd)
        {
            // multiply with milliseconds in a day
            return (_milliseconds + (_daysToAdd * 86400000.0));
        }

        public static double AddHours(double _milliseconds, int _hoursToAdd)
        {
            // multiply with milliseconds in a hour
            return (_milliseconds + (_hoursToAdd * 3600000.0));
        }

        public static double AddMinutes(double _milliseconds, int _minutesToAdd)
        {
            // multiply with milliseconds in a minute
            return (_milliseconds + (_minutesToAdd * 60000.0));
        }

        public static double AddSeconds(double _milliseconds, int _secondsToAdd)
        {
            // multiply with milliseconds in a second
            return (_milliseconds + (_secondsToAdd * 1000.0));
        }

        #endregion Unix Millisecond Timer

        #region UnixTime

        /// <summary>
        ///  Convert froms the unix(Epoc) time to the DataDate.
        /// </summary>
        /// <returns>The unix DataTime.</returns>
        /// <param name="unixTime">Unix time.</param>
        public static DateTime FromUnixTime(long unixTime)
        {
            return UnixEpoch.AddMilliseconds(unixTime);
        }

        public static string FromUnixTimeInWords(long unixTime)
        {
            return UnixEpoch.AddMilliseconds(unixTime)
                .ToString("ddd, MMM dd, yyyy hh:mm tt"); // Wed, Jun 19, 2019 08:27 AM
        }

        public static ulong GetMillisecondsFromDateTime(string _dateTime)
        {
            DateTime dateTime;
            DateTime.TryParse(_dateTime, out dateTime);
            return (ulong) (dateTime - UnixEpoch).TotalMilliseconds;
        }

        public static long GetTimeFromNow(long _timeFromPast)
        {
            //Debug.Log("GetTimeFromNow: " + _timeFromPast + "\nUnixTime: " + GameUtils.FromUnixTime(_timeFromPast)
            //          + "\nNow: " + DateTime.Now.ToUniversalTime() + "\nReturn: " + ((long) (DateTime.Now.ToUniversalTime() - FromUnixTime(_timeFromPast)).TotalSeconds) );
            return ((long) (DateTime.Now.ToUniversalTime() - FromUnixTime(_timeFromPast)).TotalSeconds);
        }

        public static long GetTimeDifferenceInSeconds(long _fromTime, long _toTime)
        {
            long _diff = _fromTime - _toTime;
            return (_diff / 1000); // converting milliseconds to seconds
        }

        /// <summary>
        /// Gets the current time stamp with randomness.
        /// </summary>
        /// <returns>The current time stamp with randomness.</returns>
        /// <param name="flag">If set to <c>true</c> flag (Length=18), otherwise <c>false</c> (Length=14).</param>
        public static string GetCurrentTimeStampWithRandomness(bool flag = false)
        {
            //string timeStampNow = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string timeStampNow = DateTime.Now.ToString("yyyyMMddHHmmss"); // makes the string of length 14
            if (flag)
            {
                // adds 4 random digits to the end and total length becomes 18
                timeStampNow += UnityEngine.Random.Range(1000, 10000).ToString();
                //timeStampNow += SystemInfo.deviceUniqueIdentifier;
            }

            return timeStampNow;
        }

        public static bool DateIsAlreadyPassed(DateTime? _dateTime = null)
        {
            DateTime temp = _dateTime.Value;
            if (GetUnixTimestampMilliseconds(temp) <= GetCurrentUnixTimestampMilliseconds())
                return true; // past
            //Debug.LogError("Date selected is in Future!");
            return false; // future
        }

        #endregion UnixTime

        #region Data Path

        public static bool FileExistsAtPersistentDataPath(string _fileName)
        {
            return File.Exists(GetPersistentDataPath(_fileName));
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

        #endregion Data Path

        #region Text Highlight

        public static string HighlightTextGreen(string _highlightedText, string _data)
        {
            return HighlightText("green", _highlightedText, _data);
        }

        public static string HighlightTextRed(string _highlightedText, string _data)
        {
            return HighlightText("red", _highlightedText, _data);
        }

        public static string HighlightText(string _color, string _highlightedText, string _data)
        {
            return (new StringBuilder("<color=").Append(_color).Append(">").Append(_highlightedText)
                .Append(":</color> ").Append(_data).ToString());
        }

        #endregion Text Highlight

        #region Children GameObjects

        public static void RemoveChildren(Transform _transform)
        {
            for (int i = _transform.childCount - 1; i >= 0; --i)
            {
                MonoBehaviour.Destroy(_transform.GetChild(i).gameObject);
            }
        }

        public static void DisableChildren(Transform _transform)
        {
            for (int i = _transform.childCount - 1; i >= 0; --i)
            {
                _transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public static void DisableGameObjectsInList<T>(ref List<T> _list) where T : MonoBehaviour
        {
            if (_list == null || _list.Count == 0)
                return;
            for (int i = _list.Count - 1; i >= 0; --i)
            {
                _list[i].gameObject.SetActive(false);
            }
        }

        #endregion Children GameObjects

        #region Get Active Object

        public static T GetActiveObject<T>(ref T[] _maintainingArray) where T : MonoBehaviour
        {
            if (_maintainingArray.Length == 0 ||
                _maintainingArray.Where(ti => !ti.gameObject.activeInHierarchy).ToArray<T>().Length == 0)
                return null;

            T _item;
            _item = _maintainingArray.First(tItem => !tItem.gameObject.activeInHierarchy);
            _item.gameObject.SetActive(true);
            return _item;
        }

        public static T GetActiveObjectCreateIfNone<T>(ref List<T> _maintainingLst, Transform _parentObject = null,
            GameObject _prefabToInstantiate = null,
            string prefabPath = "", bool setParent = true) where T : MonoBehaviour
        {
            T _item;
            if (_maintainingLst == null)
            {
                Debug.LogError("Utility-GetActiveObjectCreateIfNone-Passed list is NULL: instantiating...");
                _maintainingLst = new List<T>();
            }

            if (_maintainingLst.Count == 0 ||
                _maintainingLst.Where(ti => !ti.gameObject.activeInHierarchy).ToList().Count == 0 ||
                _maintainingLst.Where(tItem =>
                        !tItem.gameObject.activeInHierarchy &&
                        tItem.name.Equals((_prefabToInstantiate.name + "(Clone)")))
                    .ToList().Count == 0)
            {
                GameObject itemClone =
                    (string.IsNullOrEmpty(prefabPath)
                        ? UnityEngine.Object.Instantiate(_prefabToInstantiate)
                        : Resources.Load(prefabPath)) as GameObject;
                if (setParent && _parentObject != null)
                    itemClone.transform.SetParent(_parentObject, false);
                _item = itemClone.GetComponent<T>();
                _maintainingLst.Add(_item);
            }
            else if (_prefabToInstantiate != null)
            {
                _item = _maintainingLst.First(tItem =>
                    !tItem.gameObject.activeInHierarchy && tItem.name.Equals((_prefabToInstantiate.name + "(Clone)")));
                if (setParent && _parentObject != null)
                    _item.transform.SetParent(_parentObject, false);
            }
            else
            {
                Debug.LogError("Utility-GetActiveObjectCreateIfNone: " + _prefabToInstantiate.name + "   Path: " +
                               prefabPath + "\nSomething wierd happened");
                _item = _maintainingLst.First(ti => !ti.gameObject.activeInHierarchy);
                if (setParent && _parentObject != null)
                    _item.transform.SetParent(_parentObject, false);
            }

            _item.gameObject.SetActive(true);
            return _item;
        }

        #endregion Get Active Object

        #region String Functions

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static bool StringIsNew(List<string> _list, string _newString)
        {
            if (_list.Count == 0)
                return true;
            return !_list.Exists(str => str.Equals(_newString));
        }

        public static List<string> AddPrefixToList(List<string> _stringList, string _prefixToAdd)
        {
            List<string> temp = new List<string>();
            for (int i = _stringList.Count - 1; i >= 0; --i)
                temp.Add(_prefixToAdd + _stringList[i]);
            return temp;
        }

        #endregion String Functions

        public static Sprite GetSpriteFromTexture2D(Texture2D _texture)
        {
            return Sprite.Create(_texture, new Rect(0.0f, 0.0f, _texture.width, _texture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
        }

        /// <summary>
        /// Rotates the increment. If max is 3, then the return values would be 0, 1, 2, 0, 1, 2,...
        /// </summary>
        /// <returns>The increment.</returns>
        /// <param name="_max">Max.</param>
        /// <param name="_current">Current.</param>
        public static int RotateIncrement(this int _max, int _current)
        {
            return (_current + 1) % _max;
        }

        public static bool IsValidEmail(string _email)
        {
            try
            {
                //string expresion = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)* @((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
                //string expresion = "\\w+([-+._']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                //string expresion = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                //string expresion = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                string expresion = @"^([0-9a-zA-Z]" + //Start with a digit or alphabetical
                                   @"([\+\-_\.][0-9a-zA-Z]+)*" + // No continuous or ending +-_. chars in email
                                   @")+" +
                                   @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$";
                if (Regex.IsMatch(_email, expresion))
                {
                    var addr = new MailAddress(_email);
                    return addr.Address == _email;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public static bool IsConnectedToInternet
        {
            get
            {
#if UNITY_EDITOR || UNITY_STANDALONE
                try
                {
                    using (var client = new WebClient())
                    {
                        using (var stream = client.OpenRead("http://www.google.com"))
                        {
                            return true;
                        }
                    }
                }
                catch
                {
                    return false;
                }
#else
                return Application.internetReachability != NetworkReachability.NotReachable;
#endif
            }
        }
    }
}
