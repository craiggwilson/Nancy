using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nancy.Elements
{
    /// <summary>
    /// An element representing <input type="textbox" />
    /// </summary>
    public class TextBoxElement : TagElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxElement"/> class.
        /// </summary>
        public TextBoxElement()
            : base("input")
        {
            this.SetAttribute("type", "text");
        }
    }
}
