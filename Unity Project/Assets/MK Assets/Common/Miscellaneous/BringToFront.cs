/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
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