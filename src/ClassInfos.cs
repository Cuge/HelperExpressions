using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace HelperExpressions
{
    public class ClassInfos<T>
    {
        private static readonly Type _type = typeof(T);
        public Func<T, Tout> GetPropertyFunc<Tout> (string propertyName) 
        {
            ParameterExpression arg = Expression.Parameter(_type);
            Expression expr = Expression.Property(arg, propertyName);
            var propResolver = Expression.Lambda<Func<T, Tout>>(expr, arg).Compile();
            return propResolver;
        }

        public MethodInfo GetPropertyGetMethodInfo(string propertyName)
        {
            return _type.GetRuntimeProperty(propertyName).GetMethod;
        }

        public Delegate GetPropertyDelegate(string propertyName)
        {
            ParameterExpression arg = Expression.Parameter(_type);
            Expression expr = Expression.Property(arg, propertyName);
            var propResolver = Expression.Lambda(expr, arg).Compile();
            return propResolver;
        }
    }
}
