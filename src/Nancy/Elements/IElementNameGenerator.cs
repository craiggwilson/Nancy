namespace Nancy.Elements
{
    using Nancy.Reflection;

    /// <summary>
    /// Generates a name for an element.
    /// </summary>
    public interface IElementNameGenerator
    {
        /// <summary>
        /// Generates the name.
        /// </summary>
        /// <param name="accessor">The accessor.</param>
        /// <returns></returns>
        string GenerateName(IAccessor accessor);
    }
}