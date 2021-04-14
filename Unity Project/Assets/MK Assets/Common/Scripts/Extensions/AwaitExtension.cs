/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MK.Common.Extensions
{
    public static class AwaitExtension
    {
        public static TaskAwaiter GetAwaiter(this TimeSpan _timeSpan)
        {
            return Task.Delay(_timeSpan).GetAwaiter();
        }

        #region Invoke Await

        public static async void InvokeAwait(float _delayInSeconds, Action _action, Action _callBack = null)
        {
            await Task.Delay(TimeSpan.FromSeconds(_delayInSeconds));

            if (_action != null) _action.Invoke();

            if (_callBack != null) _callBack.Invoke();
        }

        public static async Task InvokeAwaitTask(float _delayInSeconds, Action _action, Action _callBack = null)
        {
            await Task.Delay(TimeSpan.FromSeconds(_delayInSeconds));

            if (_action != null) _action.Invoke();

            if (_callBack != null) _callBack.Invoke();
        }

        #endregion Invoke Await
    }
}
