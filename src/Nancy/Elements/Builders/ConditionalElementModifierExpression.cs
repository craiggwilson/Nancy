namespace Nancy.Elements.Builders
{
    using System;

    /// <summary>
    /// Creates an element modifier conditionally.
    /// </summary>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    public class ConditionalElementModifierExpression<TElement> : IElementModifierExpression<TElement> where TElement : Element
    {
        private readonly Func<ElementContext, Element, bool> condition;
        private readonly Action<IElementModifier> addModifier;

        private readonly string key;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConditionalElementModifierExpression&lt;TElement&gt;"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="addModifier">The add modifier.</param>
        public ConditionalElementModifierExpression(Func<ElementContext, Element, bool> condition, Action<IElementModifier> addModifier)
        {
            this.condition = condition;
            this.addModifier = addModifier;
        }

        /// <summary>
        /// Instructs an element generator to modify an element with the modifier.
        /// </summary>
        /// <param name="modifier">The modifier.</param>
        public void ModifyWith(IElementModifier modifier)
        {
            this.addModifier(this.WrapModifer(modifier));
        }

        private IElementModifier WrapModifer(IElementModifier modifier)
        {
            return new ConditionalElementModifier(this.condition, modifier);
        }
    }
}