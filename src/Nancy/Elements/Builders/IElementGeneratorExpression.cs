namespace Nancy.Elements.Builders
{
    /// <summary>
    /// Expression off an element generator.
    /// </summary>
    public interface IElementGeneratorExpression : IElementFactoryExpression, IElementModifierExpression
    { }

    /// <summary>
    /// Expression off an element generator.
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    public interface IElementGeneratorExpression<TElement> : IElementFactoryExpression<TElement>, IElementModifierExpression<TElement>, IElementGeneratorExpression
        where TElement : Element
    { }
}