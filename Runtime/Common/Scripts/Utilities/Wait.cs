/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using UnityEngine;

namespace MK.Common.Utilities
{
    public static class Wait
    {
        private static WaitForEndOfFrame m_WaitForEndOfFrame = new WaitForEndOfFrame();
        private static WaitForFixedUpdate m_WaitForFixedUpdate = new WaitForFixedUpdate();

        public static WaitForEndOfFrame ForEndOfFrame
        {
            get { return m_WaitForEndOfFrame; }
        }

        public static WaitForFixedUpdate ForFixedUpdate
        {
            get { return m_WaitForFixedUpdate; }
        }
    }
}
