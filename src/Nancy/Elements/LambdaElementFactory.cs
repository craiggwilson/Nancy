namespace Nancy.Elements
{
    using System;

    /// <summary>
    /// An element factory using delegates.
    /// </summary>
    public class LambdaElementFactory : IElementFactory
    {
        private readonly Func<ElementContext, Element> factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaElementFactory"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public LambdaElementFactory(Func<ElementContext, Element> factory)
        {
            this.factory = factory;
        }

        /// <summary>
        /// Creates an element for the given context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public Element Create(ElementContext context)
        {
            return this.factory(context);
        }
    }
}