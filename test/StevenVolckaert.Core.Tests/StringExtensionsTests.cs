namespace StevenVolckaert.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class StringExtensionsTests
    {
        [Fact]
        public void IsGuid_ReturnsTrue()
        {
            var validGuidStrings = new List<string>
            {
                // Lower case, no dashes, no braces.
                "dfdc04eb167941cd8e23ef66d6cc756e",
                // Upper case, no dashes, no braces.
                "DFDC04EB167941CD8E23EF66D6CC756E",
                // lower case, with dashes, no braces.
                "dfdc04eb-1679-41cd-8e23-ef66d6cc756e",
                // Upper case, with dashes, no braces.
                "DFDC04EB-1679-41CD-8E23-EF66D6CC756E",
                // Lower case, with dashes, with braces.
                "{dfdc04eb-1679-41cd-8e23-ef66d6cc756e}",
                // Upper case, with dashes, with braces.
                "{DFDC04EB-1679-41CD-8E23-EF66D6CC756E}",
            };

            validGuidStrings.ForEach(
                x => Assert.True(x.IsGuid(), $"Expected string value '{x}' to be a valid GUID.")
            );
        }

        [Fact]
        public void IsGuid_ReturnsFalse()
        {
            var invalidGuidStrings = new List<string>
            {
                // Contains invalid character ('g').
                "gfdc04eb167941cd8e23ef66d6cc756e",
                // Not enough characters.
                "FDC04EB167941CD8E23EF66D6CC756E",
                // Not enough characters.
                "{dfdc04eb167941cd8e23ef66d6cc756}",
                // Dash in the wrong place.
                "DFDC04EB-16794-1CD-8E23EF66D6CC756E",
                // A random string.
                "Hello world!",
            };

            invalidGuidStrings.ForEach(
                x => Assert.False(x.IsGuid(), $"Expected string value '{x}' to be an invalid GUID.")
            );
        }

        public enum MockedEnumeration
        {
            Foo,
            Bar,
            Qux = 0

        }

        public enum MockedEnumerationWithoutDefaultValue
        {
            Foo = 1,
            Bar = 2,
        }

        [Fact]
        public void ParseAs_Succeeds()
        {
            Assert.Equal(MockedEnumeration.Foo, "Foo".ParseAs<MockedEnumeration>());
            Assert.Equal(MockedEnumeration.Bar, "Bar".ParseAs<MockedEnumeration>());
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
            string str = null;
            Assert.Throws<ArgumentNullException>(() => str.ParseAs<MockedEnumeration>());
        }

        [Fact]
        public void ParseAsWhileIgnoringCase_Succeeds()
        {
            Assert.Equal(MockedEnumeration.Foo, "foo".ParseAs<MockedEnumeration>(ignoreCase: true));
            Assert.Equal(MockedEnumeration.Bar, "bar".ParseAs<MockedEnumeration>(ignoreCase: true));
        }

        [Fact]
        public void ParseAs_WhileIgnoringCase_ThrowsArgumentException()
        {
            // "baz" is not a valid enumeration value of MockedEnumeration, so it should throw an exception.
            Assert.Throws<ArgumentException>(() => "baz".ParseAs<MockedEnumeration>(ignoreCase: true));
        }

        [Fact]
        public void TryParseAs_Succeeds()
        {
            Assert.Equal(MockedEnumeration.Foo, "Foo".TryParseAs<MockedEnumeration>());
            Assert.Equal(MockedEnumeration.Bar, "Bar".TryParseAs<MockedEnumeration>());
            Assert.Equal(MockedEnumeration.Qux, "Baz".TryParseAs<MockedEnumeration>());

            Assert.Equal(
                MockedEnumerationWithoutDefaultValue.Foo,
                "Foo".TryParseAs<MockedEnumerationWithoutDefaultValue>()
            );
            Assert.Equal(
                MockedEnumerationWithoutDefaultValue.Bar,
                "Bar".TryParseAs<MockedEnumerationWithoutDefaultValue>()
            );
            Assert.Equal(
                default(MockedEnumerationWithoutDefaultValue),
                "Baz".TryParseAs<MockedEnumerationWithoutDefaultValue>()
            );
        }

        [Fact]
        public void TryParseAs_WithDefaultResult_Succeeds()
        {
            Assert.Equal(MockedEnumeration.Foo, "Foo".TryParseAs(defaultResult: MockedEnumeration.Foo));
            Assert.Equal(MockedEnumeration.Bar, "Bar".TryParseAs(defaultResult: MockedEnumeration.Foo));
            Assert.Equal(MockedEnumeration.Foo, "Baz".TryParseAs(defaultResult: MockedEnumeration.Foo));

            string str = null;
            Assert.Equal(MockedEnumeration.Bar, str.TryParseAs(defaultResult: MockedEnumeration.Bar));
        }

        [Fact]
        public void TryParseAs_WithDefaultResult_WhileIgnoringCase_Succeeds()
        {
            Assert.Equal(
                MockedEnumeration.Foo,
                "foo".TryParseAs(defaultResult: MockedEnumeration.Foo, ignoreCase: true)
            );
            Assert.Equal(
                MockedEnumeration.Bar,
                "bar".TryParseAs(defaultResult: MockedEnumeration.Foo, ignoreCase: true)
            );
            Assert.Equal(
                MockedEnumeration.Foo,
                "baz".TryParseAs(defaultResult: MockedEnumeration.Foo, ignoreCase: true)
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
