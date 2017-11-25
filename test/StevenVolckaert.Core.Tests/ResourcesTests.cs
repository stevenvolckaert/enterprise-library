namespace StevenVolckaert.Tests
{
    using Xunit;

    public class ResourcesTests
    {
        [Fact]
        public void CanAccessResources()
        {
            var resourceManager = Resources.ResourceManager;

            Assert.NotNull(resourceManager);
            Assert.NotNull(Resources.ValueNullEmptyOrWhiteSpace);
        }
    }
}
