/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace MK.Common.Miscellaneous
{
    public class TabularUI : MonoBehaviour
    {
        [SerializeField] Toggle[] tabs;
        [SerializeField] GameObject[] tabPanels;
        [SerializeField] int defaultTab;
        [SerializeField] TabEvent onClick;

        private void OnEnable()
        {
            ShowDefaultTab();
        }

        public void ShowDefaultTab()
        {
            if (tabs.Length < 2)
                this.LogError("You don't need tabs, as you added tabs less than 2");
            if (tabPanels.Length < 2)
                this.LogError("You don't need tab panels, as you added tab panels less than 2");
            tabs[0].isOn = (defaultTab == 0 ? true : false); // without this line you need to take care from the prefab
            tabs[1].isOn = (defaultTab == 1 ? true : false);
            ShowContentOfIndex(defaultTab);
            if (onClick != null) onClick.Invoke(defaultTab);
        }

        public void TabClicked(Toggle _toggle)
        {
            if (_toggle.isOn)
            {
                for (int i = tabs.Length - 1; i >= 0; --i)
                {
                    if (tabs[i].isOn)
                    {
                        ShowContentOfIndex(i);
                        if (onClick != null) onClick.Invoke(i);
                    }
                }
            }
        }

        void ShowContentOfIndex(int _index)
        {
            for (int i = tabPanels.Length - 1; i >= 0; --i)
            {
                tabPanels[i].SetActive(i == _index);
            }
        }


#if UNITY_EDITOR
        [ContextMenu("Switch To Tab 1")]
        void SwitchToTab1()
        {
            tabs[0].isOn = true;
        }

        [ContextMenu("Switch To Tab 2")]
        void SwitchToTab2()
        {
            tabs[1].isOn = true;
        }
#endif
    }

    [Serializable]
    public class TabEvent : UnityEvent<int>
    {
    }
}
