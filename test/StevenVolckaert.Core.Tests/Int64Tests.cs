namespace StevenVolckaert
{
    using System;
    using Xunit;

    public class Int64Tests
    {
        [Fact]
        public void BytesToStringTest_ThrowsArgumentOutOfRangeException()
        {
            const long subject = -1;
            Assert.Throws<ArgumentOutOfRangeException>(testCode: () => subject.BytesToString());
        }

        [Theory]
        [InlineData("0 B", 0)]
        [InlineData("1 B", 1)]
        [InlineData("2 B", 2)]
        [InlineData("1000 B", 1000)]
        [InlineData("1 KiB", 1024)]
        [InlineData("1.5 KiB", 1536)]
        [InlineData("4 KiB", 4096)]
        [InlineData("976.6 KiB", 1000000)]
        [InlineData("1 MiB", 1048576)]
        [InlineData("953.7 MiB", 1000000000)]
        [InlineData("1 GiB", 1073741824)]
        [InlineData("931.3 GiB", 1000000000000)]
        [InlineData("1 TiB", 1099511627776)]
        [InlineData("909.5 TiB", 1000000000000000)]
        [InlineData("1 PiB", 1125899906842624)]
        [InlineData("88.8 PiB", 100000000000000000)]
        [InlineData("100 PiB", 112589990684262400)]
        public void BytesToStringBinaryTest(string expected, long subject)
        {
            Assert.Equal(expected, subject.BytesToString());
        }

        [Theory]
        [InlineData("0 B", 0)]
        [InlineData("1 B", 1)]
        [InlineData("2 B", 2)]
        [InlineData("1 kB", 1000)]
        [InlineData("1 kB", 1024)]
        [InlineData("1.5 kB", 1536)]
        [InlineData("4.1 kB", 4096)]
        [InlineData("1 MB", 1000000)]
        [InlineData("1 MB", 1048576)]
        [InlineData("1 GB", 1000000000)]
        [InlineData("1.1 GB", 1073741824)]
        [InlineData("1 TB", 1000000000000)]
        [InlineData("1.1 TB", 1099511627776)]
        [InlineData("1 PB", 1000000000000000)]
        [InlineData("1.1 PB", 1125899906842624)]
        [InlineData("100 PB", 100000000000000000)]
        [InlineData("112.6 PB", 112589990684262400)]
        public void BytesToStringDecimalTest(string expected, long subject)
        {
            Assert.Equal(expected, actual: subject.BytesToString(UnitOfInformationPrefix.Decimal));
        }
    }
}
