namespace Nancy.ViewEngines.Razor
{
    using System;
    using System.IO;
    using System.Web;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using Nancy.Reflection;
    using Nancy.Elements;

    /// <summary>
    /// Helpers to generate html content.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class HtmlHelpers<TModel>
    {
        private readonly TModel model;

        private readonly RazorViewEngine engine;
        private readonly IRenderContext renderContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlHelpers"/> class.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="renderContext"></param>
        public HtmlHelpers(RazorViewEngine engine, IRenderContext renderContext, TModel model)
        {
            this.engine = engine;
            this.renderContext = renderContext;
            this.model = model;
        }

        public IHtmlString DisplayFor(Expression<Func<TModel, object>> memberExpression)
        {
            return GenerateElement("Display", model, memberExpression);
        }

        public IHtmlString EditorFor(Expression<Func<TModel, object>> memberExpression)
        {
            return GenerateElement("Editor", model, memberExpression);
        }

        public IHtmlString LabelFor(Expression<Func<TModel, object>> memberExpression)
        {
            return GenerateElement("Label", model, memberExpression);
        }

        /// <summary>
        /// Renders a partial with the given view name.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        public IHtmlString Partial(string viewName)
        {
            return this.Partial(viewName, null);
        }

        /// <summary>
        /// Renders a partial with the given view name.
        /// </summary>
        /// <param name="viewName">Name of the view.</param>
        /// <param name="model">The model.</param>
        public IHtmlString Partial(string viewName, dynamic model)
        {
            var view = this.renderContext.LocateView(viewName, model);

            var response = this.engine.RenderView(view, model, this.renderContext);
            Action<Stream> action = response.Contents;
            var mem = new MemoryStream();

            action.Invoke(mem);
            mem.Position = 0;

            var reader = new StreamReader(mem);

            return new NonEncodedHtmlString(reader.ReadToEnd());
        }

        /// <summary>
        /// Returns an html string composed of raw, non-encoded text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public IHtmlString Raw(string text)
        {
            return new NonEncodedHtmlString(text);
        }

        /// <summary>
        /// Creates an anti-forgery token.
        /// </summary>
        /// <returns></returns>
        public IHtmlString AntiForgeryToken()
        {
            var tokenKeyValue = this.renderContext.GetCsrfToken();

            return new NonEncodedHtmlString(String.Format("<input type=\"hidden\" name=\"{0}\" value=\"{1}\"/>", tokenKeyValue.Key, tokenKeyValue.Value));
        }

        private IHtmlString GenerateElement(string generatorKey, TModel instance, Expression<Func<TModel, object>> memberExpression)
        {
            var elementContext = CreateElementContext(generatorKey, instance, memberExpression);

            var element = renderContext.ElementGenerator.Generate(elementContext);

            return new HtmlString(RenderElement(element));
        }

        private ElementContext CreateElementContext(string generatorKey, TModel instance, Expression<Func<TModel, object>> memberExpression)
        {
            var accessor = memberExpression.ToAccessor();

            return new ElementContext(generatorKey, renderContext.ElementGenerator.GenerateName(accessor), instance, accessor);
        }

        private static MemberInfo GetMemberInfo(Expression<Func<TModel, object>> expression)
        {
            var member = expression.Body as MemberExpression;
            if (member != null)
                return member.Member;

            throw new ArgumentException("Expression must be a MemberExpression", "expression");
        }

        private static string RenderElement(Element element)
        {
            var root = element.Root();

            var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
            {
                root.Render(writer);
                return sb.ToString();
            }
        }
    }
}