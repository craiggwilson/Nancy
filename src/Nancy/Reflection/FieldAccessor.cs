namespace Nancy.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// An accessor for a FieldInfo.
    /// </summary>
    public class FieldAccessor : IAccessor
    {
        private readonly FieldInfo fieldInfo;

        /// <summary>
        /// Gets the types of all the accessors on the path.
        /// </summary>
        public IEnumerable<Type> Types
        {
            get { return new[] { this.fieldInfo.FieldType }; }
        }

        /// <summary>
        /// Gets the names of all the members on the path.
        /// </summary>
        public IEnumerable<string> Names
        {
            get { return new[] { this.fieldInfo.Name }; }
        }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public Type ValueType
        {
            get { return this.fieldInfo.FieldType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldAccessor"/> class.
        /// </summary>
        /// <param name="fieldInfo">The field info.</param>
        public FieldAccessor(FieldInfo fieldInfo)
        {
            this.fieldInfo = fieldInfo;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            return this.fieldInfo.GetValue(target);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public void SetValue(object target, object value)
        {
            this.fieldInfo.SetValue(target, value);
        }
    }
}