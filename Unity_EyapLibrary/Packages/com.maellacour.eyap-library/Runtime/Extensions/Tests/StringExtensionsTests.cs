namespace EyapLibrary.Extensions.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("foo", "bar", ExpectedResult = "foobar")]
        [TestCase("foob", "bar", ExpectedResult = "foobar")]
        [TestCase("foobar", "bar", ExpectedResult = "foobar")]
        [TestCase("foobar", "Bar", ExpectedResult = "foobarBar")]
        [TestCase("", "bar", ExpectedResult = "bar")]
        public string WithEndingTest(string value, string ending)
        {
            return value.WithEnding(ending);
        }

        [TestCase("bar", "foo", ExpectedResult = "foobar")]
        [TestCase("oobar", "foo", ExpectedResult = "foobar")]
        [TestCase("foobar", "foo", ExpectedResult = "foobar")]
        [TestCase("foobar", "Foo", ExpectedResult = "Foofoobar")]
        [TestCase("", "foo", ExpectedResult = "foo")]
        public string WithStartingTest(string value, string ending)
        {
            return value.WithStarting(ending);
        }

        [TestCase("FooBar", 3, ExpectedResult = "Foo")]
        [TestCase("foobar", 3, ExpectedResult = "foo")]
        [TestCase("Foo", 6, ExpectedResult = "Foo")]
        [TestCase("foo", 6, ExpectedResult = "foo")]
        public string LeftTest(string value, int length)
        {
            return value.Left(length);
        }

        [Test]
        public void LeftTestInvalidArguments()
        {
            string value = null;
            Assert.Throws<ArgumentNullException>(() => value.Left(4));
            Assert.Throws<ArgumentNullException>(() => value.Left(-3));
            value = "Foobar";
            Assert.Throws<ArgumentOutOfRangeException>(() => value.Left(-3));
        }

        [TestCase("FooBar", 3, ExpectedResult = "Bar")]
        [TestCase("foobar", 3, ExpectedResult = "bar")]
        [TestCase("Foo", 6, ExpectedResult = "Foo")]
        [TestCase("foo", 6, ExpectedResult = "foo")]
        public string RightTest(string value, int length)
        {
            return value.Right(length);
        }

        [Test]
        public void RightTestInvalidArguments()
        {
            string value = null;
            Assert.Throws<ArgumentNullException>(() => value.Right(4));
            Assert.Throws<ArgumentNullException>(() => value.Right(-3));
            value = "Foobar";
            Assert.Throws<ArgumentOutOfRangeException>(() => value.Right(-3));
        }
    }
}
