namespace StevenVolckaert.Web
{
    using System.Collections.Generic;
    using System.Web.UI;
    using Xunit;

    public class StateBagExtensionTests
    {
        [Fact]
        public void GetInt32Test()
        {
            var subject = new StateBag { { "foo", 1 } };
            Assert.Equal(expected: 1, actual: subject.Get<int>("foo"));
        }

        [Fact]
        public void GetInt32WithoutValueTest()
        {
            var subject = new StateBag();
            Assert.Equal(expected: 0, actual: subject.Get<int>("foo"));
        }

        [Fact]
        public void GetInt32WithDefaultValueTest()
        {
            var subject = new StateBag();
            Assert.Equal(expected: 2, actual: subject.Get<int>(key: "foo", defaultValue: 2));
        }

        [Fact]
        public void SetInt32Test()
        {
            var subject = new StateBag();
            subject.Set(1, key: "foo");
            Assert.Equal(expected: 1, actual: subject["foo"]);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetNullableInt32Test(int? value)
        {
            var subject = new StateBag { { "key", value } };
            Assert.Equal(expected: value, actual: subject.Get<int?>("key"));
        }

        [Fact]
        public void GetNullableInt32WithoutValueTest()
        {
            var subject = new StateBag();
            Assert.Null(subject.Get<int?>(key: "key"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void GetNullableInt32WithDefaultValueTest(int? value)
        {
            var subject = new StateBag();
            Assert.Equal(expected: value, actual: subject.Get(defaultValue: value, key: "key"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(100)]
        public void SetNullableInt32Test(int? value)
        {
            var subject = new StateBag();
            subject.Set(value, key: "key");
            Assert.Equal(expected: value, actual: subject["key"]);
        }

        // TODO add more Set*Tests

        [Fact]
        public void GetBooleanTest()
        {
            var subject = new StateBag { { "foo", true } };
            Assert.True(subject.Get<bool>(key: "foo"));
        }

        [Fact]
        public void GetBooleanWithoutValueTest()
        {
            var subject = new StateBag();
            Assert.False(subject.Get<bool>(key: "foo"));
        }

        [Fact]
        public void GetBooleanWithDefaultValueTest()
        {
            var subject = new StateBag();
            Assert.True(subject.Get<bool>(defaultValue: true, key: "foo"));
        }

        [Fact]
        public void GetStringTest()
        {
            var subject = new StateBag { { "foo", "bar" } };
            Assert.Equal(expected: "bar", actual: subject.Get<string>(key: "foo"));
        }

        [Fact]
        public void GetStringWithoutValueTest()
        {
            var subject = new StateBag();
            Assert.Null(subject.Get<string>(key: "foo"));
        }

        [Fact]
        public void GetStringWithDefaultValueTest()
        {
            var subject = new StateBag();
            Assert.Equal(expected: "bar", actual: subject.Get(defaultValue: "bar", key: "foo"));
        }

        public enum MockedEnumeration { Foo = 0, Bar, Baz }

        [Theory]
        [InlineData(MockedEnumeration.Foo)]
        [InlineData(MockedEnumeration.Bar)]
        [InlineData(MockedEnumeration.Baz)]
        public void GetEnumerationTest(MockedEnumeration value)
        {
            var subject = new StateBag { { "key", value } };
            Assert.Equal(expected: value, actual: subject.Get<MockedEnumeration>(key: "key"));
        }

        [Fact]
        public void GetEnumerationWithoutValueTest()
        {
            var subject = new StateBag();
            Assert.Equal(expected: MockedEnumeration.Foo, actual: subject.Get<MockedEnumeration>(key: "key"));
        }

        [Theory]
        [InlineData(MockedEnumeration.Foo)]
        [InlineData(MockedEnumeration.Bar)]
        [InlineData(MockedEnumeration.Baz)]
        public void GetEnumerationWithDefaultValueTest(MockedEnumeration value)
        {
            var subject = new StateBag();
            Assert.Equal(expected: value, actual: subject.Get(defaultValue: value, key: "key"));
        }

        [Theory]
        [InlineData(MockedEnumeration.Foo)]
        [InlineData(MockedEnumeration.Bar)]
        [InlineData(MockedEnumeration.Baz)]
        public void SetEnumerationTest(MockedEnumeration value)
        {
            var subject = new StateBag();
            subject.Set(value, key: "key");
            Assert.Equal(expected: value, actual: subject["key"]);
        }

        public class MockedComplexType
        {
            public string Foo { get; set; }
            public int Bar { get; set; }
            public MockedEnumeration Baz { get; set; }
        }

        /// <summary>
        ///     Returns a sequence of data that can be used to initialize instances of
        ///     the <see cref="MockedComplexType"/> class.
        /// </summary>
        public static IEnumerable<object[]> MockedComplexTypeData()
        {
            yield return new object[] { null };
            yield return new object[] { new MockedComplexType { Foo = "Foo", Bar = 1, Baz = MockedEnumeration.Baz } };
            yield return new object[] { new MockedComplexType { Foo = "Bar", Bar = 100, Baz = MockedEnumeration.Foo } };
        }

        [Theory]
        [MemberData(nameof(MockedComplexTypeData))]
        public void GetComplexTypeTest(MockedComplexType value)
        {
            var subject = new StateBag { { "key", value } };
            Assert.Equal(expected: value, actual: subject.Get<MockedComplexType>(key: "key"));
        }

        [Fact]
        public void GetComplexTypeWithoutValueTest()
        {
            var subject = new StateBag();
            Assert.Null(subject.Get<MockedComplexType>(key: "key"));
        }

        [Theory]
        [MemberData(nameof(MockedComplexTypeData))]
        public void GetComplexTypeWithDefaultValueTest(MockedComplexType value)
        {
            var subject = new StateBag();
            Assert.Equal(expected: value, actual: subject.Get(defaultValue: value, key: "key"));
        }
    }
}
