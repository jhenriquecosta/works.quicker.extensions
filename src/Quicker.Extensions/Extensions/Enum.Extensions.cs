using Quicker.Helpers;
using System;
using System.ComponentModel;
using System.Globalization;

namespace Quicker.Extensions
{
    public static class EnumExtensions
    {
        
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            string description = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                        {
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        }

                        break;
                    }
                }
            }

            return description;
        }
        //public static string WithIconSize<T>(this T e,IconSize iconSize = IconSize.Px18) where T : IConvertible
        //{
        //    var size = GetDescription(iconSize);
        //    var icon = GetDescription(e);
        //    return $"{icon} {size}";
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public static int Value( this System.Enum instance ) {
            return QuickerEnum.GetValue( instance.GetType(), instance );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="instance"></param>
        public static TResult Value<TResult>( this System.Enum instance ) {
            return QuickerConvert.To<TResult>( Value( instance ) );
        }

        /// <summary>
        /// ,
        /// </summary>
        /// <param name="instance"></param>
        public static string Description( this System.Enum instance ) {
            return QuickerEnum.GetDescription( instance.GetType(), instance );
        }

        
        

        



    }
}
