namespace StevenVolckaert.Globalization.Tests
{
    using Globalization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CultureManagerTests
    {
        /* TODO Implement the unit tests. For instance: Calling any of the SetCulture methods should return the name
         * of the culture after execution, which must equal the CurrentCultureName property of the culture manager
         * (after execution). Steven Volckaert. December 13, 2016.
         */

        //[TestMethod]
        public void CultureManagerTest()
        {
            Assert.Fail();
        }

        //[TestMethod]
        public void CultureManagerTest1()
        {
            Assert.Fail();
        }

        //[TestMethod]
        public void GetCultureInfoTest()
        {
            Assert.Fail();
        }

        //[TestMethod]
        public void GetNeutralCultureNameTest()
        {
            Assert.Fail();
        }

        //[TestMethod]
        public void IsCultureSupportedTest()
        {
            Assert.Fail();
        }

        //[TestMethod]
        public void IsSpecificCultureSupportedTest()
        {
            Assert.Fail();
        }

        //[TestMethod]
        public void IsCultureSelectedTest()
        {
            Assert.Fail();
        }

        //[TestMethod]
        public void SetCultureTest1()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SetCultureTest2()
        {
            var cultureManager =
                new CultureManager(
                    defaultCultureName: "en-US",
                    supportedCultureNames: new string[] { "en-US", "nl-BE" }
                );

            Assert.AreEqual("en-US", cultureManager.SetCulture("pt", "zh-Hans"));
            Assert.AreEqual("nl-BE", cultureManager.SetCulture("pt", "zh-Hans", "nl-BE"));

            // TODO Add additional asserts.
            // Steven Volckaert. December 13, 2016.
        }

        //[TestMethod]
        public void SetDefaultCultureTest()
        {
            Assert.Fail();
        }

        //[TestMethod]
        public void SetSpecificCultureTest()
        {
            Assert.Fail();
        }
    }
}
