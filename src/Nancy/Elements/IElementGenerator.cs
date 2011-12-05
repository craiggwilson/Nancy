namespace Nancy.Elements
{
    using System;
    using Nancy.Reflection;

    public interface IElementGenerator
    {
        /// <summary>
        /// Generates an element for the given context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        Element Generate(ElementContext context);

        /// <summary>
        /// Generates the name for an accessor.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        string GenerateName(IAccessor accessor);
    }
}