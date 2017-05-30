using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using TFun = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Collections.Generic.IDictionary<string, object>>;


namespace HelperExpressions
{
    public class ClassInfos
    {
        //private static readonly Type _type = typeof(T);
        public Func<object, object> GetPropertyFunc<T>(string propertyName) 
        {
            var type = typeof(T);
            ParameterExpression arg = Expression.Parameter(typeof(object));
            var arg1 = Expression.Convert(arg, type);
            //ParameterExpression arg1 = Expression.Parameter(type);
            Expression expr = Expression.Property(arg1, propertyName);

            var pro = Expression.Lambda(expr, arg);
            Expression converted = Expression.Convert(pro.Body, typeof(object));
            var newpro = Expression.Lambda<Func<object, object>>(converted, arg).Compile();
            return newpro;
        }

         public MethodInfo GetPropertyGetMethodInfo<T>(string propertyName)
        {
            var type = typeof(T);
            return type.GetRuntimeProperty(propertyName).GetMethod;
        }

        public PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            var type = typeof(T);
            return type.GetRuntimeProperty(propertyName);
        }

        public Delegate GetPropertyDelegate<T>(string propertyName)
        {
            var type = typeof(T);
            var pInfo = type.GetRuntimeProperty(propertyName);
            var delegateType = typeof(Func<,>).MakeGenericType(type, pInfo.PropertyType);
            var temp = pInfo.GetMethod.CreateDelegate(delegateType);
            return temp;
        }
    }
}
