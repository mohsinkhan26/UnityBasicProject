/* 
 * Author : Mohsin Khan
 * Website : http://mohsinkhan.000webhostapp.com/
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