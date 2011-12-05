namespace Nancy.Tests.Unit.Elements
{
    using System;
    using FakeItEasy;
    using Nancy.Elements;
    using Nancy.Elements.Builders;
    using Xunit;

    public class ConditionalElementFactoryExpressionFixture
    {
        private IElementFactory addedFactory;
        private ConditionalElementFactoryExpression<TagElement> subject;

        public ConditionalElementFactoryExpressionFixture()
        {
            subject = new ConditionalElementFactoryExpression<TagElement>(c => c.GeneratorKey == "Blah", f => addedFactory = f);
        }

        [Fact]
        public void Should_wrap_factory_before_adding()
        {
            var factory = A.Fake<IElementFactory>();
            subject.CreateWith(factory);

            addedFactory.ShouldNotBeNull();
            addedFactory.ShouldNotBeSameAs(factory);
            addedFactory.ShouldBeOfType<ConditionalElementFactory>();
        }
    }
}