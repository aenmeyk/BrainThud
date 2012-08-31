
using System;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;

namespace BrainThudTest.Extensions
{
    public static class BrainThudAssertionExtensions
    {
        public static void CanGetSetValue<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> expression, TProperty value)
        {
            var body = (MemberExpression)expression.Body;
            var propertyInfo = typeof(TSource).GetProperty(body.Member.Name);
            propertyInfo.SetValue(source, value, null);
            var actual = propertyInfo.GetValue(source, null);

            Assert.AreEqual(value.GetType(), propertyInfo.PropertyType);
            Assert.AreEqual(value, actual, "The value set on the property did not equal the value returned from the property getter.");
        }
    }
}