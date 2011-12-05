namespace Nancy.ViewEngines
{
    using Cryptography;
    using Session;
    using Elements;

    /// <summary>
    /// Default render context factory implementation.
    /// </summary>
    public class DefaultRenderContextFactory : IRenderContextFactory
    {
        private readonly IElementGenerator elementGenerator;
        private readonly IViewCache viewCache;
        private readonly IViewResolver viewResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRenderContextFactory"/> class.
        /// </summary>
        /// <param name="viewCache">The view cache that should be used by the created render context.</param>
        /// <param name="viewResolver">The view resolver that should be sused by the created render context.</param>
        /// <param name="elementGenerator">The element generator.</param>
        public DefaultRenderContextFactory(IViewCache viewCache, IViewResolver viewResolver, IElementGenerator elementGenerator)
        {
            this.viewCache = viewCache;
            this.viewResolver = viewResolver;
            this.elementGenerator = elementGenerator;
        }

        /// <summary>
        /// Gets a <see cref="IRenderContext"/> for the specified <see cref="ViewLocationContext"/>.
        /// </summary>
        /// <param name="viewLocationContext">The <see cref="ViewLocationContext"/> for which the context should be created.</param>
        /// <returns>A <see cref="IRenderContext"/> instance.</returns>
        public IRenderContext GetRenderContext(ViewLocationContext viewLocationContext)
        {
            return new DefaultRenderContext(this.viewResolver, this.viewCache, viewLocationContext, this.elementGenerator);
        }
    }
}