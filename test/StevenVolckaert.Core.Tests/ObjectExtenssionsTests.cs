namespace StevenVolckaert
{
    using System;
    using Xunit;

    public class ObjectExtensionsTests
    {
        [Serializable]
        private class SerializableType
        {
            public string Foo { get; set; }
            public int Bar { get; set; }
        }

        private class NonSerializableType
        {
            public int Baz { get; set; }
            public double Qux { get; set; }
        }

        //[Fact]
        //public void DeepCopy_ThrowsArgumentNullExceptionTest()
        //{
        //    SerializableType subject = null;
        //    Assert.Throws<ArgumentNullException>(testCode: () => subject.DeepCopy());
        //}

        //[Fact]
        //public void DeepCopy_ThrowsArgumentExceptionTest()
        //{
        //    var subject = new NonSerializableType { Baz = 123, Qux = 1.23 };
        //    Assert.Throws<ArgumentException>(testCode: () => subject.DeepCopy());
        //}

        //[Fact]
        //public void DeepCopyTest()
        //{
        //    var subject = new SerializableType { Foo = "Foo", Bar = 123 };
        //    var actual = subject.DeepCopy();

        //    Assert.NotStrictEqual(subject, actual);
        //}
    }
}
