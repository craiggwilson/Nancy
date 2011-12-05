namespace Nancy.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// An accessor for a PropertyInfo.
    /// </summary>
    public class PropertyAccessor : IAccessor
    {
        private readonly PropertyInfo propertyInfo;

        /// <summary>
        /// Gets the types of all the accessors on the path.
        /// </summary>
        public IEnumerable<Type> Types
        {
            get { return new[] { this.propertyInfo.PropertyType }; }
        }

        /// <summary>
        /// Gets the names of all the members on the path.
        /// </summary>
        public IEnumerable<string> Names
        {
            get { return new[] { this.propertyInfo.Name }; }
        }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public Type ValueType
        {
            get { return this.propertyInfo.PropertyType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyAccessor"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property.</param>
        public PropertyAccessor(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            return this.propertyInfo.GetValue(target, null);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public void SetValue(object target, object value)
        {
            this.propertyInfo.SetValue(target, value, null);
        }
    }
}
