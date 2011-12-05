namespace Nancy.Elements
{
    using System.IO;

    /// <summary>
    /// Base class for rendered elements.
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// Gets or sets the next element.  Using this member will require manual adjustment of the graph to keep every node in sync.
        /// </summary>
        /// <value>
        /// The next element.
        /// </value>
        public Element Next { get; set; }

        /// <summary>
        /// Gets or sets the parent. Using this member will require manual adjustment of the graph to keep every node in sync.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public Element Parent { get; set; }

        /// <summary>
        /// Gets or sets the previous element. Using this member will require manual adjustment of the graph to keep every node in sync.
        /// </summary>
        /// <value>
        /// The previous element.
        /// </value>
        public Element Previous { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        protected Element()
        { }

        /// <summary>
        /// Renders the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public virtual void Render(TextWriter writer)
        {
            if (Next != null)
                Next.Render(writer);
        }
    }
}