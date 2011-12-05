namespace Nancy.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Part of a chain of accessors.
    /// </summary>
    public class AccessorChain : IAccessor
    {
        private readonly IAccessor current;
        private readonly IAccessor next;

        /// <summary>
        /// Gets the types of all the accessors on the path.
        /// </summary>
        public IEnumerable<Type> Types
        {
            get { return this.current.Types.Concat(this.next.Types); }
        }

        /// <summary>
        /// Gets the names of all the members on the path.
        /// </summary>
        public IEnumerable<string> Names
        {
            get { return this.current.Names.Concat(this.next.Names); }
        }

        /// <summary>
        /// Gets the type of the value.
        /// </summary>
        /// <value>
        /// The type of the value.
        /// </value>
        public Type ValueType
        {
            get { return this.next.ValueType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessorChain"/> class.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="next">The next.</param>
        public AccessorChain(IAccessor current, IAccessor next)
        {
            this.current = current;
            this.next = next;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public object GetValue(object target)
        {
            target = this.current.GetValue(target);
            return this.next.GetValue(target);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        public void SetValue(object target, object value)
        {
            target = this.current.GetValue(target);
            this.next.SetValue(target, value);
        }
    }
}