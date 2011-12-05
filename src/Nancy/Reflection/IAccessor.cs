namespace Nancy.Reflection
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A standardized way of accessing a value.
    /// </summary>
    public interface IAccessor
    {
        /// <summary>
        /// Gets the types of all the accessors on the path.
        /// </summary>
        IEnumerable<Type> Types { get; }

        /// <summary>
        /// Gets the names of all the members on the path.
        /// </summary>
        IEnumerable<string> Names { get; }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        Type ValueType { get; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        object GetValue(object target);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        void SetValue(object target, object value);
    }
}