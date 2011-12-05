namespace Nancy.Elements
{
    using System;
    using Nancy.Reflection;

    /// <summary>
    /// Generates a name based on a lambda function.
    /// </summary>
    public class LambdaElementNameGenerator : IElementNameGenerator
    {
        private readonly Func<IAccessor, string> generator;

        /// <summary>
        /// Initializes a new instance of the <see cref="LambdaElementNameGenerator"/> class.
        /// </summary>
        /// <param name="generator">The generator.</param>
        public LambdaElementNameGenerator(Func<IAccessor, string> generator)
        {
            this.generator = generator;
        }

        /// <summary>
        /// Generates the name.
        /// </summary>
        /// <param name="accessor">The accessor.</param>
        /// <returns></returns>
        public string GenerateName(IAccessor accessor)
        {
            return this.generator(accessor);
        }
    }
}