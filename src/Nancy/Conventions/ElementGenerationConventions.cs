namespace Nancy.Conventions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Nancy.Elements;
    using Nancy.Elements.Builders;
    using Nancy.Reflection;

    /// <summary>
    /// Conventions for element generation
    /// </summary>
    public class ElementGenerationConventions
    {
        private readonly Dictionary<string, KeyedElementGeneratorBuilder> builders;

        /// <summary>
        /// Gets the name generator.
        /// </summary>
        public IElementNameGenerator NameGenerator { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementGenerationConventions"/> class.
        /// </summary>
        public ElementGenerationConventions()
        {
            builders = new Dictionary<string, KeyedElementGeneratorBuilder>();
        }

        /// <summary>
        /// Generators the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public KeyedElementGeneratorBuilder Generator(string key)
        {
            KeyedElementGeneratorBuilder builder;
            if (!builders.TryGetValue(key, out builder))
                builder = builders[key] = new KeyedElementGeneratorBuilder(key);

            return builder;
        }

        /// <summary>
        /// Gets the factories.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IElementFactory> GetFactories()
        {
            return builders.Values.SelectMany(b => b.GetFactories());
        }

        /// <summary>
        /// Gets the modifiers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IElementModifier> GetModifiers()
        {
            return builders.Values.SelectMany(b => b.GetModifiers());
        }

        /// <summary>
        /// Sets the name generator.
        /// </summary>
        /// <param name="nameGenerator">The name generator.</param>
        public void SetNameGenerator(IElementNameGenerator nameGenerator)
        {
            NameGenerator = nameGenerator;
        }

        /// <summary>
        /// Sets the name generator.
        /// </summary>
        /// <param name="nameGenerator">The name generator.</param>
        public void SetNameGenerator(Func<IAccessor, string> nameGenerator)
        {
            NameGenerator = new LambdaElementNameGenerator(nameGenerator);
        }
    }
}