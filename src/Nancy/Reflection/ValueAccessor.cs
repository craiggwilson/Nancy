namespace Nancy.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ValueAccessor : IAccessor
    {
        private readonly IEnumerable<string> names;
        private readonly IEnumerable<Type> types;
        private object value;

        /// <summary>
        /// Gets the types of all the accessors on the path.
        /// </summary>
        public IEnumerable<Type> Types
        {
            get { return this.types; }
        }

        /// <summary>
        /// Gets the names of all the members on the path.
        /// </summary>
        public IEnumerable<string> Names
        {
            get { return this.names; }
        }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public Type ValueType
        {
            get { return this.types.Last(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueAccessor"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public ValueAccessor(Type type, string name, object value)
            : this(new [] { type }, new [] { name }, value)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueAccessor"/> class.
        /// </summary>
        /// <param name="types">The types.</param>
        /// <param name="names">The names.</param>
        /// <param name="value">The value.</param>
        public ValueAccessor(IEnumerable<Type> types, IEnumerable<string> names, object value)
        {
            this.types = types;
            this.names = names;
            this.value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            return value;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public void SetValue(object target, object value)
        {
            this.value = value;
        }
    }
}