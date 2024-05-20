/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using UnityEngine;
using TMPro;
using MK.Common.Extensions;

namespace MK.Common.Miscellaneous
{
    public sealed class ShowAppVersion : MonoBehaviour
    {
        [SerializeField] private string bundleNumber = "0.0.1";
        [SerializeField] private TMP_Text versionText;

        private void Awake()
        {
            Reset();
        }

        void OnEnable()
        {
#if UNITY_IPHONE
            versionText.text = this.ConcatenateString("v", bundleNumber.Trim());
                // Application.version); // doesn't work in iOS
#else // for Editor & Android
            versionText.text = this.ConcatenateString("v", Application.version);
#endif
        }

        public void HideVersion()
        {
            gameObject.SetActive(false);
        }

        // runs only in editor automatically, when you apply this script
        private void Reset()
        {
            if (versionText == null)
                versionText = GetComponent<TMP_Text>();
        }
    }
}