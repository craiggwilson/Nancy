namespace Nancy.Tests.Unit.Reflection
{
    using System.Collections.Generic;
    using System.Reflection;
    using Nancy.Reflection;
    using Xunit;

    public class PropertyAccessorFixture
    {
        private PropertyAccessor subject;

        public PropertyAccessorFixture()
        {
            subject = new PropertyAccessor(typeof(TestModel).GetProperty("Name"));
        }

        [Fact]
        public void Should_have_one_type()
        {
            subject.Types.ShouldHaveCount(1);
        }

        [Fact]
        public void Should_have_one_name()
        {
            subject.Names.ShouldHaveCount(1);
        }

        [Fact]
        public void Should_have_the_correct_value_type()
        {
            subject.ValueType.ShouldEqual(typeof(string));
        }

        [Fact]
        public void Should_get_value_for_single_member()
        {
            var test = new TestModel { Name = "Jack" };

            var result = (string)subject.GetValue(test);

            result.ShouldEqual("Jack");
        }

        [Fact]
        public void Should_set_value_for_single_member()
        {
            var test = new TestModel { Name = "Jack" };

            subject.SetValue(test, "Jimmy");

            test.Name.ShouldEqual("Jimmy");
        }

        public class TestModel
        {
            public string Name { get; set; }
        }
    }
}