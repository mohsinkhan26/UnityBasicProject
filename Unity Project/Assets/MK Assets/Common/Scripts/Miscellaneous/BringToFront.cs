/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using UnityEngine;

namespace MK.Common.Miscellaneous
{
    public class BringToFront : MonoBehaviour
    {
        void OnEnable()
        {
            transform.SetAsLastSibling();
        }
    }
}
