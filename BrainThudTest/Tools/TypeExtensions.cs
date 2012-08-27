using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace BrainThudTest.Tools
{
    public static class TypeExtensions
    {
        public static void AllPublicMembersShouldBeVirtual(this Type type)
        {
            var publicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            var propertyAccessors = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).SelectMany(x => x.GetAccessors());

            AllMethodsShouldBeVirtual(publicMethods);
            AllMethodsShouldBeVirtual(propertyAccessors);
        }

        public static void AllMethodsShouldBeVirtual(IEnumerable<MethodInfo> methodInfos)
        {
            foreach (var methodInfo in methodInfos)
            {
                if (!methodInfo.IsVirtual)
                {
                    var message = string.Format("Method {0} is not marked as virtual", methodInfo.Name);
                    throw new AssertionException(message);
                }
            }
        }
    }
}