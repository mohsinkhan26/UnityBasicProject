/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MK.Common.Helpers
{
    /// <summary>
    /// Helper for toggle buttons to manage as group
    /// Attach this script to the parent GameObject containing many toggle buttons.
    /// </summary>
    public class ToggleSelection : MonoBehaviour
    {
        [SerializeField] private Toggle[] toggles;
        [SerializeField] private Text[] toggleTexts;

        public bool IsSelected
        {
            get { return toggles.Any(t => t.isOn); }
        }

        public string[] SelectedTexts
        {
            get
            {
                List<string> texts = new List<string>();
                if (!IsSelected) return texts.ToArray();

                for (int i = 0; i < toggles.Length; ++i)
                {
                    if (toggles[i].isOn)
                        texts.Add(toggleTexts[i].text);
                }

                // returns text arrays, in case of multiple selection is allowed
                // otherwise the data is on 0 index, in case of one selection
                return texts.ToArray();
            }
        }

        private void OnEnable()
        {
            UnSelectAll();
        }

        public void UnSelectAll()
        {
            ToggleBehaviours(false);
        }

        public void ToggleBehaviours(bool _enable)
        {
            for (int i = 0; i < toggles.Length; ++i)
                toggles[i].enabled = _enable;
        }

        [ContextMenu("Unselect All Toggles")]
        void UnselectAllToggles()
        {
            UnSelectAll();
        }

        [ContextMenu("Add Toggle Group")]
        void AddToggleGroup()
        {
            ToggleGroup toggleGroup = gameObject.GetComponent<ToggleGroup>();
            if (toggleGroup == null)
                toggleGroup = gameObject.AddComponent<ToggleGroup>();

            for (int i = 0; i < toggles.Length; ++i)
                toggles[i].group = toggleGroup;
        }
    }
}
