namespace Nancy.Conventions
{
    using System;
    using System.Linq;
    using Nancy.Elements;
    using Nancy.Elements.Builders;

    /// <summary>
    /// The default element generation conventions.
    /// </summary>
    public class DefaultElementGenerationConventions : ElementGenerationConventions, IConvention
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultElementGenerationConventions"/> class.
        /// </summary>
        public DefaultElementGenerationConventions()
        {
            SetNameGenerator(new DefaultElementNameGenerator());

            Generator("Display")
                .ByDefault
                .CreateWith(c => new TagElement("span").Append(new TextElement().Text(c.StringValue)));

            Generator("Editor")
                .ByDefault
                .CreateWith(c => new TextBoxElement().Attribute("value", c.StringValue));

            Generator("Editor")
                .IfValueTypeIs<bool>()
                .CreateWith(c => new CheckBoxElement((bool)c.Value));

            Generator("Editor")
                .IfTagElement()
                .IfTagNameIs("input")
                .If((c, e) => !e.HasAttribute("name"))
                .ModifyWith((c, e) => e.Attribute("name", c.ElementName));

            Generator("Label")
                .ByDefault
                .CreateWith(c => new TagElement("label").Attribute("for", c.ElementName).Append(new TextElement().Text(c.Accessor.Names.Last())));
        }

        public void Initialise(NancyConventions conventions)
        {
            conventions.ElementGenerationConventions = this;
        }

        public System.Tuple<bool, string> Validate(NancyConventions conventions)
        {
            if (conventions.ElementGenerationConventions == null)
            {
                return Tuple.Create(false, "The element generation conventions cannot be null.");
            }

            if (conventions.ElementGenerationConventions.NameGenerator == null)
            {
                return Tuple.Create(false, "The name generator cannot be null.");
            }

            return Tuple.Create<bool, string>(true, string.Empty);
        }
    }
}