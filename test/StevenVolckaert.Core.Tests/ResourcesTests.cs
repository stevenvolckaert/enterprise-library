namespace StevenVolckaert.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ResourcesTests
    {
        [TestMethod]
        public void CanAccessResources()
        {
            var resourceManager = Resources.ResourceManager;

            Assert.IsNotNull(resourceManager);
            Assert.IsNotNull(Resources.ValueNullEmptyOrWhiteSpace);
        }
    }
}
