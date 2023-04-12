using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Quicker.Extensions;

namespace Quicker.Helpers
{
    public static class QuickerConvert
    {

        public static int ToInt(object input)
        {
            return ToIntOrNull(input) ?? 0;
        }


        public static int? ToIntOrNull(object input)
        {
            var success = int.TryParse(input.SafeString(), out var result);
            if (success)
                return result;
            try
            {
                var temp = ToDoubleOrNull(input, 0);
                if (temp == null)
                    return null;
                return Convert.ToInt32(temp);
            }
            catch
            {
                return null;
            }
        }


        public static long ToLong(object input)
        {
            return ToLongOrNull(input) ?? 0;
        }


        public static long? ToLongOrNull(object input)
        {
            var success = long.TryParse(input.SafeString(), out var result);
            if (success)
                return result;
            try
            {
                var temp = ToDecimalOrNull(input, 0);
                if (temp == null)
                    return null;
                return Convert.ToInt64(temp);
            }
            catch
            {
                return null;
            }
        }

        public static float ToFloat(object input, int? digits = null)
        {
            return ToFloatOrNull(input, digits) ?? 0;
        }


        public static float? ToFloatOrNull(object input, int? digits = null)
        {
            var success = float.TryParse(input.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return (float)Math.Round(result, digits.Value);
        }


        public static double ToDouble(object input, int? digits = null)
        {
            return ToDoubleOrNull(input, digits) ?? 0;
        }


        public static double? ToDoubleOrNull(object input, int? digits = null)
        {
            var success = double.TryParse(input.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return Math.Round(result, digits.Value);
        }


        public static decimal ToDecimal(object input, int? digits = null)
        {
            return ToDecimalOrNull(input, digits) ?? 0;
        }


        public static decimal? ToDecimalOrNull(object input, int? digits = null)
        {
            var success = decimal.TryParse(input.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return Math.Round(result, digits.Value);
        }


        public static bool ToBool(object input)
        {
            if (input == null) return false;
            return ToBoolOrNull(input) ?? false;
        }


        public static bool? ToBoolOrNull(object input)
        {
            bool? value = GetBool(input);
            if (value != null)
                return value.Value;
            return bool.TryParse(input.SafeString(), out var result) ? (bool?)result : null;
        }


        private static bool? GetBool(object input)
        {
            switch (input.SafeString().ToLower())
            {
                case "0":
                    return false;
                case "f":
                    return false;
                case "false":
                    return false;
                case "no":
                    return false;
                case "fail":
                    return false;
                case "1":
                    return true;
                case "":
                    return true;
                case "ok":
                    return true;
                case "yes":
                    return true;
                default:
                    return null;
            }
        }


        public static DateTime ToDate(object input)
        {
            return ToDateOrNull(input) ?? DateTime.MinValue;
        }


        public static DateTime? ToDateOrNull(object input)
        {
            return DateTime.TryParse(input.SafeString(), out var result) ? (DateTime?)result : null;
        }


        public static Guid ToGuid(object input)
        {
            return ToGuidOrNull(input) ?? Guid.Empty;
        }


        public static Guid? ToGuidOrNull(object input)
        {
            return Guid.TryParse(input.SafeString(), out var result) ? (Guid?)result : null;
        }

        public static List<Guid> ToGuidList(string input)
        {
            return ToList<Guid>(input);
        }
        public static List<int> ToIntList(string input)
        {
            return ToList<int>(input);
        }
        public static List<T> ToList<T>(string input)
        {
            var result = new List<T>();
            if (string.IsNullOrWhiteSpace(input))
                return result;
            var array = input.Split(',');
            result.AddRange(from each in array where !string.IsNullOrWhiteSpace(each) select To<T>(each));
            return result;
        }

        public static object ConvertTo<T>(object input, T defaultValue) where T : Type
        {

            try
            {
                return input.ConvertTo(defaultValue);

            }
            catch
            {
                return default(T);
            }
        }
        public static T ConvertTo<T>(object input)
        {
            if (input == null)
                return default;
            if (input is string && string.IsNullOrWhiteSpace(input.ToString()))
                return default;
            Type type = QuickerCommon.GetType<T>();
            var typeName = type.Name.ToLower();
            try
            {
                if (typeName == "string")
                    return (T)(object)input.ToString();
                if (typeName == "guid")
                    return (T)(object)new Guid(input.ToString());
                if (type.IsEnum)
                    return QuickerEnum.Parse<T>(input);
                if (input is IConvertible)
                    return (T)Convert.ChangeType(input, type);
                return (T)input;
            }
            catch
            {
                return default;
            }
        }
        public static T To<T>(object obj)
        {

            if (typeof(T) == typeof(Guid) || typeof(T) == typeof(TimeSpan))
            {
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


        public static byte[] ToBytes(string input)
        {
            return ToBytes(input, Encoding.UTF8);
        }


        public static byte[] ToBytes(string input, Encoding encoding)
        {
            return string.IsNullOrWhiteSpace(input) ? new byte[] { } : encoding.GetBytes(input);
        }
    }
}
