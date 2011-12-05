namespace Nancy.Elements.Builders
{
    using System;

    /// <summary>
    /// Creates an element factory or an element modifier.
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    public class ElementGeneratorExpression<TElement> : IElementGeneratorExpression<TElement> where TElement : Element
    {
        private readonly Action<IElementFactory> addFactory;
        private readonly Action<IElementModifier> addModifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementGeneratorExpression&lt;TElement&gt;"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        public ElementGeneratorExpression(Action<IElementFactory> addFactory, Action<IElementModifier> addModifier)
        {
            this.addFactory = addFactory;
            this.addModifier = addModifier;
        }

        /// <summary>
        /// Instructs an element generator to create an element with the factory.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public void CreateWith(IElementFactory factory)
        {
            this.addFactory(factory);
        }

        /// <summary>
        /// Instructs an element generator to modify and element with the modifier.
        /// </summary>
        /// <param name="modifier">The modifier.</param>
        public void ModifyWith(IElementModifier modifier)
        {
            this.addModifier(modifier);
        }
    }
}