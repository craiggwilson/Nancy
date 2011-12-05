namespace Nancy.Elements.Builders
{
    /// <summary>
    /// Expression indicating the element generator to modify an element with the modifier.
    /// </summary>
    public interface IElementModifierExpression
    {
        /// <summary>
        /// Instructs an element generator to modify an element with the modifier.
        /// </summary>
        /// <param name="modifier">The modifier.</param>
        void ModifyWith(IElementModifier modifier);
    }

    /// <summary>
    /// Expression indicating the element generator to modify an element with the modifier.
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    public interface IElementModifierExpression<TElement> : IElementModifierExpression where TElement : Element
    { }
}