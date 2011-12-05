namespace Nancy.Elements
{
    using System;

    /// <summary>
    /// Modifies an element based upon a condition.
    /// </summary>
    public class ConditionalElementModifier : IElementModifier
    {
        private readonly Func<ElementContext, Element, bool> canModify;
        private readonly IElementModifier inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalElementModifier"/> class.
        /// </summary>
        /// <param name="canModify">The can modify.</param>
        public ConditionalElementModifier(Func<ElementContext, Element, bool> canModify, IElementModifier inner)
        {
            this.canModify = canModify;
            this.inner = inner;
        }

        /// <summary>
        /// Modifies the element.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="element">The element.</param>
        public void Modify(ElementContext context, Elements.Element element)
        {
            if (this.canModify(context, element))
                this.inner.Modify(context, element);
        }
    }
}