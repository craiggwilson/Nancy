namespace Nancy.Tests.Unit.Elements
{
    using System;
    using FakeItEasy;
    using Nancy.Elements;
    using Nancy.Elements.Builders;
    using Xunit;

    public class ConditionalElementModifierExpressionFixture
    {
        private IElementModifier addedModifier;
        private ConditionalElementModifierExpression<TagElement> subject;

        public ConditionalElementModifierExpressionFixture()
        {
            subject = new ConditionalElementModifierExpression<TagElement>((c, e) => c.GeneratorKey == "Blah", m => addedModifier = m);
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