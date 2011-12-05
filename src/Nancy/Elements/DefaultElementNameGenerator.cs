namespace Nancy.Elements
{
    using Nancy.Reflection;

    /// <summary>
    /// Generates a name by concatenating all the names with a ".".
    /// </summary>
    public class DefaultElementNameGenerator : IElementNameGenerator
    {
        /// <summary>
        /// Generates the name.
        /// </summary>
        /// <param name="accessor">The accessor.</param>
        /// <returns></returns>
        public string GenerateName(IAccessor accessor)
        {
            return string.Join(".", accessor.Names);
        }
    }
}