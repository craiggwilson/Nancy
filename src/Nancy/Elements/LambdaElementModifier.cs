namespace Nancy.Elements
{
    using System;

    /// <summary>
    /// A conditionally executed modifier.
    /// </summary>
    public class LambdaElementModifier : IElementModifier
    {
        private readonly Action<ElementContext, Element> modifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaElementModifier"/> class.
        /// </summary>
        /// <param name="canModify">The can modify.</param>
        public LambdaElementModifier(Action<ElementContext, Element> modifier)
        {
            this.modifier = modifier;
        }

        /// <summary>
        /// Modifies the element.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="element">The element.</param>
        public void Modify(ElementContext context, Element element)
        {
            this.modifier(context, element);
        }
    }
}