namespace Nancy.Elements
{
    /// <summary>
    /// Creates elements.
    /// </summary>
    public interface IElementFactory
    {
        /// <summary>
        /// Creates an element for the given context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        Element Create(ElementContext context);
    }
}