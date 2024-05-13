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
    [RequireComponent(typeof(TMP_Text))]
    public sealed class ShowAppVersion : MonoBehaviour
    {
        [SerializeField] private TMP_Text versionText;

        private void Awake()
        {
            Reset();
        }

        void OnEnable()
        {
            versionText.text = this.ConcatenateString("v",
                Application.version);
        }

        public void HideVersion()
        {
            versionText.transform.parent.gameObject.SetActive(false);
        }

        // runs only in editor automatically, when you apply this script
        private void Reset()
        {
            if (versionText == null)
                versionText = GetComponent<TMP_Text>();
        }
    }
}
