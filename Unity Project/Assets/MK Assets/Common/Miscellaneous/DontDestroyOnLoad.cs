/* 
 * Author : Mohsin Khan
 * Website : http://mohsinkhan.000webhostapp.com/
*/
using UnityEngine;

namespace MK.Common.Miscellaneous
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
