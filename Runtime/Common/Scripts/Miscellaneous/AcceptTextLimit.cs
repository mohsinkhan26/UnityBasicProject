/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MK.Common.Miscellaneous
{
    [RequireComponent(typeof(TMP_InputField))]
    public sealed class AcceptTextLimit : MonoBehaviour
    {
        [Tooltip("Accept more than this number, condition is greater than")]
        public int minimumTextLimit = 0;

        public TMP_InputField inputField;
        public bool setInStart = true;
        private ColorBlock colorBlock;
        private ColorBlock colorBlockTemp = new ColorBlock();
        public Color minimumLimitNotReached = new Color(0.933f, 0.325f, 0.313f); // light red color

        private void Start()
        {
            if (inputField == null)
                inputField = GetComponent<TMP_InputField>();
            colorBlock = inputField.colors;
            colorBlockTemp = inputField.colors;
            colorBlockTemp.normalColor = minimumLimitNotReached;
            colorBlockTemp.highlightedColor = minimumLimitNotReached;
            colorBlockTemp.pressedColor = minimumLimitNotReached;
            colorBlockTemp.selectedColor = minimumLimitNotReached;
            if (setInStart) OnTextValueChange();
        }

        public void OnTextValueChange()
        {
            inputField.colors =
                inputField.text.Length > minimumTextLimit ? colorBlock : colorBlockTemp;
        }
    }
}