using JetBrains.Annotations;
using Quicker.Extensions;
using Quicker.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Quicker.Extensions
{
    public static partial class ConvertExtensions
    {

        public static string SafeString(this object input)
        {
            return input?.ToString().Trim() ?? string.Empty;
        }


        public static bool ToBool(this string obj)
        {
            return QuickerConvert.ToBool(obj);
        }


        public static bool? ToBoolOrNull(this string obj)
        {
            return QuickerConvert.ToBoolOrNull(obj);
        }


        public static int ToInt(this string obj)
        {
            return QuickerConvert.ToInt(obj);
        }


        public static int? ToIntOrNull(this string obj)
        {
            return QuickerConvert.ToIntOrNull(obj);
        }


        public static long ToLong(this string obj)
        {
            return QuickerConvert.ToLong(obj);
        }


        public static long? ToLongOrNull(this string obj)
        {
            return QuickerConvert.ToLongOrNull(obj);
        }


        public static double ToDouble(this string obj)
        {
            return QuickerConvert.ToDouble(obj);
        }


        public static double? ToDoubleOrNull(this string obj)
        {
            return QuickerConvert.ToDoubleOrNull(obj);
        }


        public static decimal ToDecimal(this string obj)
        {
            return QuickerConvert.ToDecimal(obj);
        }


        public static decimal? ToDecimalOrNull(this string obj)
        {
            return QuickerConvert.ToDecimalOrNull(obj);
        }


        public static DateTime ToDate(this string obj)
        {
            return QuickerConvert.ToDate(obj);
        }


        public static DateTime? ToDateOrNull(this string obj)
        {
            return QuickerConvert.ToDateOrNull(obj);
        }


        public static Guid ToGuid(this string obj)
        {
            return QuickerConvert.ToGuid(obj);
        }


        public static Guid? ToGuidOrNull(this string obj)
        {
            return QuickerConvert.ToGuidOrNull(obj);
        }

        public static List<int> ToIntList(this string obj)
        {
            return QuickerConvert.ToIntList(obj);
        }
        public static List<int> ToIntList(this IList<string> obj)
        {
            if (obj == null)
                return new List<int>();
            return obj.Select(t => t.ToInt()).ToList();
        }

        public static List<Guid> ToGuidList(this string obj)
        {
            return QuickerConvert.ToGuidList(obj);
        }


        public static List<Guid> ToGuidList(this IList<string> obj)
        {
            if (obj == null)
                return new List<Guid>();
            return obj.Select(t => t.ToGuid()).ToList();
        }


        public static T As<T>(this object obj) where T : class
        {
            return (T)obj;
        }

        public static T To<T>(this object obj) where T : struct
        {
            if (typeof(T) == typeof(Guid) || typeof(T) == typeof(TimeSpan))
            {
                // ReSharper disable once PossibleNullReferenceException
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
            }
            if (typeof(T).IsEnum)
            {
                if (Enum.IsDefined(typeof(T), obj))
                {
                    return (T)Enum.Parse(typeof(T), obj.ToString());
                }
                else
                {
                    throw new ArgumentException($"Enum type undefined '{obj}'.");
                }
            }

            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        [NotNull]
        public static T ChangeType<T>([NotNull] this object obj)
        {
            return QuickerConvert.ConvertTo<T>(obj);
        }

        public static object ChangeType<T>([NotNull] this object obj, T defaultValue) where T : Type
        {
            var value = QuickerConvert.ConvertTo<T>(obj, defaultValue);
            return value;
        }

        /// <summary>
        /// Check if an item is in a list.
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="list">List of items</param>
        /// <typeparam name="T">Type of the items</typeparam>
        public static bool IsIn<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }
}
