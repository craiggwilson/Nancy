namespace Nancy.Elements
{
    using System;

    /// <summary>
    /// Creates an element based upon a condition.
    /// </summary>
    public class ConditionalElementFactory : IElementFactory
    {
        private readonly Func<ElementContext, bool> canCreate;
        private readonly IElementFactory inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalElementModifier"/> class.
        /// </summary>
        /// <param name="canCreate">The can create.</param>
        /// <param name="inner">The inner.</param>
        public ConditionalElementFactory(Func<ElementContext, bool> canCreate, IElementFactory inner)
        {
            this.canCreate = canCreate;
            this.inner = inner;
        }

        /// <summary>
        /// Creates the element.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Element Create(ElementContext context)
        {
            return this.canCreate(context)
                ? this.inner.Create(context)
                : null;
        }
    }
}