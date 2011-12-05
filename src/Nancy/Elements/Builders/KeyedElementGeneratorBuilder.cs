namespace Nancy.Elements.Builders
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Builds an element generator for a specified generator key.
    /// </summary>
    public class KeyedElementGeneratorBuilder : IElementGeneratorExpression<Element>
    {
        private readonly string key;
        private readonly List<IElementFactory> factories;
        private readonly List<IElementModifier> modifiers;
        private readonly List<IElementFactory> defaultFactories;
        private readonly List<IElementModifier> defaultModifiers;

        /// <summary>
        /// Indicates that an element should be created with factory if none other matches.
        /// </summary>
        public IElementFactoryExpression<Element> ByDefault
        {
            get { return new ConditionalElementFactoryExpression<Element>(c => c.GeneratorKey == this.key, this.defaultFactories.Add); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedElementGeneratorBuilder"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public KeyedElementGeneratorBuilder(string key)
        {
            this.key = key;
            this.factories = new List<IElementFactory>();
            this.modifiers = new List<IElementModifier>();
            this.defaultFactories = new List<IElementFactory>();
            this.defaultModifiers = new List<IElementModifier>();
        }

        /// <summary>
        /// Instructs an element generator to create an element with the factory.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public void CreateWith(IElementFactory factory)
        {
            this.factories.Insert(0, new ConditionalElementFactory(c => c.GeneratorKey == this.key, factory));
        }

        /// <summary>
        /// Gets the factories.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IElementFactory> GetFactories()
        {
            return this.factories.Concat(this.defaultFactories);
        }

        /// <summary>
        /// Gets the modifiers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IElementModifier> GetModifiers()
        {
            return this.modifiers.Concat(this.defaultModifiers);
        }

        /// <summary>
        /// Instructs an element generator to modify an element with the modifier.
        /// </summary>
        /// <param name="modifier">The modifier.</param>
        public void ModifyWith(IElementModifier modifier)
        {
            this.modifiers.Insert(0, new ConditionalElementModifier((c, e) => c.GeneratorKey == this.key, modifier));
        }
    }
}