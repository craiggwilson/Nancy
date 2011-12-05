namespace Nancy.Tests.Unit.Elements
{
    using FakeItEasy;
    using Nancy.Elements;
    using Xunit;

    public class ConditionalElementModifierFixture
    {
        private IElementModifier modifier;
        private ConditionalElementModifier subject;

        public ConditionalElementModifierFixture()
        {
            modifier = A.Fake<IElementModifier>();
            subject = new ConditionalElementModifier((c, e) => c.GeneratorKey == "Blah", modifier);
        }

        [Fact]
        public void Should_not_call_modifier_when_condition_is_not_met()
        {
            var element = new TextElement();
            var elementContext = new ElementContext("Not", "something", null, null);
            subject.Modify(elementContext, element);

            A.CallTo(() => modifier.Modify(A<ElementContext>.Ignored, element)).MustNotHaveHappened();
        }

        [Fact]
        public void Should_call_modifier_when_condition_is_met()
        {
            var element = new TextElement();
            var elementContext = new ElementContext("Blah", "something", null, null);
            subject.Modify(elementContext, element);

            A.CallTo(() => modifier.Modify(A<ElementContext>.Ignored, element)).MustHaveHappened();
        }
    }
}