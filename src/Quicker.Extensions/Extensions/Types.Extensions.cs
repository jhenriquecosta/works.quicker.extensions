using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Quicker.Helpers;

namespace Quicker.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypesExtensions
    {
         public static Assembly GetAssembly(this Type type)
        {
           
            return type.GetTypeInfo().Assembly;
        }
        public static Assembly GetAssembly(this object type)
        {
            return type.GetType().GetTypeInfo().Assembly;
        }
    }
}
