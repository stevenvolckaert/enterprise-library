namespace StevenVolckaert.Globalization.Tests
{
    using Globalization;
    using Xunit;

    public class CultureManagerTests
    {
        /* TODO Implement the unit tests. For instance: Calling any of the SetCulture methods should return
         * the name of the culture after execution, which must equal the CurrentCultureName property of the
         * culture manager (after execution). Steven Volckaert. December 13, 2016.
         */

        //[Fact]
        public void CultureManagerTest()
        {
        }

        //[Fact]
        public void CultureManagerTest1()
        {
        }

        //[Fact]
        public void GetCultureInfoTest()
        {
        }

        //[Fact]
        public void GetNeutralCultureNameTest()
        {
        }

        //[Fact]
        public void IsCultureSupportedTest()
        {
        }

        //[Fact]
        public void IsSpecificCultureSupportedTest()
        {
        }

        //[Fact]
        public void IsCultureSelectedTest()
        {
        }

        //[Fact]
        public void SetCultureTest1()
        {
        }

        [Fact]
        public void SetCultureTest2()
        {
            var cultureManager =
                new CultureManager(
                    defaultCultureName: "en-US",
                    supportedCultureNames: new string[] { "en-US", "nl-BE" }
                );

            Assert.Equal("en-US", cultureManager.SetCulture("pt", "zh-Hans"));
            Assert.Equal("nl-BE", cultureManager.SetCulture("pt", "zh-Hans", "nl-BE"));

            // TODO Add additional asserts.
            // Steven Volckaert. December 13, 2016.
        }

        //[Fact]
        public void SetDefaultCultureTest()
        {
        }

        //[Fact]
        public void SetSpecificCultureTest()
        {
        }
    }
}
