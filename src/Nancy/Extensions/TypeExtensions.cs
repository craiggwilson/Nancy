namespace Nancy.Extensions
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class TypeExtensions
    {
        public static string GetAssemblyPath(this Type source)
        {
            var assemblyUri =
                new Uri(source.Assembly.EscapedCodeBase);

            return assemblyUri.LocalPath;
        }

        /// <summary>
        /// Indicates whether the specified type is an anonymous type.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        ///   <c>true</c> if if the specified type is an anonymous type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAnonymousType(this Type source)
        {
            if (source == null)
            {
                return false;
            }

            return source.IsGenericType
                   && (source.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic
                   && (source.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) || source.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
                   && (source.Name.Contains("AnonymousType") || source.Name.Contains("AnonType"))
                   && Attribute.IsDefined(source, typeof(CompilerGeneratedAttribute), false);
        }
    }
}