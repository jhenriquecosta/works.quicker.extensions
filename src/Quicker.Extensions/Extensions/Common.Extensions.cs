using System;
using System.Collections.Generic;
using System.Linq;
using Quicker.Helpers;

namespace Quicker.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static T SafeValue<T>( this T? value ) where T : struct {
            return value ?? default( T );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="quotes"> "'"</param>
        /// <param name="separator">，</param>
        public static string Join<T>( this IEnumerable<T> list, string quotes = "", string separator = "," ) {
            return QuickerStrings.Join( list, quotes, separator );
        }

             /// <summary>
        /// ,<see cref="ArgumentNullException"/>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameterName"></param>
        public static void CheckNull( this object obj, string parameterName ) {
            if( obj == null )
                throw new ArgumentNullException( parameterName );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static bool IsEmpty( this string value ) {
            return string.IsNullOrWhiteSpace( value );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static bool IsEmpty( this Guid value ) {
            return value == Guid.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static bool IsEmpty( this Guid? value ) {
            if ( value == null )
                return true;
            return value == Guid.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static bool IsEmpty<T>( this IEnumerable<T> value ) {
            if ( value == null )
                return true;
            return !value.Any();
        }
    }
}
