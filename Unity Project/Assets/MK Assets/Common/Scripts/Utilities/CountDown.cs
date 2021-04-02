/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MK.Common.Utilities
{ // NOTE: Performance wise, it is better than the IEnumerator solution, but result wise might have few seconds difference in case of bigger time.
    public class CountDown
    {
        IEnumerator countDownCoroutine;
        double count;
        double limit;
        bool keepCounting;
        Action<bool, string> onDone;

        Text display;
        TMP_Text displayTMP;

        public string TimeInText { get { return Utility.ConvertTime(count); } }

        public void StartCounting(MonoBehaviour _mono, double _count, double _limit, bool _increasing, Text _text, Action<bool, string> _onDone)
        {
            display = _text;
            StartCounting(_mono, _count, _limit, _increasing, _onDone);
        }

        public void StartCounting(MonoBehaviour _mono, double _count, double _limit, bool _increasing, TMP_Text _tmpText, Action<bool, string> _onDone)
        {
            displayTMP = _tmpText;
            StartCounting(_mono, _count, _limit, _increasing, _onDone);
        }

        public void StartCounting(MonoBehaviour _mono, double _count, double _limit, bool _increasing, Action<bool, string> _onDone)
        {
            StopCounting(_mono); // stop previous one first, just in case
            count = _count;
            limit = _limit;
            keepCounting = true;
            onDone = _onDone;
            countDownCoroutine = CountingCoroutine(_mono, _increasing, 1f);
            if (onDone != null) onDone.Invoke(keepCounting, TimeInText);
            SetDisplayTimer();
            _mono.StartCoroutine(countDownCoroutine);
        }

        public void StopCounting(MonoBehaviour _mono)
        {
            keepCounting = false;
            count = 0;
            if (countDownCoroutine != null)
                _mono.StopCoroutine(countDownCoroutine);
        }

        IEnumerator CountingCoroutine(MonoBehaviour _mono, bool _increasing, float _delay)
        {
            while (keepCounting)
            {
                SetDisplayTimer();
                yield return new WaitForSecondsRealtime(_delay);
                if (_increasing)
                    count += 1;
                else // decreasing
                    count -= 1;

                SetDisplayTimer();

                if (_increasing && count >= limit)
                { // increasing
                    keepCounting = false;
                }
                else if (!_increasing && count <= limit)
                { // decreasing
                    keepCounting = false;
                }
                if (onDone != null) onDone.Invoke(keepCounting, TimeInText);
            }
        }

        void SetDisplayTimer()
        {
            if (display != null)
                display.text = TimeInText;
            else if (displayTMP != null)
                displayTMP.text = TimeInText;
        }
    }
}
