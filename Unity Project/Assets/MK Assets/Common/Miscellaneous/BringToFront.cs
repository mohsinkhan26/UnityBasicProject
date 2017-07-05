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