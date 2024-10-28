/*
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 */

using NUnit.Framework;
using MK.Common.Extensions;
using MK.Common.Utilities;

namespace MK.Common
{
    public sealed class GenericTest
    {
        [Test]
        public void StringComparison_IsTrue()
        {
            Assert.IsTrue(("abc").CaseSensitiveEquals("abc"), "String comparison is NOT correct");
            Assert.IsTrue(("ABCDEF").CaseSensitiveEquals("ABCDEF"), "String comparison is NOT correct");
            Assert.IsTrue(("abcdefABCDEF").CaseSensitiveEquals("abcdefABCDEF"), "String comparison is NOT correct");
            Assert.IsTrue(("123456").CaseSensitiveEquals("123456"), "String comparison is NOT correct");
            Assert.IsTrue(("!@#$%^&*").CaseSensitiveEquals("!@#$%^&*"), "String comparison is NOT correct");
            Assert.IsTrue(("新增身體訊號").CaseSensitiveEquals("新增身體訊號"), "String comparison is NOT correct");
        }

        [Test]
        public void StringComparison_IsFalse()
        {
            Assert.IsFalse(("abcABC").CaseSensitiveEquals("abcabc"), "String comparison is NOT correct");
            Assert.IsFalse(("ABCabc").CaseSensitiveEquals("abcabc"), "String comparison is NOT correct");
            Assert.IsFalse(("ABCabc").CaseSensitiveEquals("abcABC"), "String comparison is NOT correct");
            Assert.IsFalse(("!@#$%^&*(").CaseSensitiveEquals("123456789"), "String comparison is NOT correct");
            Assert.IsFalse(("abc").CaseSensitiveEquals("abcABC"), "String comparison is NOT correct");
            Assert.IsFalse(("新增身體訊號").CaseSensitiveEquals("新增行為表現"), "String comparison is NOT correct");
        }

        [TestCase("This is my testing sentence")]
        public void StringFirstNamesTest(string str)
        {
            Assert.AreEqual(str.FirstLettersOfSentence(), "TIMTS", "Doesn't match");
        }

        [TestCase(-7, -5, -6)]
        [TestCase(8, 7, 7.5f)]
        [TestCase(-6, 3, -1.5f)]
        [TestCase(9, -2, 3.5f)]
        public void FindMidPointTest(float num1, float num2, float result)
        {
            Assert.AreEqual(GameUtilities.FindMidPoint(num1, num2), result, "Doesn't match");
        }
    }
}
