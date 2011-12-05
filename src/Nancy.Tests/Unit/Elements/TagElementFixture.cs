namespace Nancy.Tests.Unit.Elements
{
    using System.IO;
    using System.Text;
    using Nancy.Elements;
    using Xunit;

    public class TagElementFixture
    {
        private TagElement subject;

        public TagElementFixture()
        {
            subject = new TagElement("div");
        }

        [Fact]
        public void Should_render_with_attributes()
        {
            subject.Attribute("one", "two")
                .Id("awesome")
                .Data("test", "yes");

            RenderElement(subject).ShouldEqual("<div one=\"two\" id=\"awesome\" data-test=\"yes\"></div>");
        }

        [Fact]
        public void Should_render_with_children()
        {
            subject.Append(new TagElement("div"))
                .Append(new TagElement("table"));

            RenderElement(subject).ShouldEqual("<div><div></div><table></table></div>");
        }

        [Fact]
        public void Should_render_with_self_closing_tag()
        {
            subject.SelfClosing(true).Attribute("test", "funny");

            RenderElement(subject).ShouldEqual("<div test=\"funny\"/>");
        }

        [Fact]
        public void Should_render_after_siblings()
        {
            subject.After(new TagElement("span"))
                .Before(new TagElement("nope"));

            RenderElement(subject).ShouldEqual("<div></div><span></span>");
        }

        private string RenderElement(Element element)
        {
            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
                element.Render(writer);

            return sb.ToString();
        }
    }
}