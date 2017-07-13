/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
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