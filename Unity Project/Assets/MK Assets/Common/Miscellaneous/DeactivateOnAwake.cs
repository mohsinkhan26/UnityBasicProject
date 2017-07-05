using UnityEngine;

namespace MK.Common.Miscellaneous
{
    public class DeactivateOnAwake : MonoBehaviour
    {
        void Awake()
        {
            //Debug.Log("ActivationOnAwake-" + this.gameObject.name + ": " + activate.ToString());
            this.gameObject.SetActive(false);
        }
    }
}