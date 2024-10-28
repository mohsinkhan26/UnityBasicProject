/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using PimDeWitte.UnityMainThreadDispatcher;

namespace MK.Common.Utilities
{
    public enum ElapseTask
    {
        GameStateFetchDelay,
        GameStateUpdateDelay,
        EducationEntityUpdateDelay,
        TileMapUpdateDelay,
        None
    }

    public sealed class TimeElapse : MonoBehaviour
    {
        private Dictionary<ElapseTask, ElapseData> elapseDataDictionary = new Dictionary<ElapseTask, ElapseData>();

        public void AddTimeElapse(ElapseTask _elapseTask, Action _action, float _timeToWait = 5f)
        {
            if (elapseDataDictionary.ContainsKey(_elapseTask))
            {
                elapseDataDictionary[_elapseTask].UpdateWaitTime(_timeToWait);
                return;
            }

            Action tempAction = _action;
            ElapseData elapseData = new ElapseData();
            elapseData.SetData(_elapseTask, tempAction, _timeToWait);
            elapseDataDictionary.Add(_elapseTask, elapseData);
        }

        public void UpdateTimer(ElapseTask _elapseTask)
        {
            if (!elapseDataDictionary.ContainsKey(_elapseTask)) return;

            elapseDataDictionary[_elapseTask].UpdateTimer();
        }

        public void UpdateAction(ElapseTask _elapseTask, Action _action)
        {
            if (_action == null) return;
            if (!elapseDataDictionary.ContainsKey(_elapseTask))
            {
                AddTimeElapse(_elapseTask, _action);
                return;
            }

            elapseDataDictionary[_elapseTask].UpdateAction(_action);
        }

        public void UpdateWaitTime(ElapseTask _elapseTask, float _timeToWait)
        {
            if (!elapseDataDictionary.ContainsKey(_elapseTask)) return;

            elapseDataDictionary[_elapseTask].UpdateWaitTime(_timeToWait);
        }

        private void Update()
        {
            if (elapseDataDictionary.Count == 0) return;

            List<ElapseData> elapseDataList = elapseDataDictionary.Values
                .OrderBy(e => e.RemainingTime).ToList();

            for (int i = elapseDataList.Count - 1; i >= 0; --i)
            {
                if (!elapseDataList[i].IsTimeElapsed) continue;
                if (elapseDataList[i].ActionToRunAfterWait == null) continue;

                // this.Log(elapseDataList.Count + " Executing...! " + elapseDataList[i].ElapseTask);

                // won't miss any run, but run with the latest data after the delay
                elapseDataList[i].ActionToRunAfterWait.Invoke();
                elapseDataDictionary.Remove(elapseDataList[i].ElapseTask);
                return;
            }
        }
    }

    public sealed class ElapseData
    {
        public ElapseTask ElapseTask;
        public float TimeToWait;
        public Action ActionToRunAfterWait;

        /// This is simply to get the elapsed time for this phase of AssetLoading.
        private float startTime;

        /// time elapsed according to wait time,
        /// can be used to restrict frequent update calls
        public bool IsTimeElapsed
        {
            get
            {
                float elapsedTime = Time.realtimeSinceStartup - startTime;

                if (elapsedTime > TimeToWait)
                {
                    UpdateTimer();
                    return true;
                }

                return false;
            }
        }

        /// Positive: time already passed RUN the action,  Negative: still time left to pass
        public float RemainingTime
        {
            get { return Time.realtimeSinceStartup - startTime; }
        }

        public void SetData(ElapseTask _elapseTask, Action _action, float _timeToWait = 5f)
        {
            ElapseTask = _elapseTask;
            TimeToWait = _timeToWait;
            ActionToRunAfterWait = null;
            ActionToRunAfterWait = _action;
            UpdateTimer();
        }

        /// just in case you want to update the timer
        public void UpdateTimer()
        {
            // startTime = Time.realtimeSinceStartup; // get_realtimeSinceStartup can only be called from the main thread
            UnityMainThreadDispatcher.Instance()?.Enqueue(() => { startTime = Time.realtimeSinceStartup; });
        }

        public void UpdateAction(Action _action)
        {
            ActionToRunAfterWait = null;
            ActionToRunAfterWait = _action;
            UpdateTimer();
        }

        /// update the wait timer
        public void UpdateWaitTime(float _timeToWait)
        {
            TimeToWait = _timeToWait;
            UpdateTimer();
        }
    }
}
/* // usage
[SerializeField] protected TimeElapse timeElapse;

public void UpdateGameState() {
    // restrict frequent update calls
    timeElapse.UpdateAction(ElapseTask.GameStateUpdateDelay, () => { UpdateDataOfUser(); });
}
*/
