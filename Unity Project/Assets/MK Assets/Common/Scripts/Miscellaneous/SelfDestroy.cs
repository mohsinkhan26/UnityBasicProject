/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using UnityEngine;

namespace MK.Common.Miscellaneous
{
    public class SelfDestroy : MonoBehaviour
    {
        public float Life = 5.0f;

        protected virtual void Start()
        {
            Destroy(gameObject, Life);
        }
    }
}
