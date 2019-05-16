namespace StevenVolckaert
{
    using System;
    using System.Collections.Generic;
    using System.Text;
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
        [InlineData("Foo", MockedEnumerationWithoutDefaultValue.Foo)]
        [InlineData("Bar", MockedEnumerationWithoutDefaultValue.Bar)]
        [InlineData("Baz", default(MockedEnumerationWithoutDefaultValue))]
        public void TryParseAs_EnumerationWithoutDefaultValue_Succeeds(
            string subject
,
            MockedEnumerationWithoutDefaultValue expected)
        {
            Assert.Equal(expected, subject.TryParseAs<MockedEnumerationWithoutDefaultValue>());
        }

        [Theory]
        [InlineData("Foo", MockedEnumeration.Foo, MockedEnumeration.Foo)]
        [InlineData("Bar", MockedEnumeration.Bar, MockedEnumeration.Foo)]
        [InlineData("Baz", MockedEnumeration.Foo, MockedEnumeration.Foo)]
        [InlineData(null, MockedEnumeration.Bar, MockedEnumeration.Bar)]
        public void TryParseAs_WithDefaultResult_Succeeds(
            string subject,
            MockedEnumeration expected,
            MockedEnumeration defaultResult
        )
        {
            Assert.Equal(expected, subject.TryParseAs(defaultResult));
        }

        [Theory]
        [InlineData("foo", MockedEnumeration.Foo)]
        [InlineData("bAR", MockedEnumeration.Bar)]
        [InlineData("baz", MockedEnumeration.Foo)]
        public void TryParseAs_WithDefaultResult_WhileIgnoringCase_Succeeds(
            string subject,
            MockedEnumeration expected)
        {
            Assert.Equal(
                expected: expected,
                actual: subject.TryParseAs(defaultResult: MockedEnumeration.Foo, ignoreCase: true)
            );
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData(" ", "")]
        [InlineData(" Foo    ", "Foo")]
        [InlineData("Bar", "Bar")]
        [InlineData(" Baz", "Baz")]
        [InlineData("Qux ", "Qux")]
        public void TryTrim(string subject, string expected)
        {
            Assert.Equal(expected, subject.TryTrim());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("\rfoo bar baz", "foo bar baz")]
        [InlineData("\r\nfoo bar baz", "foo bar baz")]
        [InlineData("\nfoo bar baz", "foo bar baz")]
        [InlineData("\rfoo bar baz\r\r", "foo bar baz")]
        [InlineData("\rfoo bar baz\r\n\r\n", "foo bar baz")]
        [InlineData("\rfoo bar baz \n", "foo bar baz ")]
        [InlineData("foo bar baz\r\n", "foo bar baz")]
        [InlineData("foo bar baz\r", "foo bar baz")]
        [InlineData("foo bar baz\n", "foo bar baz")]
        [InlineData("foo bar baz\r\nqux", "foo bar baz\r\nqux")]
        [InlineData("foo bar \nbaz \nqux\n", "foo bar \nbaz \nqux")]
        public void TryTrimNewLine(string subject, string expected)
        {
            var actual = subject.TryTrimNewLine();
            Assert.Equal(expected, actual);
        }

        /// <summary>
        ///     Returns a sequence of data that links string values to its Base64-encoded equivalent.
        ///     Source @ https://www.base64encode.org/
        /// </summary>
        public static IEnumerable<object[]> StringToBase64Data()
        {
            yield return new object[] { "Foo", "Rm9v" };
            yield return new object[] { "BAR", "QkFS" };
            yield return new object[] { "baz", "YmF6" };
            yield return new object[] { "Lorem ipsum dolor sit amet...", "TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQuLi4=" };
            yield return new object[]
            {
                "Despite the constant negative ipsum covfefe. I will write some great placeholder text - and nobody writes better placeholder text than me, believe me - and I'll write it very inexpensively. I will write some great, great text on your website's Southern border, and I will make Google pay for that text. Mark my words. An 'extremely credible source' has called my office and told me that Barack Obama's placeholder text is a fraud. The best taco bowls are made in Trump Tower Grill. I love Hispanics! Lorem Ipsum is the single greatest threat. We are not - we are not keeping up with other websites.",
                "RGVzcGl0ZSB0aGUgY29uc3RhbnQgbmVnYXRpdmUgaXBzdW0gY292ZmVmZS4gSSB3aWxsIHdyaXRlIHNvbWUgZ3JlYXQgcGxhY2Vob2xkZXIgdGV4dCAtIGFuZCBub2JvZHkgd3JpdGVzIGJldHRlciBwbGFjZWhvbGRlciB0ZXh0IHRoYW4gbWUsIGJlbGlldmUgbWUgLSBhbmQgSSdsbCB3cml0ZSBpdCB2ZXJ5IGluZXhwZW5zaXZlbHkuIEkgd2lsbCB3cml0ZSBzb21lIGdyZWF0LCBncmVhdCB0ZXh0IG9uIHlvdXIgd2Vic2l0ZSdzIFNvdXRoZXJuIGJvcmRlciwgYW5kIEkgd2lsbCBtYWtlIEdvb2dsZSBwYXkgZm9yIHRoYXQgdGV4dC4gTWFyayBteSB3b3Jkcy4gQW4gJ2V4dHJlbWVseSBjcmVkaWJsZSBzb3VyY2UnIGhhcyBjYWxsZWQgbXkgb2ZmaWNlIGFuZCB0b2xkIG1lIHRoYXQgQmFyYWNrIE9iYW1hJ3MgcGxhY2Vob2xkZXIgdGV4dCBpcyBhIGZyYXVkLiBUaGUgYmVzdCB0YWNvIGJvd2xzIGFyZSBtYWRlIGluIFRydW1wIFRvd2VyIEdyaWxsLiBJIGxvdmUgSGlzcGFuaWNzISBMb3JlbSBJcHN1bSBpcyB0aGUgc2luZ2xlIGdyZWF0ZXN0IHRocmVhdC4gV2UgYXJlIG5vdCAtIHdlIGFyZSBub3Qga2VlcGluZyB1cCB3aXRoIG90aGVyIHdlYnNpdGVzLg=="
            };
        }

        [Theory]
        [MemberData(nameof(StringToBase64Data))]
        public void ToBase64Test(string value, string base64EncodedValue)
        {
            Assert.Equal(
                expected: base64EncodedValue,
                actual: value.ToBase64()
            );
        }

        [Theory]
        [MemberData(nameof(StringToBase64Data))]
        public void ToBase64WithEncodingTest(string value, string base64EncodedValue)
        {
            Assert.Equal(
                expected: base64EncodedValue,
                actual: value.ToBase64(Encoding.ASCII)
            );
        }

        [Theory]
        [MemberData(nameof(StringToBase64Data))]
        public void ParseAsBase64Test(string value, string base64EncodedValue)
        {
            Assert.Equal(
                expected: value,
                actual: base64EncodedValue.ParseAsBase64()
            );
        }

        [Theory]
        [MemberData(nameof(StringToBase64Data))]
        public void ParseAsBase64WithEncodingTest(string value, string base64EncodedValue)
        {
            Assert.Equal(
                expected: value,
                actual: base64EncodedValue.ParseAsBase64(Encoding.ASCII)
            );
        }
    }
}
