using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using TFun = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Collections.Generic.IDictionary<string, object>>;


namespace HelperExpressions
{
    public class ClassInfos<T>
    {
        private static readonly Type _type = typeof(T);
        public Func<T, Tout> GetPropertyFunc<Tout> (string propertyName) 
        {
            ParameterExpression arg = Expression.Parameter(_type);
            Expression expr = Expression.Property(arg, propertyName);
            var propResolver = Expression.Lambda(expr, arg).Compile();
            return propResolver;
        }

        public MethodInfo GetPropertyGetMethodInfo(string propertyName)
        {
            return _type.GetRuntimeProperty(propertyName).GetMethod;
        }

        public Func<T, int> GetPropertyDelegate(string propertyName)
        {

            var pInfo = _type.GetRuntimeProperty(propertyName);
            var delegateType = typeof(Func<,>).MakeGenericType(_type, pInfo.PropertyType);

            return (Func<T, int>)pInfo.GetMethod.CreateDelegate(delegateType);
        }
    }
}
