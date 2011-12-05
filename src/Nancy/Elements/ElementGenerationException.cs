namespace Nancy.Elements
{
    using System;

    /// <summary>
    /// Thrown when an error occurs generating an element.
    /// </summary>
    public class ElementGenerationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementGenerationException"/> class.
        /// </summary>
        /// <param name="message">The message that should be displayed with the exception.</param>
        public ElementGenerationException(string message)
            : base(message)
        { }
    }
}