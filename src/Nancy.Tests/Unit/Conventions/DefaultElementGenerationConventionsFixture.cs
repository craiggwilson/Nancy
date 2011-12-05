namespace Nancy.Tests.Unit.Conventions
{
    using System;
    using System.IO;
    using System.Text;
    using Xunit;
    using Nancy.Conventions;
    using Nancy.Reflection;
    using Nancy.Security;
    using Nancy.Elements;

    public class DefaultElementGenerationConventionsFixture
    {
        private DefaultElementGenerationConventions subject;
        private IElementGenerator generator;

        public DefaultElementGenerationConventionsFixture()
        {
            subject = new DefaultElementGenerationConventions();
            generator = new DefaultElementGenerator(subject);
        }

        [Fact]
        public void Should_generate_a_label()
        {
            var model = new TestModel { Name = "Jack" };
            var elementContext = new ElementContext("Label", "Name", model, typeof(TestModel).GetProperty("Name").ToAccessor());

            var element = generator.Generate(elementContext);

            RenderElement(element).ShouldEqual("<label for=\"Name\">Name</label>");
        }

        [Fact]
        public void Should_generate_a_display_when_a_value_does_not_exist()
        {
            var model = new TestModel();
            var elementContext = new ElementContext("Display", "Name", model, typeof(TestModel).GetProperty("Name").ToAccessor());

            var element = generator.Generate(elementContext);

            RenderElement(element).ShouldEqual("<span></span>");
        }

        [Fact]
        public void Should_generate_a_display_when_a_value_exists()
        {
            var model = new TestModel { Name = "Jack" };
            var elementContext = new ElementContext("Display", "Name", model, typeof(TestModel).GetProperty("Name").ToAccessor());

            var element = generator.Generate(elementContext);

            RenderElement(element).ShouldEqual("<span>Jack</span>");
        }

        [Fact]
        public void Should_generate_an_input_when_a_value_does_not_exist()
        {
            var model = new TestModel();
            var elementContext = new ElementContext("Editor", "Name", model, typeof(TestModel).GetProperty("Name").ToAccessor());

            var element = generator.Generate(elementContext);

            RenderElement(element).ShouldEqual("<input type=\"text\" value=\"\" name=\"Name\"></input>");
        }

        [Fact]
        public void Should_generate_an_input_when_a_value_exists()
        {
            var model = new TestModel { Name = "Jack" };
            var elementContext = new ElementContext("Editor", "Name", model, typeof(TestModel).GetProperty("Name").ToAccessor());

            var element = generator.Generate(elementContext);

            RenderElement(element).ShouldEqual("<input type=\"text\" value=\"Jack\" name=\"Name\"></input>");
        }

        [Fact]
        public void Should_generate_a_checkbox()
        {
            var model = new TestModel { IsAwesome = true };
            var elementContext = new ElementContext("Editor", "IsAwesome", model, typeof(TestModel).GetProperty("IsAwesome").ToAccessor());

            var element = generator.Generate(elementContext);

            RenderElement(element).ShouldEqual("<input type=\"checkbox\" checked=\"true\" name=\"IsAwesome\"></input>");
        }

        private string RenderElement(Element element)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
                element.Render(writer);

            return sb.ToString();
        }

        private class TestModel
        {
            public string Name { get; set; }

            public bool IsAwesome { get; set; }
        }

    }
}