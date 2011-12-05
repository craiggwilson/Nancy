namespace Nancy.Elements
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Extensions
    {
        private const string ATTR_CLASS = "class";
        private const string ATTR_DATA_PREFIX = "data-";
        private const string ATTR_ID = "id";

        /// <summary>
        /// Inserts the given element directly after the current element.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static TElement After<TElement>(this TElement @this, Element element) where TElement : Element
        {
            element.Next = @this.Next;
            if (@this.Next != null)
                @this.Next.Previous = element;
            @this.Next = element;
            element.Previous = @this;
            return @this;
        }

        /// <summary>
        /// Adds the class to the current element.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="class">The @class.</param>
        /// <returns></returns>
        public static TElement AddClass<TElement>(this TElement @this, string @class) where TElement : TagElement
        {
            var classString = @this.GetAttribute(ATTR_CLASS);
            var classes = @this.GetAttribute(ATTR_CLASS).Split(' ');
            if (!classes.Contains(@class))
                classString += " " + @class;

            @this.SetAttribute(ATTR_CLASS, classString);
            return @this;
        }

        /// <summary>
        /// Adds the classes to the current element.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="classes">The classes.</param>
        /// <returns></returns>
        public static TElement AddClasses<TElement>(this TElement @this, params string[] classes) where TElement : TagElement
        {
            foreach (var @class in classes)
                @this.AddClass(@class);

            return @this;
        }

        /// <summary>
        /// Adds the classes to the current element.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="classes">The classes.</param>
        /// <returns></returns>
        public static TElement AddClasses<TElement>(this TElement @this, IEnumerable<string> classes) where TElement : TagElement
        {
            foreach (var @class in classes)
                @this.AddClass(@class);

            return @this;
        }

        /// <summary>
        /// Appends the element to the current element's children.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="child">The child.</param>
        /// <returns></returns>
        public static TElement Append<TElement>(this TElement @this, Element child) where TElement : TagElement
        {
            child.Parent = @this;
            if (@this.Child == null)
            {
                @this.Child = child;
                return @this;
            }

            var lastChild = @this.Child;
            while (lastChild.Next != null)
                lastChild = lastChild.Next;

            lastChild.Next = child;
            child.Previous = lastChild;

            ReparentSiblings(@this, child);
            return @this;
        }

        /// <summary>
        /// Gets the value of the attribute on the current element.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string Attribute(this TagElement @this, string name)
        {
            return @this.GetAttribute(name);
        }

        /// <summary>
        /// Sets the value of the attribute on the current element.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static TElement Attribute<TElement>(this TElement @this, string name, string value) where TElement : TagElement
        {
            @this.SetAttribute(name, value);
            return @this;
        }

        /// <summary>
        /// Inserts the given element directly before the current element.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static TElement Before<TElement>(this TElement @this, Element element) where TElement : Element
        {
            element.Previous = @this.Previous;
            if (@this.Previous != null)
                @this.Previous.Next = element;
            @this.Previous = element;
            element.Next = @this;
            return @this;
        }

        /// <summary>
        /// Gets the classes for the current element.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static IEnumerable<string> Classes(this TagElement @this)
        {
            return @this.GetAttribute(ATTR_CLASS).Split(' ');
        }

        /// <summary>
        /// Gets the data-{name} value for the current element.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static string Data(this TagElement @this, string name)
        {
            return @this.GetAttribute(ATTR_DATA_PREFIX + name);
        }

        /// <summary>
        /// Sets the data-{name} value for the current element.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static TElement Data<TElement>(this TElement @this, string name, string value) where TElement : TagElement
        {
            @this.SetAttribute(ATTR_DATA_PREFIX + name, value);
            return @this;
        }

        /// <summary>
        /// Gets the first child.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static Element FirstChild(this TagElement @this)
        {
            return @this.Child;
        }

        /// <summary>
        /// Gets the first sibling.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static Element FirstSibling(this Element @this)
        {
            var current = @this;
            while (current != null)
                current = current.Previous;

            return current;
        }

        /// <summary>
        /// Indicates whether the element has an attribute with the given name.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if the element has an attribute with the given name; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasAttribute(this TagElement @this, string name)
        {
            return !string.IsNullOrWhiteSpace(@this.GetAttribute(name));
        }

        /// <summary>
        /// Gets the id for the current element.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static string Id(this TagElement @this)
        {
            return @this.GetAttribute(ATTR_ID);
        }

        /// <summary>
        /// Sets the id for the current element.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static TElement Id<TElement>(this TElement @this, string id) where TElement : TagElement
        {
            @this.SetAttribute(ATTR_ID, id);
            return @this;
        }

        /// <summary>
        /// Inserts the element at the beginning of the current element's children.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="child">The child.</param>
        /// <returns></returns>
        public static TElement Insert<TElement>(this TElement @this, Element child) where TElement : TagElement
        {
            child.Parent = @this;
            if (@this.Child == null)
            {
                @this.Child = child;
                return @this;
            }

            child.Next = @this.Child;
            @this.Child.Previous = child;
            @this.Child = child;
            return @this;
        }

        /// <summary>
        /// Gets the last child.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static Element LastChild(this TagElement @this)
        {
            var child = @this.Child;
            if (child == null)
                return null;

            while (child.Next != null)
                child = child.Next;

            return child;
        }

        /// <summary>
        /// Gets the last sibling.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static Element LastSibling(this Element @this)
        {
            var current = @this;
            while (current.Next != null)
                current = current.Next;

            return current;
        }

        /// <summary>
        /// Removes the class from the current element.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <param name="class">The @class.</param>
        /// <returns></returns>
        public static TElement RemoveClass<TElement>(this TElement @this, string @class) where TElement : TagElement
        {
            var classes = @this.Classes();
            classes = classes.Except(new[] { @class });

            return @this.Attribute(ATTR_CLASS, string.Join(" ", classes));
        }

        /// <summary>
        /// Gets the root of the element tree.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static Element Root(this Element @this)
        {
            var root = @this;
            while (root.Parent != null)
                root = root.Parent;
            while (root.Previous != null)
                root = root.Previous;

            return root;
        }

        /// <summary>
        /// Whether or not the current element is self-closing.
        /// </summary>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static bool SelfClosing(this TagElement @this)
        {
            return @this.IsSelfClosing;
        }

        /// <summary>
        /// Sets that the tag is self-closing or not.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="this">The @this.</param>
        /// <returns></returns>
        public static TElement SelfClosing<TElement>(this TElement @this, bool value) where TElement : TagElement
        {
            @this.IsSelfClosing = value;
            return @this;
        }

        /// <summary>
        /// Reparents the siblings.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        private static void ReparentSiblings(TagElement parent, Element child)
        {
            while (child.Previous != null)
                child = child.Previous;

            while (child != null)
            {
                child.Parent = parent;
                child = child.Next;
            }
        }
    }
}