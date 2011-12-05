namespace Nancy.Elements
{
    using System;
    using System.IO;

    /// <summary>
    /// An element representing text.
    /// </summary>
    public class TextElement : Element
    {
        private string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextElement"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextElement()
        {
            this.text = string.Empty;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <returns></returns>
        public string Text()
        {
            return this.text;
        }

        /// <summary>
        /// Sets the text on this instance.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public TextElement Text(string text)
        {
            this.text = text ?? string.Empty;
            return this;
        }

        /// <summary>
        /// Renders the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public override void Render(TextWriter writer)
        {
            writer.Write(this.text);
            base.Render(writer);
        }
    }
}