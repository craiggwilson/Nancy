namespace Nancy.Elements
{
    using System;
    using System.Reflection;
    using Nancy.Helpers;
    using Nancy.Reflection;

    /// <summary>
    /// Context for the generation of an html element.
    /// </summary>
    public class ElementContext
    {
        private readonly Lazy<object> value;

        /// <summary>
        /// Gets the accessor.
        /// </summary>
        public IAccessor Accessor { get; private set; }

        /// <summary>
        /// Gets the generator key.
        /// </summary>
        public string GeneratorKey { get; private set; }

        /// <summary>
        /// Gets the element id.
        /// </summary>
        public string ElementName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue
        {
            get { return this.Value != null; }
        }

        /// <summary>
        /// Gets the string value of a value.
        /// </summary>
        public string StringValue
        {
            get
            {
                return this.HasValue
                    ? this.Value.ToString()
                    : string.Empty;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public object Value
        {
            get { return this.value.Value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementContext"/> class.
        /// </summary>
        /// <param name="generatorKey">The generator key.</param>
        /// <param name="elementName">Name of the element.</param>
        /// <param name="model">The model.</param>
        /// <param name="accessor">The accessor.</param>
        public ElementContext(string generatorKey, string elementName, object model, IAccessor accessor)
        {
            this.GeneratorKey = generatorKey;
            this.ElementName = elementName;
            this.value = new Lazy<object>(() => accessor.GetValue(model));
            this.Accessor = accessor;
        }
    }
}