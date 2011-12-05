namespace Nancy.Tests.Unit.Reflection
{
    using System.Collections.Generic;
    using System.Reflection;
    using Nancy.Reflection;
    using Xunit;

    public class AccessorChainFixture
    {
        private AccessorChain subject;

        public AccessorChainFixture()
        {
            subject = new AccessorChain(
                typeof(TestModel).GetProperty("Child").ToAccessor(),
                typeof(TestChildModel).GetProperty("ChildName").ToAccessor());
        }

        [Fact]
        public void Should_have_two_types()
        {
            subject.Types.ShouldHaveCount(2);
        }

        [Fact]
        public void Should_have_types_in_the_correct_order()
        {
            subject.Types.ShouldEqualSequence(new[] { typeof(TestChildModel), typeof(string) });
        }

        [Fact]
        public void Should_have_two_names()
        {
            subject.Names.ShouldHaveCount(2);
        }

        [Fact]
        public void Should_have_names_in_the_correct_order()
        {
            subject.Names.ShouldEqualSequence(new[] { "Child", "ChildName" });
        }

        [Fact]
        public void Should_have_the_correct_value_type()
        {
            subject.ValueType.ShouldEqual(typeof(string));
        }

        [Fact]
        public void Should_get_value_for_from_nested_accessor()
        {
            var test = new TestModel { Name = "Jack", Child = new TestChildModel { ChildName = "Amy" } };

            var result = (string)subject.GetValue(test);

            result.ShouldEqual("Amy");
        }

        [Fact]
        public void Should_set_value_for_the_nested_accesor()
        {
            var test = new TestModel { Name = "Jack", Child = new TestChildModel { ChildName = "Amy" } };

            subject.SetValue(test, "Bob");

            test.Child.ChildName.ShouldEqual("Bob");
        }

        public class TestModel
        {
            public string Name { get; set; }

            public TestChildModel Child { get; set; }
        }

        public class TestChildModel
        {
            public string ChildName { get; set; }
        }
    }
}