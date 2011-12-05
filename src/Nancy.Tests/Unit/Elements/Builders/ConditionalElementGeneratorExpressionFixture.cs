namespace Nancy.Tests.Unit.Elements
{
    using System;
    using FakeItEasy;
    using Nancy.Elements;
    using Nancy.Elements.Builders;
    using Xunit;

    public class ConditionalElementGeneratorExpressionFixture
    {
        private IElementFactory addedFactory;
        private IElementModifier addedModifier;
        private ConditionalElementGeneratorExpression<TagElement> subject;

        public ConditionalElementGeneratorExpressionFixture()
        {
            subject = new ConditionalElementGeneratorExpression<TagElement>(c => c.GeneratorKey == "Blah", f => addedFactory = f, m => addedModifier = m);
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

        [Fact]
        public void Should_wrap_modifier_before_adding()
        {
            var modifier = A.Fake<IElementModifier>();
            subject.ModifyWith(modifier);

            addedModifier.ShouldNotBeNull();
            addedModifier.ShouldNotBeSameAs(modifier);
            addedModifier.ShouldBeOfType<ConditionalElementModifier>();
        }
    }
}