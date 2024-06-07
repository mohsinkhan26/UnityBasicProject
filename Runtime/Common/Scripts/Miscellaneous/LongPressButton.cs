/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace MK.Common.Miscellaneous
{
    [RequireComponent(typeof(EventTrigger))]
    public class LongPressButton : MonoBehaviour
    {
        private float startTime = 0f;
        [SerializeField] private float longPressTime = 5f;

        /// Attach function which you want to call
        [SerializeField] private UnityEvent onLongPress;

        /// Add under PointerDown in EventTrigger,
        /// simply to get the elapsed time for this phase
        public void OnPointerDown()
        {
            startTime = Time.realtimeSinceStartup;
        }

        /// Add under PointerUp in EventTrigger,
        /// Calculate and display the elapsed time
        public void OnPointerUp()
        {
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            if (elapsedTime >= longPressTime) onLongPress?.Invoke();
            // this.Log("OnPointerUp-longPress elapsedTime: " + elapsedTime + "   LongTime: " + longPressTime);
        }
    }
}