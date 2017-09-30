/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
*/
using UnityEngine;

namespace MK.Common.Helpers
{
    /// <summary>
    /// Inherit your scripts from ToggleBehaviour, specially on UIs
    /// Instead of enable/disable the while gameObject with canvas, make the Canvas component and other custom components enable/disable
    /// as it is a slow process and internally a lot of stuff executes against it
    /// </summary>
    public class ToggleBehaviour : MonoBehaviour
    {
        [Header("Toggle Behaviour")]
        [SerializeField]
        Behaviour[] behaviours;

        /// <summary>
        /// Toggles the behaviours.
        /// </summary>
        /// <param name="_enable">If set to <c>true</c> enable.</param>
        public void ToggleBehaviours(bool _enable)
        {
            for (int i = behaviours.Length - 1; i >= 0; --i)
                behaviours[i].enabled = _enable;
        }
    }
}
