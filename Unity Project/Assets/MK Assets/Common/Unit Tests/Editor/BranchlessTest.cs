/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/

using NUnit.Framework;

namespace MK.Common
{
    public class BranchlessTest
    {
        #region Smaller Number

        private int SmallerNumber(int a, int b)
        {
            if (a < b) return a;
            else return b;
        }

        private int SmallerNumberBranchless(int a, int b)
        {
            // return a * (a < b) + b * (a >= b);
            return a * int.Parse((a < b).ToString()) + b * int.Parse((a >= b).ToString());
        }

        [TestCase(5, 6)]
        public void SmallerNumberBranchless_SmallerA(int a, int b)
        {
            Assert.AreEqual(a, SmallerNumberBranchless(a, b), "First number is not smaller");
        }

        [TestCase(6, 5)]
        public void SmallerNumberBranchless_SmallerB(int a, int b)
        {
            Assert.AreEqual(b, SmallerNumberBranchless(a, b), "Second number is not smaller");
        }

        [TestCase(5, 6)]
        public void SmallerNumber_SmallerA(int a, int b)
        {
            Assert.AreEqual(a, SmallerNumber(a, b), "First number is not smaller");
        }

        [TestCase(6, 5)]
        public void SmallerNumber_SmallerB(int a, int b)
        {
            Assert.AreEqual(b, SmallerNumber(a, b), "Second number is not smaller");
        }

        #endregion Smaller Number
    }
}