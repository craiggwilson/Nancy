namespace Nancy.Elements
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// An element representing a html tag.
    /// </summary>
    public class TagElement : Element
    {
        private readonly Dictionary<string, string> attributes;

        /// <summary>
        /// Gets or sets the child. Using this member will require manual adjustment of the graph to keep every node in sync.
        /// </summary>
        /// <value>
        /// The child.
        /// </value>
        public Element Child { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this tag is self closing.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is self closing; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelfClosing { get; set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TagElement"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TagElement(string name)
        {
            this.Name = name;
            this.attributes = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetAttribute(string name)
        {
            string value;
            return this.attributes.TryGetValue(name, out value)
                ? value
                : string.Empty;
        }

        /// <summary>
        /// Sets the attribute.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void SetAttribute(string name, string value)
        {
            this.attributes[name] = value;
        }

        /// <summary>
        /// Renders the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public override void Render(TextWriter writer)
        {
            writer.Write("<{0}", Name);
            this.RenderAttributes(writer);
            if (this.IsSelfClosing && this.Child == null)
            {
                writer.Write("/>");
            }
            else
            {
                writer.Write(">");
                this.RenderChildren(writer);
                writer.Write("</{0}>", this.Name);
            }

            base.Render(writer);
        }

        private void RenderAttributes(TextWriter writer)
        {
            foreach (var kvp in this.attributes)
                writer.Write(" {0}=\"{1}\"", kvp.Key, kvp.Value);
        }

        private void RenderChildren(TextWriter writer)
        {
            if (this.Child != null)
                this.Child.Render(writer);
        }
    }
}