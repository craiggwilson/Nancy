namespace Nancy.Tests.Unit.Elements
{
    using Nancy.Conventions;
    using Nancy.Elements;
    using Xunit;
    using FakeItEasy;

    public class DefaultElementGeneratorFixture
    {
        private ElementGenerationConventions conventions;
        private DefaultElementGenerator subject;

        public DefaultElementGeneratorFixture()
        {
            conventions = new ElementGenerationConventions();
            subject = new DefaultElementGenerator(conventions);
        }

        [Fact]
        public void Should_use_first_factory_that_returns_an_element()
        {
            var factories = A.CollectionOfFake<IElementFactory>(3);
            A.CallTo(() => factories[0].Create(A<ElementContext>.Ignored)).Returns(new TagElement("div"));
            A.CallTo(() => factories[1].Create(A<ElementContext>.Ignored)).Returns(new TextElement());
            A.CallTo(() => factories[2].Create(A<ElementContext>.Ignored)).Returns(null);

            conventions.Generator("Blah").CreateWith(factories[0]);
            conventions.Generator("Blah").CreateWith(factories[1]);
            conventions.Generator("Blah").CreateWith(factories[2]);

            subject.Generate(new ElementContext("Blah", "sdf", null, null)).ShouldBeOfType<TextElement>();
        }

        [Fact]
        public void Should_use_default_if_no_other_factories_match()
        {
            var factories = A.CollectionOfFake<IElementFactory>(3);
            A.CallTo(() => factories[0].Create(A<ElementContext>.Ignored)).Returns(null);
            A.CallTo(() => factories[1].Create(A<ElementContext>.Ignored)).Returns(null);
            A.CallTo(() => factories[2].Create(A<ElementContext>.Ignored)).Returns(new TextElement());

            conventions.Generator("Blah").CreateWith(factories[0]);
            conventions.Generator("Blah").CreateWith(factories[1]);
            conventions.Generator("Blah").ByDefault.CreateWith(factories[2]);

            subject.Generate(new ElementContext("Blah", "sdf", null, null)).ShouldBeOfType<TextElement>();
        }

        [Fact]
        public void Should_throw_ElementGenerationException_if_no_factories_can_create_the_element()
        {
            var exception = Record.Exception(() => subject.Generate(new ElementContext("Blah", "sdf", null, null)));
            exception.ShouldNotBeNull();
        }

        [Fact]
        public void Should_invoke_all_modifiers_after_an_element_has_been_created()
        {
            var factory = A.Fake<IElementFactory>();
            var modifiers = A.CollectionOfFake<IElementModifier>(3);

            conventions.Generator("Blah").ByDefault.CreateWith(factory);
            conventions.Generator("Blah").ModifyWith(modifiers[0]);
            conventions.Generator("Blah").ModifyWith(modifiers[1]);
            conventions.Generator("Blah").ModifyWith(modifiers[2]);

            subject.Generate(new ElementContext("Blah", "sdf", null, null));

            A.CallTo(() => modifiers[0].Modify(A<ElementContext>.Ignored, A<Element>.Ignored)).MustHaveHappened();
            A.CallTo(() => modifiers[1].Modify(A<ElementContext>.Ignored, A<Element>.Ignored)).MustHaveHappened();
            A.CallTo(() => modifiers[2].Modify(A<ElementContext>.Ignored, A<Element>.Ignored)).MustHaveHappened();
        }
    }
}