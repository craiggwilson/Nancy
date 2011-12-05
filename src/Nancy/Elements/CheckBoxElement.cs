namespace Nancy.Elements
{
    /// <summary>
    /// An html element <input type="checkbox" />
    /// </summary>
    public class CheckBoxElement : TagElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxElement"/> class.
        /// </summary>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public CheckBoxElement(bool value)
            : base("input")
        {
            this.SetAttribute("type", "checkbox");

            if (value)
                this.SetAttribute("checked", "true");
        }
    }
}