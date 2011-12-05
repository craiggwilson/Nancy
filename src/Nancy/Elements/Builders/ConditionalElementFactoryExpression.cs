namespace Nancy.Elements.Builders
{
    using System;

    /// <summary>
    /// Creates an element factory conditionally.
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    public class ConditionalElementFactoryExpression<TElement> : IElementFactoryExpression<TElement> where TElement : Element
    {
        private readonly Func<ElementContext, bool> condition;
        private readonly Action<IElementFactory> addFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalElementFactoryExpression&lt;TElement&gt;"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="addFactory">The add factory.</param>
        public ConditionalElementFactoryExpression(Func<ElementContext, bool> condition, Action<IElementFactory> addFactory)
        {
            this.condition = condition;
            this.addFactory = addFactory;
        }

        /// <summary>
        /// Adds an element to the generator.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public void CreateWith(IElementFactory factory)
        {
            this.addFactory(this.WrapFactory(factory));
        }

        private IElementFactory WrapFactory(IElementFactory factory)
        {
            return new ConditionalElementFactory(this.condition, factory);
        }
    }
}