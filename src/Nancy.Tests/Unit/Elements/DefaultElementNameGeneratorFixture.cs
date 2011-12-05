namespace Nancy.Tests.Unit.Elements
{
    using System;
    using Nancy.Conventions;
    using Nancy.Elements;
    using Xunit;
    using FakeItEasy;
    using Nancy.Reflection;

    public class DefaultElementNameGeneratorFixture
    {
        private DefaultElementNameGenerator subject;

        public DefaultElementNameGeneratorFixture()
        {
            subject = new DefaultElementNameGenerator();
        }

        [Fact]
        public void Should_return_correct_name_when_there_is_only_one_name()
        {
            var accessor = new ValueAccessor(typeof(string), "Blah", "yeah");
            var name = subject.GenerateName(accessor);

            name.ShouldEqual("Blah");
        }

        [Fact]
        public void Should_return_correct_name_when_there_is_more_than_one_name()
        {
            var accessor = new ValueAccessor(new [] { typeof(string), typeof(string) }, new [] { "Blah", "Again" }, "yeah");
            var name = subject.GenerateName(accessor);

            name.ShouldEqual("Blah.Again");
        }
    }
}