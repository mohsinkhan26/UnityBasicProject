/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using UnityEngine;
using UnityEngine.UI;

namespace MK.Common.Miscellaneous
{
    public class Splash : MonoBehaviour
    {
        [SerializeField] float delay = 4f;
        [SerializeField] Image image;
        static bool flag = true;

        // Start is called before the first frame update
        void Start()
        {
            if (!flag)
            {
                Deactivate();
                return;
            }
            image.CrossFadeAlpha(0f, delay / 20f, true);
            Debug.Log("Splash Start");
            image.CrossFadeAlpha(1f, delay / 6f, true);
            Invoke("FadeImage", delay);
            flag = false;
        }

        void FadeImage()
        {
            image.CrossFadeAlpha(0f, delay / 4f, true);
            Invoke("Deactivate", delay / 3f);
        }

        void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
