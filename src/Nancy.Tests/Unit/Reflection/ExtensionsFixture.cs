namespace Nancy.Tests.Unit.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Nancy.Reflection;
    using Xunit;
    using System.Linq.Expressions;

    public class ExtensionsFixture
    {
        [Fact]
        public void Should_return_accessor_for_single_member()
        {
            Expression<Func<TestModel, string>> expression = m => m.Name;

            var accessor = expression.ToAccessor();

            accessor.ShouldNotBeNull();

            accessor.Names.ShouldHaveCount(1);
        }

        [Fact]
        public void Should_return_accessor_for_multiple_members()
        {
            Expression<Func<TestModel, string>> expression = m => m.Child.ChildName;

            var accessor = expression.ToAccessor();

            accessor.ShouldNotBeNull();

            accessor.Names.ShouldHaveCount(2);
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