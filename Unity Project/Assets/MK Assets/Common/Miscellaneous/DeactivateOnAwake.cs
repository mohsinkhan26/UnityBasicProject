/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
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