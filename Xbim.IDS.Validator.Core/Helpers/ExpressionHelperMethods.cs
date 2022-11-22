﻿using System.Linq.Expressions;
using System.Reflection;
using Xbim.Common;

namespace Xbim.IDS.Validator.Core.Helpers
{

    internal class ExpressionHelperMethods
    {
        //private static MethodInfo _enumerableOfTypeMethod = GenericMethodOf(_ => Enumerable.OfType<int>(default(IEnumerable)));
        //private static MethodInfo _queryableOfTypeMethod = GenericMethodOf(_ => Queryable.OfType<int>(default(IQueryable)));
        private static MethodInfo _entityCollectionofTypeMethod = typeof(IReadOnlyEntityCollection).GetMethod(nameof(IReadOnlyEntityCollection.OfType), new Type[] { typeof(string), typeof(bool) });
        private static MethodInfo _enumerableWhereMethod = GenericMethodOf(_ => Enumerable.Where<int>(default(IEnumerable<int>), default(Func<int, bool>)));
        private static MethodInfo _enumerableCastMethod = GenericMethodOf(_ => Enumerable.Cast<int>(default(IEnumerable<int>)));


        //	public static MethodInfo EnumerableOfType
        //	{
        //		get { return _enumerableOfTypeMethod; }
        //	}
        //
        //	public static MethodInfo QueryableOfType
        //	{
        //		get { return _queryableOfTypeMethod; }
        //	}

        public static MethodInfo EnumerableWhereGeneric
        {
            get { return _enumerableWhereMethod; }
        }

        public static MethodInfo EntityCollectionOfType
        {
            get { return _entityCollectionofTypeMethod; }
        }

        public static MethodInfo EnumerableCastGeneric
        {
            get { return _enumerableCastMethod; }
        }

        private static MethodInfo GenericMethodOf<TReturn>(Expression<Func<object, TReturn>> expression)
        {
            return GenericMethodOf(expression as Expression);
        }

        private static MethodInfo GenericMethodOf(Expression expression)
        {
            if (expression is null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            LambdaExpression lambdaExpression = expression as LambdaExpression;

            //Contract.Assert(expression.NodeType == ExpressionType.Lambda);
            //Contract.Assert(lambdaExpression != null);
            //Contract.Assert(lambdaExpression.Body.NodeType == ExpressionType.Call);

            return (lambdaExpression.Body as MethodCallExpression).Method.GetGenericMethodDefinition();
        }

        internal static bool IsIQueryable(Type type)
        {
            return typeof(IQueryable).IsAssignableFrom(type);
        }
    }

}
