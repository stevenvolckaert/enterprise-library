namespace StevenVolckaert.Tests
{
    using System;
    using Xunit;

    public class StringExtensionsTests
    {
        [Theory]
        // Lower case, no dashes, no braces.
        [InlineData("dfdc04eb167941cd8e23ef66d6cc756e")]
        // Upper case, no dashes, no braces.
        [InlineData("DFDC04EB167941CD8E23EF66D6CC756E")]
        // lower case, with dashes, no braces.
        [InlineData("dfdc04eb-1679-41cd-8e23-ef66d6cc756e")]
        // Upper case, with dashes, no braces.
        [InlineData("DFDC04EB-1679-41CD-8E23-EF66D6CC756E")]
        // Lower case, with dashes, with braces.
        [InlineData("{dfdc04eb-1679-41cd-8e23-ef66d6cc756e}")]
        // Upper case, with dashes, with braces.
        [InlineData("{DFDC04EB-1679-41CD-8E23-EF66D6CC756E}")]
        public void IsGuid_ReturnsTrue(string subject)
        {
            Assert.True(subject.IsGuid(), $"Expected string value '{subject}' to be a valid GUID.");
        }

        [Theory]
        // Contains invalid character ('g').
        [InlineData("gfdc04eb167941cd8e23ef66d6cc756e")]
        // Not enough characters.
        [InlineData("FDC04EB167941CD8E23EF66D6CC756E")]
        // Not enough characters.
        [InlineData("{dfdc04eb167941cd8e23ef66d6cc756}")]
        // Dash in the wrong place.
        [InlineData("DFDC04EB-16794-1CD-8E23EF66D6CC756E")]
        // A random string.
        [InlineData("Hello world!")]
        public void IsGuid_ReturnsFalse(string subject)
        {
            Assert.False(subject.IsGuid(), $"Expected string value '{subject}' to be an invalid GUID.");
        }

        public enum MockedEnumeration
        {
            Qux = 0,
            Foo,
            Bar,
        }

        public enum MockedEnumerationWithoutDefaultValue
        {
            Foo = 1,
            Bar = 2,
        }

        [Theory]
        [InlineData(MockedEnumeration.Foo, "Foo")]
        [InlineData(MockedEnumeration.Bar, "Bar")]
        public void ParseAs_Succeeds(MockedEnumeration expected, string subject)
        {
            Assert.Equal(expected, subject.TryParseAs<MockedEnumeration>());
        }

        [Theory]
        [InlineData(MockedEnumeration.Foo, "foo")]
        [InlineData(MockedEnumeration.Bar, "baR")]
        public void ParseAs_Fails(MockedEnumeration notExpected, string subject)
        {
            Assert.NotEqual(notExpected, subject.TryParseAs<MockedEnumeration>());
        }

        [Fact]
        public void ParseAs_ThrowsArgumentException()
        {
            // "Baz" is not a valid enumeration value of MockedEnumeration, so it should throw an exception.
            Assert.Throws<ArgumentException>(() => "Baz".ParseAs<MockedEnumeration>());
        }

        [Fact]
        public void ParseAs_ThrowsArgumentNullException()
        {
            string subject = null;
            Assert.Throws<ArgumentNullException>(() => subject.ParseAs<MockedEnumeration>());
        }

        [Theory]
        [InlineData(MockedEnumeration.Foo, "foo")]
        [InlineData(MockedEnumeration.Bar, "baR")]
        public void ParseAs_WhileIgnoringCase_Succeeds(MockedEnumeration expected, string subject)
        {
            Assert.Equal(expected, subject.TryParseAs<MockedEnumeration>(ignoreCase: true));
        }

        [Fact]
        public void ParseAs_WhileIgnoringCase_ThrowsArgumentException()
        {
            // "baz" is not a valid enumeration value of MockedEnumeration, so it should throw an exception.
            Assert.Throws<ArgumentException>(() => "baz".ParseAs<MockedEnumeration>(ignoreCase: true));
        }

        [Theory]
        [InlineData(MockedEnumeration.Foo, "Foo")]
        [InlineData(MockedEnumeration.Bar, "Bar")]
        [InlineData(MockedEnumeration.Qux, "Qux")]
        public void TryParseAs_Succeeds(MockedEnumeration expected, string subject)
        {
            Assert.Equal(expected, subject.TryParseAs<MockedEnumeration>());
        }

        [Theory]
        [InlineData(MockedEnumerationWithoutDefaultValue.Foo, "Foo")]
        [InlineData(MockedEnumerationWithoutDefaultValue.Bar, "Bar")]
        [InlineData(default(MockedEnumerationWithoutDefaultValue), "Baz")]
        public void TryParseAs_EnumerationWithoutDefaultValue_Succeeds(MockedEnumerationWithoutDefaultValue expected, string subject)
        {
            Assert.Equal(expected, subject.TryParseAs<MockedEnumerationWithoutDefaultValue>());
        }

        [Theory]
        [InlineData(MockedEnumeration.Foo, "Foo", MockedEnumeration.Foo)]
        [InlineData(MockedEnumeration.Bar, "Bar", MockedEnumeration.Foo)]
        [InlineData(MockedEnumeration.Foo, "Baz", MockedEnumeration.Foo)]
        [InlineData(MockedEnumeration.Bar, null, MockedEnumeration.Bar)]
        public void TryParseAs_WithDefaultResult_Succeeds(
            MockedEnumeration expected,
            string subject,
            MockedEnumeration defaultResult
        )
        {
            Assert.Equal(expected, subject.TryParseAs(defaultResult));
        }

        [Theory]
        [InlineData(MockedEnumeration.Foo, "foo")]
        [InlineData(MockedEnumeration.Bar, "bAR")]
        [InlineData(MockedEnumeration.Foo, "baz")]
        public void TryParseAs_WithDefaultResult_WhileIgnoringCase_Succeeds(
            MockedEnumeration expected,
            string subject
        )
        {
            Assert.Equal(
                expected: expected,
                actual: subject.TryParseAs(defaultResult: MockedEnumeration.Foo, ignoreCase: true)
            );
        }

        [Theory]
        [InlineData("Foo", " Foo    ")]
        [InlineData("Bar", "Bar")]
        [InlineData("Baz", " Baz")]
        public void TryTrim(string expected, string subject)
        {
            Assert.Equal(expected, subject.TryTrim());
        }

        [Fact]
        public void TryTrim_ReturnsNull()
        {
            string subject = null;
            Assert.Null(subject.TryTrim());
        }
    }
}
