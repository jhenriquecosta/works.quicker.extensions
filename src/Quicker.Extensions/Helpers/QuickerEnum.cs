using Quicker.Application.Services.Dto;
using Quicker.Data.Common;
using Quicker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Quicker.Helpers
{
    public static class QuickerEnum
    {

        public static bool IsValid(object value)
        {
            var enumType = value.GetType();

            if (enumType == null)
            {
                throw new InvalidOperationException("Type cannot be null");
            }
            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException("Type must be an enum");
            }
            if (!Enum.IsDefined(enumType, value))
            {
                return false;
            }
            return true;
        }
        public static TEnum Parse<TEnum>(object member)
        {
            string value = member.SafeString();
            if (string.IsNullOrWhiteSpace(value))
            {
                if (typeof(TEnum).IsGenericType)
                    return default;
                throw new ArgumentNullException(nameof(member));
            }
            return (TEnum)Enum.Parse(QuickerCommon.GetType<TEnum>(), value, true);
        }
        public static List<ComboboxItemDto> ToComboBox<TEnum>(Type typeEnum)
        {
            var lstData = Enum.GetNames(typeEnum)
                  .ToDictionary(key => GetValue(typeEnum, key), value => GetDescription(typeEnum, value))
                  .ToList()
                  .Select(x => new ComboboxItemDto(x.Key.ToString(), x.Value)).ToList();
            return lstData;
        }
        public static string GetName<TEnum>(object member)
        {
            return GetName(QuickerCommon.GetType<TEnum>(), member);
        }

        public static string GetName(Type type, object member)
        {
            if (type == null)
                return string.Empty;
            if (member == null)
                return string.Empty;
            if (member is string)
                return member.ToString();
            if (type.GetTypeInfo().IsEnum == false)
                return string.Empty;
            return Enum.GetName(type, member);
        }

        public static int GetValue<TEnum>(object member)
        {
            return GetValue(QuickerCommon.GetType<TEnum>(), member);
        }

        public static int GetValue(Type type, object member)
        {
            string value = member.SafeString();
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(member));
            return (int)Enum.Parse(type, member.ToString(), true);
        }
        public static string GetDescription<TEnum>(object member)
        {
            return QuickerReflection.GetDescription<TEnum>(GetName<TEnum>(member));
        }

        public static string GetDescription(Type type, object member)
        {
            return QuickerReflection.GetDescription(type, GetName(type, member));
        }
        public static TEnum GetValueFromStr<TEnum>(string value)
        {
            if (value.IsEmpty()) return default;


            var typeEnum = typeof(TEnum);
            var getEnum = Enum.GetNames(typeEnum).Select
                (item => new
                {
                    Key = GetValue(typeEnum, item),
                    Description = GetDescription(typeEnum, item),
                    Value = GetValue(typeEnum, item).ChangeType<TEnum>()
                }).SingleOrDefault(f => f.Description.Equals(value)).Value;
            return getEnum;
        }
        
        public static List<Item> GetItems<TEnum>()
        {
            return GetItems(typeof(TEnum));
        }
        public static List<Item> GetItems(Type typeEnum)
        {
            var lstData = Enum.GetNames(typeEnum).ToDictionary(key => GetValue(typeEnum, key), value => GetDescription(typeEnum, value)).ToList();
            var dataItemList = lstData.Select(x =>
                new Item(x.Value, x.Key.ChangeType(typeEnum),x.Key)
                

            ).ToList();
            return dataItemList;
        }
        

        public static List<string> GetNames<TEnum>()
        {
            return GetNames(typeof(TEnum));
        }
        
        public static List<string> GetNames(Type type)
        {
            type = QuickerCommon.GetType(type);
            if (type.IsEnum == false)
                throw new InvalidOperationException($" {type} ");
            var result = new List<string>();
            foreach (var field in type.GetFields())
            {
                if (!field.FieldType.IsEnum)
                    continue;
                result.Add(field.Name);
            }
            return result;
        }
    }
}
