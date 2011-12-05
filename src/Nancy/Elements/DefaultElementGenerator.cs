namespace Nancy.Elements
{
    using System.Collections.Generic;
    using System.Linq;
    using Nancy.Conventions;
    using Nancy.Reflection;

    /// <summary>
    /// The default element generator.
    /// </summary>
    public class DefaultElementGenerator : IElementGenerator
    {
        private readonly IElementNameGenerator nameGenerator;
        private readonly IEnumerable<IElementFactory> factories;
        private readonly IEnumerable<IElementModifier> modifiers;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultElementGenerator"/> class.
        /// </summary>
        /// <param name="factories">The factories.</param>
        /// <param name="modifiers">The modifiers.</param>
        public DefaultElementGenerator(ElementGenerationConventions conventions)
        {
            this.nameGenerator = conventions.NameGenerator;
            this.factories = conventions.GetFactories();
            this.modifiers = conventions.GetModifiers();
        }

        /// <summary>
        /// Generates an element for the given context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Element Generate(ElementContext context)
        {
            var element = this.factories
                .Select(x => x.Create(context))
                .FirstOrDefault(x => x != null);

            if (element == null)
                throw new ElementGenerationException(string.Format("No element factory was found for accessor {0}.", context.ElementName));

            foreach (var modifier in this.modifiers)
                modifier.Modify(context, element);

            return element;
        }

        /// <summary>
        /// Generates the name for an accessor.
        /// </summary>
        /// <param name="accessor"></param>
        /// <returns></returns>
        public string GenerateName(IAccessor accessor)
        {
            return this.nameGenerator.GenerateName(accessor);
        }
    }
}