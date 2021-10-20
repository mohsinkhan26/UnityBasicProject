/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using UnityEngine;

namespace MK.Common.Miscellaneous
{
    public class ManageTargetFrameRate : MonoBehaviour
    {
        [SerializeField] private bool setOnEnable = true;
        [SerializeField] private int framerate = 60;

        void OnEnable()
        {
            if (setOnEnable) SetTargetFrameRate(framerate);
        }

        public void SetTargetFrameRate(int _frameRate = -1)
        {
            // frame rate should be set between 10 and 60 OR -1 (default rate depends on the platform/device)
            if (_frameRate == -1 || (10 <= _frameRate && _frameRate <= 60))
                // https://docs.unity3d.com/ScriptReference/Application-targetFrameRate.html
                // VR Unity will use the target frame rate specified by the SDK and ignores values specified by the game
                Application.targetFrameRate = _frameRate;
        }
    }
}
