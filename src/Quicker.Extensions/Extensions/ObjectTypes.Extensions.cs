using System;
using System.Collections.Generic;
using System.Text;

namespace Quicker.Extensions 
{
    public static class ObjectTypeExtensions
    {
        public static bool IsBoolean(this Type type)
        {
            if (type == typeof(bool))
                return true;
            if (type == typeof(bool?))
                return true;
            return false;
        }
        public static bool IsDate(this Type type)
        {
            if (type == typeof(DateTime))
                return true;
            if (type == typeof(DateTime?))
                return true;
            return false;
        }
        public static bool IsNumber(this Type type)
        {
            if (IsNumberDecimal(type) || IsNumberInt(type)) return true;
            return false;
        }
        public static bool IsNumberDecimal(this Type type)
        {
            if (type == typeof(double))
                return true;
            if (type == typeof(double?))
                return true;
            if (type == typeof(decimal))
                return true;
            if (type == typeof(decimal?))
                return true;
            if (type == typeof(float))
                return true;
            if (type == typeof(float?))
                return true;
            return false;
        }
        public static bool IsNumberInt(this Type type)
        {
            if (type == typeof(int))
                return true;
            if (type == typeof(int?))
                return true;
            if (type == typeof(short))
                return true;
            if (type == typeof(short?))
                return true;
            if (type == typeof(long))
                return true;
            if (type == typeof(long?))
                return true;
            return false;
        }
    }
}
