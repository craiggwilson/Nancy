namespace Nancy.Tests.Unit.Elements
{
    using System;
    using Nancy.Conventions;
    using Nancy.Elements;
    using Xunit;
    using FakeItEasy;
    using Nancy.Reflection;

    public class LambdaElementNameGeneratorFixture
    {
        private LambdaElementNameGenerator subject;

        public LambdaElementNameGeneratorFixture()
        {
            subject = new LambdaElementNameGenerator(a => "Blah");
        }

        [Fact]
        public void Should_use_function_to_create_name()
        {
            string name = subject.GenerateName(new ValueAccessor(typeof(string), "hmm", "s"));

            name.ShouldEqual("Blah");
        }
    }
}