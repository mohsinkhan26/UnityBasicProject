/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MK.Common.Extensions
{
    public static class MiscExtension
    {
        const string CONTINUED = "...";

        #region String Functions

        /// <summary>
        /// Strings the compare. Case-Sensitive string comparison
        /// </summary>
        /// <returns><c>true</c>, if compare was strung, <c>false</c> otherwise.</returns>
        /// <param name="str">String.</param>
        /// <param name="_comparison">Comparison.</param>
        public static bool CaseSensitiveEquals(this string str, string _comparison)
        {
            if (str.Length == _comparison.Length &&
                str.IndexOf(_comparison, StringComparison.OrdinalIgnoreCase) > -1)
            {
                // compare their ASCII characters to confirm the characters are same in both strings, even case wise
                byte[] source = Encoding.ASCII.GetBytes(str);
                byte[] comparer = Encoding.ASCII.GetBytes(_comparison);
                for (int i = 0; i < source.Length; ++i)
                {
                    // character is different
                    if (source[i] != comparer[i]) return false;
                }

                // passed string is exactly same with the comparer
                return true;
            }

            return false;

            // return str.Length == _comparison.Length &&
            //        str.IndexOf(_comparison, StringComparison.OrdinalIgnoreCase) > -1;
            // return str.GetHashCode() == _comparison.GetHashCode();
        }

        public static string ConcatenateString<T>(this T sender, params string[] _params) where T : class
        {
            // makes efficient string concatenation
            StringBuilder sb = new StringBuilder(_params.Length > 0 ? _params[0] : "");
            for (int i = 1; i < _params.Length; ++i)
                sb.Append(_params[i]);
            return sb.ToString();
        }

        /// Reference: https://stackoverflow.com/questions/4713444/shortest-way-to-get-first-char-from-every-word-in-a-string
        /// less readable, but loop the string once.
        public static string FirstLettersOfSentence(this string _text)
        {
            return string.Join("",
                _text.Where((ch, index) => ch != ' '
                                           && (index == 0 || _text[index - 1] == ' '))).ToUpper();
        }

        public static string FirstPart(this string _text, char _separator)
        {
            return _text.Split(_separator)[0];
        }

        public static string FileNameWithoutExtension(this string _fileWithExtension)
        {
            return _fileWithExtension.Split('.')[0];
        }

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

        public static string RestrictTextToLimit(this string _message, int _limit, bool _useDots = true)
        {
            if (_message.Length > _limit)
            {
                // restrict too long text
                _message = _message.Substring(0, _limit);
                if (_useDots) _message += CONTINUED;
            }

            return _message;
        }

        public static void RestrictTextToLimit(this Text _text, string _message, int _limit, bool _useDots = true)
        {
            _text.text = _message.RestrictTextToLimit(_limit, _useDots);
        }

        public static void RestrictTextToLimit(this TMP_Text _tmpText, string _message, int _limit,
            bool _useDots = true)
        {
            _tmpText.text = _message.RestrictTextToLimit(_limit, _useDots);
        }

        #endregion String Functions

        public static float RoundDecimalDigits(this float _amount, int _digitsAfterDecimal = 1)
        {
            return (float) Math.Round(_amount, _digitsAfterDecimal,
                MidpointRounding.AwayFromZero); // to round the value to 1 digit after decimal
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
            return _code == 0 || _code == 1 || (200 <= _code && _code < 300); // success code range 0, 1, 200-299
        }

        #region Text Highlight

        public static string BoldText<T>(this T sender, string text)
        {
            return new StringBuilder("<b>").Append(text).Append("</b>").ToString();
        }

        public static string HighlightText<T>(this T sender, string _color, string _highlightedText)
        {
            return new StringBuilder("<color=").Append(_color).Append(">").Append(_highlightedText)
                .Append("</color> ").ToString();
        }

        public static string HighlightText<T>(this T sender, string _color, string _highlightedText, string _data)
        {
            return new StringBuilder("<color=").Append(_color).Append(">").Append(_highlightedText)
                .Append("</color> ").Append(_data).ToString();
        }

        public static string HighlightText<T>(this T sender, string _color, string _preData, string _highlightedText,
            string _data)
        {
            return new StringBuilder(_preData).Append("<color=").Append(_color).Append(">").Append(_highlightedText)
                .Append("</color> ").Append(_data).ToString();
        }

        public static string HighlightTextGreen<T>(this T sender, string _highlightedText, string _data)
        {
            return sender.HighlightText("green", _highlightedText, _data);
        }

        public static string HighlightTextRed<T>(this T sender, string _highlightedText, string _data)
        {
            return sender.HighlightText("red", _highlightedText, _data);
        }

        public static bool BaseHighlightColor(this string _colorCode, out Color _newColor)
        {
            if (_colorCode.IndexOf('#') > -1) // color has hash sign
            {
                return ColorUtility.TryParseHtmlString(_colorCode, out _newColor);
            }

            return ColorUtility.TryParseHtmlString(
                ("").ConcatenateString("#", _colorCode), out _newColor);
        }

        #endregion Text Highlight
    }
}
