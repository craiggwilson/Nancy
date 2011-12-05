namespace Nancy.Elements.Builders
{
    using System;

    /// <summary>
    /// Creates an element factory or element modifier conditionally.
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    public class ConditionalElementGeneratorExpression<TElement> : IElementGeneratorExpression<TElement> where TElement : Element
    {
        private readonly Func<ElementContext, bool> condition;
        private readonly Action<IElementFactory> addFactory;
        private readonly Action<IElementModifier> addModifier;

        private readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalElementGeneratorExpression&lt;TElement&gt;"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="addFactory">The add factory.</param>
        /// <param name="addModifier">The add modifier.</param>
        public ConditionalElementGeneratorExpression(Func<ElementContext, bool> condition, Action<IElementFactory> addFactory, Action<IElementModifier> addModifier)
        {
            this.condition = condition;
            this.addFactory = addFactory;
            this.addModifier = addModifier;
        }

        /// <summary>
        /// Instructs an element generator to construct an element with the factory.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public void CreateWith(IElementFactory factory)
        {
            this.addFactory(this.WrapFactory(factory));
        }

        /// <summary>
        /// Instructs and element generator to modify an element with the modifier.
        /// </summary>
        /// <param name="modifier">The modifier.</param>
        public void ModifyWith(IElementModifier modifier)
        {
            this.addModifier(this.WrapModifer(modifier));
        }

        private IElementFactory WrapFactory(IElementFactory factory)
        {
            return new ConditionalElementFactory(this.condition, factory);
        }

        private IElementModifier WrapModifer(IElementModifier modifier)
        {
            return new ConditionalElementModifier((c, e) => this.condition(c), modifier);
        }
    }
}