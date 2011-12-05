namespace Nancy.Elements
{
    using System;

    /// <summary>
    /// Modifies an element.
    /// </summary>
    public interface IElementModifier
    {
        /// <summary>
        /// Modifies the element.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="element">The element.</param>
        void Modify(ElementContext context, Element element);
    }
}