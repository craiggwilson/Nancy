namespace Nancy.Reflection
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class Extensions
    {
        /// <summary>
        /// Creates an accessor out of the expression.
        /// </summary>
        /// <typeparam name="TTarget">The type of the target.</typeparam>
        /// <typeparam name="TMember">The type of the member.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static IAccessor ToAccessor<TTarget, TMember>(this Expression<Func<TTarget, TMember>> expression)
        {
            var memberExpression = GetMemberExpression(expression);
            var members = new List<MemberInfo>();
            BuildMemberChain(memberExpression, members);

            var last = members.First().ToAccessor();
            var result = members.Skip(1).Aggregate(last, (a, m) => new AccessorChain(m.ToAccessor(), a));

            return result;
        }

        /// <summary>
        /// Creates an accessor from the PropertyInfo.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <returns></returns>
        public static IAccessor ToAccessor(this PropertyInfo propertyInfo)
        {
            return new PropertyAccessor(propertyInfo);
        }

        /// <summary>
        /// Creates an accessor from the FieldInfo.
        /// </summary>
        /// <param name="fieldInfo">The field info.</param>
        /// <returns></returns>
        public static IAccessor ToAccessor(this FieldInfo fieldInfo)
        {
            return new FieldAccessor(fieldInfo);
        }

        /// <summary>
        /// Creates an accessor from the MemberInfo.
        /// </summary>
        /// <param name="memberInfo">The member info.</param>
        /// <returns></returns>
        public static IAccessor ToAccessor(this MemberInfo memberInfo)
        {
            PropertyInfo propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
                return propertyInfo.ToAccessor();

            FieldInfo fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
                return fieldInfo.ToAccessor();

            throw new NotSupportedException("Only properties and fields are supported.");
        }

        private static void BuildMemberChain(Expression expression, List<MemberInfo> members)
        {
            MemberExpression memberExpression = expression as MemberExpression;
            if (memberExpression != null)
            {
                members.Add(memberExpression.Member);
                BuildMemberChain(memberExpression.Expression, members);
            }
        }

        private static MemberExpression GetMemberExpression(Expression expression)
        {
            MemberExpression memberExpression = null;
            if (expression.NodeType == ExpressionType.Lambda)
            {
                var lambda = (LambdaExpression)expression;
                if (lambda.Body.NodeType == ExpressionType.Convert)
                {
                    var body = (UnaryExpression)lambda.Body;
                    memberExpression = body.Operand as MemberExpression;
                }
                else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
                {
                    memberExpression = lambda.Body as MemberExpression;
                }
            }
            else if (expression.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = (MemberExpression)expression;
            }

            if (memberExpression == null)
                throw new NotSupportedException("Expression is not a member expression.");

            return memberExpression;
        }

    }
}