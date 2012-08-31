
using System;
using System.Linq.Expressions;
using System.Reflection;
using BrainThudTest.Tools;
using NUnit.Framework;

namespace BrainThudTest.Extensions
{
    public static class BrainThudAssertionExtensions
    {
        public static void CanGetSetGuid<TSource>(this TSource source, Expression<Func<TSource, Guid>> expression)
        {
            source.CanGetSetValue(expression, TestValues.GUID, typeof(Guid));
        }

        public static void CanGetSetString<TSource>(this TSource source, Expression<Func<TSource, string>> expression)
        {
            source.CanGetSetValue(expression, TestValues.STRING, typeof(string));
        }


        public static void CanGetSetValue<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> expression, TProperty value, Type expectedType)
        {
            var body = (MemberExpression)expression.Body;
            var propertyInfo = typeof(TSource).GetProperty(body.Member.Name);
            propertyInfo.SetValue(source, value, null);
            var actual = propertyInfo.GetValue(source, null);

            Assert.AreEqual(expectedType, propertyInfo.PropertyType);
            Assert.AreEqual(value, actual, "The value set on the property did not equal the value returned from the property getter.");
        }
    }
}