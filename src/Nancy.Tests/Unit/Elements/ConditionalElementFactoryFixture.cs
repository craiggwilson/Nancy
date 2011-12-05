namespace Nancy.Tests.Unit.Elements
{
    using FakeItEasy;
    using Nancy.Elements;
    using Xunit;

    public class ConditionalElementFactoryFixture
    {
        private IElementFactory factory;
        private ConditionalElementFactory subject;

        public ConditionalElementFactoryFixture()
        {
            factory = A.Fake<IElementFactory>();
            subject = new ConditionalElementFactory(c => c.GeneratorKey == "Blah", factory);
        }

        [Fact]
        public void Should_not_call_factory_when_condition_is_not_met()
        {
            var elementContext = new ElementContext("Not", "something", null, null);
            subject.Create(elementContext);

            A.CallTo(() => factory.Create(A<ElementContext>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public void Should_return_null_when_condition_is_not_met()
        {
            var elementContext = new ElementContext("Not", "something", null, null);
            var result = subject.Create(elementContext);

            result.ShouldBeNull();
        }

        [Fact]
        public void Should_call_factory_when_condition_is_met()
        {
            var elementContext = new ElementContext("Blah", "something", null, null);
            subject.Create(elementContext);

            A.CallTo(() => factory.Create(A<ElementContext>.Ignored)).MustHaveHappened();
        }
    }
}