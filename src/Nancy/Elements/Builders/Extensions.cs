namespace Nancy.Elements.Builders
{
    using System;

    public static class Extensions
    {
        /// <summary>
        /// Instructs an element generator to create an element with the factory.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <param name="factory">The factory.</param>
        public static void CreateWith(this IElementFactoryExpression @this, Func<ElementContext, Element> factory)
        {
            @this.CreateWith(new LambdaElementFactory(factory));
        }

        /// <summary>
        /// Instructs an element generator to modify an element with the modifier.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="modifier">The modifier.</param>
        public static void ModifyWith<TElement>(this IElementModifierExpression<TElement> @this, Action<ElementContext, TElement> modifier) where TElement : Element
        {
            @this.ModifyWith(new LambdaElementModifier((c, e) => modifier(c, (TElement)e)));
        }

        /// <summary>
        /// Creates a conditional element generator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static IElementGeneratorExpression IfValueTypeIs<T>(this IElementGeneratorExpression @this)
        {
            return new ConditionalElementGeneratorExpression<Element>(c => typeof(T).IsAssignableFrom(c.Accessor.ValueType), @this.CreateWith, @this.ModifyWith);
        }

        /// <summary>
        /// Creates a conditional element modifier.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static IElementModifierExpression<TagElement> IfTagElement(this IElementModifierExpression @this)
        {
            return new ConditionalElementModifierExpression<TagElement>((c, e) => e is TagElement, @this.ModifyWith);
        }

        /// <summary>
        /// Creates a conditional element modifier.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns></returns>
        public static IElementModifierExpression<TElement> IfTagNameIs<TElement>(this IElementModifierExpression<TElement> @this, string tagName) where TElement : TagElement
        {
            return new ConditionalElementModifierExpression<TElement>((c, e) => ((TagElement)e).Name == tagName, @this.ModifyWith);
        }

        /// <summary>
        /// Creates a conditional element modifier.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public static IElementModifierExpression<TElement> If<TElement>(this IElementModifierExpression<TElement> @this, Func<ElementContext, TElement, bool> condition) where TElement : Element
        {
            return new ConditionalElementModifierExpression<TElement>((c, e) => condition(c, (TElement)e), @this.ModifyWith);
        }
    }
}