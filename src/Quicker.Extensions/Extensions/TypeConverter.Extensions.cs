using Quicker.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Quicker.Extensions
{
    public static partial class TypeExtensions
    {
        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <returns>The converted value.</returns>
        public static object ConvertTo(this object value, Type destinationType)
        {
            return ConvertTo(value, destinationType, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="destinationType">The type to convert the value to.</param>
        /// <param name="culture">Culture</param>
        /// <returns>The converted value.</returns>
        public static object ConvertTo(this object value, Type destinationType, CultureInfo culture)
        {
            if (value != null)
            {
                var sourceType = value.GetType();

                TypeConverter destinationConverter = GetCustomTypeConverter(destinationType);
                TypeConverter sourceConverter = GetCustomTypeConverter(sourceType);
                if (destinationConverter != null && destinationConverter.CanConvertFrom(value.GetType()))
                    return destinationConverter.ConvertFrom(null, culture, value);
                if (sourceConverter != null && sourceConverter.CanConvertTo(destinationType))
                    return sourceConverter.ConvertTo(null, culture, value, destinationType);
                if (destinationType.IsEnum && value is int)
                {
                    var valueConverted = System.Enum.ToObject(destinationType, (int)value);
                    return valueConverted;
                }
                    
                if (!destinationType.IsInstanceOfType(value))
                    return Convert.ChangeType(value, destinationType, culture);
            }
            return value;
        }

        /// <summary>
        /// Converts a value to a destination type.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <typeparam name="T">The type to convert the value to.</typeparam>
        /// <returns>The converted value.</returns>
        public static T ConvertTo<T>(this object value)
        {
            return (T)ConvertTo(value, typeof(T));
        }

        public static TypeConverter GetCustomTypeConverter(this Type type)
        {
            if (type == typeof(List<int>)) return new GenericListTypeConverter<int>();
            if (type == typeof(List<decimal>)) return new GenericListTypeConverter<decimal>();
            if (type == typeof(List<string>)) return new GenericListTypeConverter<string>();
            return TypeDescriptor.GetConverter(type);
        }
    }
}
