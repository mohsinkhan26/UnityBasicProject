/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/

using System;
using System.Text;

namespace MK.Common.Extensions
{
    public static class MiscExtension
    {
        /// <summary>
        /// Strings the compare. Case-Sensitive string comparison
        /// </summary>
        /// <returns><c>true</c>, if compare was strung, <c>false</c> otherwise.</returns>
        /// <param name="str">String.</param>
        /// <param name="_comparison">Comparison.</param>
        public static bool CaseSensitiveEquals(this string str, string _comparison)
        {
            return str.GetHashCode() == _comparison.GetHashCode();
        }

        public static string ConcatenateString<T>(this T sender, params string[] _params) where T : class
        { // makes efficient string concatination
            StringBuilder sb = new StringBuilder(_params.Length > 0 ? _params[0] : "");
            for (int i = 1; i < _params.Length; ++i)
                sb.Append(_params[i]);
            return sb.ToString();
        }

        public static string FileNameWithoutExtension(this string _fileWithExtension)
        {
            return _fileWithExtension.Split('.')[0];
        }

        public static float RoundDecimalDigits(this float _amount)
        {
            return (float)Math.Round(_amount, 1, MidpointRounding.AwayFromZero); // to round the value to 1 digit after decimal
        }

        /// <summary>
        /// Amount in words by rounding the decimal digits
        /// </summary>
        /// <param name="_amount"></param>
        /// <returns></returns>
        public static string RoundDecimalDigitsInWords(this float _amount)
        {
            return _amount.RoundDecimalDigits().ToString("0.0");
            //return _amount.ToString("0.00");
        }

        /// <summary>
        /// Amount in words without rounding decimal digits
        /// </summary>
        /// <param name="_amount"></param>
        /// <returns></returns>
        public static string AmountInWords(this float _amount)
        {
            return _amount.ToString("0.0");
            //return _amount.ToString("0.00");
        }

        public static bool IsErrorCode(this int _code)
        {
            if (500 <= _code) // server execution/fetch errors
                return true;
            else if (1 < _code && _code < 200) // server computation/logic errors
                return true;
            return false;
        }

        public static bool IsServerErrorCode(this int _code)
        {
            if (500 <= _code && _code < 600) // server execution/fetch errors
                return true;
            return false;
        }

        public static bool IsSuccessCode(this int _code)
        {
            return _code == 1 || (200 <= _code && _code < 300); // success code range 1, 200-299
        }
    }
}
