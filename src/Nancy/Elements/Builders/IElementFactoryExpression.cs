namespace Nancy.Elements.Builders
{
    /// <summary>
    /// Expression indicating the element generator to create an element using the specified factory.
    /// </summary>
    public interface IElementFactoryExpression
    {
        /// <summary>
        /// Instructs an element generator to create an element with the factory.
        /// </summary>
        /// <param name="factory">The factory.</param>
        void CreateWith(IElementFactory factory);
    }

    /// <summary>
    /// Expression indicating the element generator to create an element using the specified factory.
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    public interface IElementFactoryExpression<TElement> : IElementFactoryExpression where TElement : Element
    { }
}