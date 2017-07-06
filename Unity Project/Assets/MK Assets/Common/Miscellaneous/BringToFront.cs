/* 
 * Author : Mohsin Khan
 * Website : http://mohsinkhan.000webhostapp.com/
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