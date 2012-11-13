using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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

        public static void CanGetSetDateTime<TSource>(this TSource source, Expression<Func<TSource, DateTime>> expression)
        {
            source.CanGetSetValue(expression, TestValues.DATETIME, typeof(DateTime));
        }

        public static void CanGetSetInt<TSource>(this TSource source, Expression<Func<TSource, int>> expression)
        {
            source.CanGetSetValue(expression, TestValues.INT, typeof(int));
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

        public static void ShouldThrowValidationException<TSource>(this TSource source, string exceptionMessage)
        {
            var exception = new Exception();

            try
            {
                var type = typeof(TSource);
                var meta = type.GetCustomAttributes(false).OfType<MetadataTypeAttribute>().FirstOrDefault();

                if(meta != null)
                {
                    type = meta.MetadataClassType;
                }

                var propertyInfo = type.GetProperties();

                foreach(var info in propertyInfo)
                {
                    var attributes = info.GetCustomAttributes(false).OfType<ValidationAttribute>();

                    foreach(var attribute in attributes)
                    {
                        var objPropInfo = source.GetType().GetProperty(info.Name);
                        attribute.Validate(objPropInfo.GetValue(source, null), info.Name);
                    }
                }
            }
            catch(ValidationException ex)
            {
                exception = ex;
            }
            finally
            {
                Assert.AreEqual(exceptionMessage, exception.Message);
            }
        }
    }
}