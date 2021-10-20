/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MK.Common.Utilities
{ // NOTE: Performance wise, it is slow, but result wise might be better than the IEnumerator solution.
    // As the Update function is called via Reflection and it will be called even when the countDown is complete
    public class CountDownUpdate : MonoBehaviour
    {
        // Remember: MonoBehaviour is just to use its Update function, but still it should be on attached on some GameObject
        double count;
        double limit;
        bool increasing;
        bool keepCounting;
        Action<bool, string> onDone;

        [SerializeField] Text display;
        [SerializeField] TMP_Text displayTMP;

        public string TimeInText
        {
            get { return Utility.ConvertTime(count); }
        }

        public void StartCounting(double _count, double _limit, bool _increasing, Text _text,
            Action<bool, string> _onDone)
        {
            display = _text;
            StartCounting(_count, _limit, _increasing, _onDone);
        }

        public void StartCounting(double _count, double _limit, bool _increasing, TMP_Text _tmpText,
            Action<bool, string> _onDone)
        {
            displayTMP = _tmpText;
            StartCounting(_count, _limit, _increasing, _onDone);
        }

        public void StartCounting(double _count, double _limit, bool _increasing, Action<bool, string> _onDone)
        {
            ResetCounting(); // reset previous one first, just in case
            count = _count;
            limit = _limit;
            increasing = _increasing;
            keepCounting = true;
            onDone = _onDone;
            if (onDone != null) onDone.Invoke(keepCounting, TimeInText);
            SetDisplayTimer();
        }

        void ResetCounting()
        {
            keepCounting = false;
            count = 0;
        }

        public void StopCounting()
        {
            keepCounting = false;
        }

        void Update()
        {
            if (keepCounting)
            {
                SetDisplayTimer();
                if (increasing)
                    count += Time.deltaTime;
                else // decreasing
                    count -= Time.deltaTime;

                SetDisplayTimer();

                if (increasing && count >= limit)
                {
                    // increasing
                    keepCounting = false;
                }
                else if (!increasing && count <= limit)
                {
                    // decreasing
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
