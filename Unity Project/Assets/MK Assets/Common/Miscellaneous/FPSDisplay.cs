/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
*/
using UnityEngine;

namespace MK.Common.Miscellaneous
{
    public class FPSDisplay : MonoBehaviour
    {
        float deltaTime = 0.0f;

        public Color textColor = new Color(1.0f, 0, 0);
        GUIStyle style = new GUIStyle();
        int w = Screen.width, h = Screen.height;
        Rect rect;

        void Start()
        {
            rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            //style.fontSize = h * 2 / 100;
            style.fontSize = h * 2 / 40;
            //style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
            style.normal.textColor = textColor;
        }

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }

        void OnGUI()
        {
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);
        }
    }
}

