namespace Nancy.Tests.Unit.Elements
{
    using System;
    using Nancy.Conventions;
    using Nancy.Elements;
    using Xunit;
    using FakeItEasy;
    using Nancy.Reflection;

    public class LambdaElementModifierFixture
    {
        private LambdaElementModifier subject;

        public LambdaElementModifierFixture()
        {
            subject = new LambdaElementModifier((c, e) => ((TextElement)e).Text(c.GeneratorKey));
        }

        [Fact]
        public void Should_create_an_element_using_the_expression()
        {
            var element = new TextElement();
            subject.Modify(new ElementContext("Blah", null, null, null), element);
            element.Text().ShouldEqual("Blah");
        }
    }
}