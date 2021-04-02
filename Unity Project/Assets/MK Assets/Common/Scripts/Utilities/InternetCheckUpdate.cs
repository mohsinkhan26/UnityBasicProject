/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/

using System;
using UnityEngine;

namespace MK.Common.Utilities
{
    /// <summary>
    /// The only drawback is Update will keep calling, even when you are done
    /// It's better to remove/delete this component as you are done with checking the Internet
    /// </summary>
    public class InternetCheckUpdate : MonoBehaviour
    {
        [SerializeField] bool allowCarrierDataNetwork = true;
        const string pingAddress = "8.8.8.8"; // Google Public DNS server
        const float waitingTime = 2.0f;

        Ping ping;
        float pingStartTime;

        bool IsInternetAvailable;
        Action<bool, string> onComplete;

        public void IsConnectedToInternet(Action<bool, string> _onComplete)
        {
            onComplete = _onComplete;

            bool internetPossiblyAvailable;
            switch (Application.internetReachability)
            {
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    internetPossiblyAvailable = true;
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    internetPossiblyAvailable = allowCarrierDataNetwork;
                    break;
                default:
                    internetPossiblyAvailable = false;
                    break;
            }
            if (!internetPossiblyAvailable)
            {
                IsInternetAvailable = false;
                if (onComplete != null) onComplete.Invoke(IsInternetAvailable, "0ms");
                return;
            }
            ping = new Ping(pingAddress);
            pingStartTime = Time.time;
        }

        void Update()
        {
            if (ping != null)
            {
                bool stopCheck = true;
                if (ping.isDone)
                {
                    if (ping.time >= 0)
                    {
                        //Debug.Log("Ping: " + pingAddress + "  t: " + ping.time + "ms");
                        IsInternetAvailable = true;
                    }
                    else
                        IsInternetAvailable = false;
                }
                else if (Time.time - pingStartTime < waitingTime)
                    stopCheck = false;
                else
                    IsInternetAvailable = false;
                if (stopCheck)
                {
                    if (onComplete != null) onComplete.Invoke(IsInternetAvailable,
                        IsInternetAvailable ? ("Ping: " + pingAddress + "  t: " + ping.time + "ms") : "0ms");
                    ping = null;
                }
            }
        }
    }

    /*// Usage
    InternetCheckUpdate internetCheckUpdate;
    void CheckInternet()
    {
        internetCheckUpdate.IsConnectedToInternet((_isConnected, _pingDetail) =>
        {
            this.Log("IsConnectedToInternet: " + _isConnected + "   " + _pingDetail);
        });
    }*/
}
