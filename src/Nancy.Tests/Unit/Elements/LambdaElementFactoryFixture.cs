namespace Nancy.Tests.Unit.Elements
{
    using System;
    using Nancy.Conventions;
    using Nancy.Elements;
    using Xunit;
    using FakeItEasy;
    using Nancy.Reflection;

    public class LambdaElementFactoryFixture
    {
        private LambdaElementFactory subject;

        public LambdaElementFactoryFixture()
        {
            subject = new LambdaElementFactory(c => new TextElement());
        }

        [Fact]
        public void Should_create_an_element_using_the_expression()
        {
            var result = subject.Create(null);
            result.ShouldNotBeNull();
        }
    }
}