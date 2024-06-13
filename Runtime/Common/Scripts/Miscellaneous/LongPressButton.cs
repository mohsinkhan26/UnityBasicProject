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
    // NOTE: tested on uGUI
    public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool isPointerDown = false;
        private float startTime = 0f;
        [SerializeField] private float longPressTime = 5f;
        [SerializeField] private UnityEvent onLongPress;
        [SerializeField] private UnityEvent<float> onLongPressValue; // returns time of press
        [SerializeField] private UnityEvent onLongPressContinue; // function will be called multiple times or once
        [SerializeField] private bool pointerDownContinueCallOnce = true;
        private bool pointerDownContinueCallOnceTemp = true;

        /// simply to get the elapsed time for this phase
        public void OnPointerDown(PointerEventData eventData)
        {
            startTime = Time.realtimeSinceStartup;
            isPointerDown = true;
            pointerDownContinueCallOnceTemp = true;
        }

        /// Calculate and display the elapsed time
        public void OnPointerUp(PointerEventData eventData)
        {
            isPointerDown = false;
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            if (elapsedTime < longPressTime) return;
            onLongPress?.Invoke();
            onLongPressValue?.Invoke(elapsedTime);
            // this.Log("OnPointerUp-elapsedTime: " + elapsedTime + "   LongTime: " + longPressTime);
        }

        /// Calculate and display the elapsed time, function will be called multiple times
        private void Update()
        {
            if (!isPointerDown) return; // don't run if pointer is not down already
            if (!pointerDownContinueCallOnceTemp) return; // function is set to be called once only
            float elapsedTime = Time.realtimeSinceStartup - startTime;
            if (elapsedTime < longPressTime) return;
            onLongPressContinue?.Invoke();
            if (pointerDownContinueCallOnce && pointerDownContinueCallOnceTemp)
            {
                pointerDownContinueCallOnceTemp = false;
            }

            // this.Log("Update-elapsedTime: "
            //          + elapsedTime + "   LongTime: " + longPressTime + "   Temp: " + pointerDownContinueCallOnceTemp);
        }
    }
}
