using System;
using UnityEngine;

namespace MK.Common.Utilities
{
    public class GameUtilities : MonoBehaviour
    {
        public static T ParseEnum<T>(int _value)
        {
            return ParseEnum<T>(_value.ToString());
        }

        public static T ParseEnum<T>(string _value)
        {
            return (T)Enum.Parse(typeof(T), _value, true);
        }

        public static void RemoveChildren(Transform _transform)
        {
            for (int i = _transform.childCount - 1; i >= 0; --i)
            {
                Destroy(_transform.GetChild(i).gameObject);
            }
        }

        public static void DisableChildren(Transform _transform)
        {
            for (int i = _transform.childCount - 1; i >= 0; --i)
            {
                _transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        public static Vector3 FindMidPoint(Vector3 v1, Vector3 v2)
        { // midpoint between v1 and v2
            return new Vector3((v1.x + v2.x) / 2.0f, (v1.y + v2.y) / 2.0f, (v1.z + v2.z) / 2.0f);
        }

        public static Vector3[] ParabolicPath(Vector3 _source, Vector3 _destination, bool _fromRight)
        {
            Vector3[] waypoints = new Vector3[3];
            waypoints[0] = _source;
            waypoints[1] = FindMidPoint(_source, _destination);
            if (_fromRight)
                waypoints[1] = new Vector3(waypoints[1].x, waypoints[1].y, waypoints[1].z + 10f);
            else
                waypoints[1] = new Vector3(waypoints[1].x, waypoints[1].y, waypoints[1].z - 10f);
            waypoints[2] = _destination;
            return waypoints;
        }

        #region UnixTime

        /// <summary>
        ///  Convert froms the unix(Epoc) time to the DataDate.
        /// </summary>
        /// <returns>The unix DataTime.</returns>
        /// <param name="unixTime">Unix time.</param>
        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTime);
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static long GetTimeFromNow(long _timeFromPast)
        {
            //Debug.Log("GetTimeFromNow: " + _timeFromPast + "\nUnixTime: " + GameUtils.FromUnixTime(_timeFromPast)
            //          + "\nNow: " + DateTime.Now.ToUniversalTime() + "\nReturn: " + ((long) (DateTime.Now.ToUniversalTime() - FromUnixTime(_timeFromPast)).TotalSeconds) );
            return ((long)(DateTime.Now.ToUniversalTime() - FromUnixTime(_timeFromPast)).TotalSeconds);
        }

        public static long GetTimeDifferenceInSeconds(long _fromTime, long _toTime)
        {
            long _diff = _fromTime - _toTime;
            return (_diff / 1000);  // converting milliseconds to seconds
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
            { // adds 4 random digits to the end and total length becomes 18
                timeStampNow += UnityEngine.Random.Range(1000, 10000).ToString();
                //timeStampNow += SystemInfo.deviceUniqueIdentifier;
            }
            return timeStampNow;
        }

        #endregion UnixTime
    }
}
