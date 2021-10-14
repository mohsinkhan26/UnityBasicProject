/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using System;
using System.Diagnostics;

namespace MK.Common.Utilities
{
    public static class PerformanceTest
    {
        public static void CalculateExecutionTime(Action _action)
        {
            if (_action == null) return;
            int max = 10000000;
            var s1 = Stopwatch.StartNew();
            _action.Invoke(); // function to test
            s1.Stop();
            UnityEngine.Debug.Log("Execution Time: " +
                                  ((double) (s1.Elapsed.TotalMilliseconds * 1000000) / max)
                                  .ToString("0.00 ns")); // time is in nano seconds
        }
    }
}