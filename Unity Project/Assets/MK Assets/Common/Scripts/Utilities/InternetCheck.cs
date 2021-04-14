/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using System.Collections;
using UnityEngine;

namespace MK.Common.Utilities
{
    /// <summary>
    /// It's better to use this 
    /// </summary>
    public class InternetCheck
    {
        const bool allowCarrierDataNetwork = true;
        const string pingAddress = "8.8.8.8"; // Google Public DNS server
        const float waitingTime = 2.0f;

        Ping ping;
        float pingStartTime;

        bool IsInternetAvailable;
        Action<bool, string> onComplete;
        IEnumerator internetCheckCoroutine;

        public void IsConnectedToInternet(MonoBehaviour _mono, Action<bool, string> _onComplete)
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

            internetCheckCoroutine = InternetCheckCoroutine(_mono);
            _mono.StartCoroutine(internetCheckCoroutine);
            pingStartTime = Time.time;
        }

        IEnumerator InternetCheckCoroutine(MonoBehaviour _mono)
        {
            while (ping != null)
            {
                //yield return new WaitForSecondsRealtime(waitingTime);
                yield return new WaitForEndOfFrame();
                bool stopCheck = true;
                if (ping.isDone)
                {
                    if (ping.time >= 0)
                    {
                        //Debug.Log("Ping: " + pingAddress + "  t: " + ping.time);
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
                    if (onComplete != null)
                        onComplete.Invoke(IsInternetAvailable,
                            IsInternetAvailable ? ("Ping: " + pingAddress + "  t: " + ping.time + "ms") : "0ms");
                    ping = null;
                    _mono.StopCoroutine(internetCheckCoroutine);
                }
            }
        }
    }

    /*// Usage 1
    void CheckInternet()
    {
        new InternetCheck().IsConnectedToInternet(this, (_isConnected, _pingDetail) =>
        {
            this.Log("IsConnectedToInternet: " + _isConnected + "   " + _pingDetail);
        });
    }*/

    /*// Usage 2
    void CheckInternet()
    {
        InternetCheck internetCheck = new InternetCheck();
        internetCheck.IsConnectedToInternet(this, (_isConnected, _pingDetail) =>
        {
            this.Log("IsConnectedToInternet: " + _isConnected + "   " + _pingDetail);
        });
    }*/
}
