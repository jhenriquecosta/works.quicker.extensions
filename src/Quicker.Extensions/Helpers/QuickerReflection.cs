using Quicker.Data.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Quicker.Helpers
{
    public static class QuickerReflection
    {
        public static string GetDescription<T>()
        {
            return GetDescription(QuickerCommon.GetType<T>());
        }


        public static string GetDescription<T>(string memberName)
        {
            return GetDescription(QuickerCommon.GetType<T>(), memberName);
        }


        public static string GetDescription(Type type, string memberName)
        {
            if (type == null)
                return string.Empty;
            if (string.IsNullOrWhiteSpace(memberName))
                return string.Empty;
            return GetDescription(type.GetTypeInfo().GetMember(memberName).FirstOrDefault());
        }


        public static string GetDescription(MemberInfo member)
        {
            if (member == null)
                return string.Empty;
            return member.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute ? attribute.Description : member.Name;
        }


        public static string GetDisplayName<T>()
        {
            return GetDisplayName(QuickerCommon.GetType<T>());
        }


        public static string GetDisplayName(MemberInfo member)
        {
            if (member == null)
                return string.Empty;
            if (member.GetCustomAttribute<DisplayAttribute>() is DisplayAttribute displayAttribute)
                return displayAttribute.Name;
            if (member.GetCustomAttribute<DisplayNameAttribute>() is DisplayNameAttribute displayNameAttribute)
                return displayNameAttribute.DisplayName;
            return string.Empty;
        }

        public static string GetDisplayNameOrDescription<T>()
        {
            return GetDisplayNameOrDescription(QuickerCommon.GetType<T>());
        }


        public static string GetDisplayNameOrDescription(MemberInfo member)
        {
            var result = GetDisplayName(member);
            return string.IsNullOrWhiteSpace(result) ? GetDescription(member) : result;
        }


        public static List<Type> FindTypes<TFind>(params Assembly[] assemblies)
        {
            var findType = typeof(TFind);
            return FindTypes(findType, assemblies);
        }


        public static List<Type> FindTypes(Type findType, params Assembly[] assemblies)
        {
            var result = new List<Type>();
            foreach (var assembly in assemblies)
                result.AddRange(GetTypes(findType, assembly));
            return result.Distinct().ToList();
        }


        private static List<Type> GetTypes(Type findType, Assembly assembly)
        {
            var result = new List<Type>();
            if (assembly == null)
                return result;
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException)
            {
                return result;
            }
            foreach (var type in types)
                AddType(result, findType, type);
            return result;
        }

        private static void AddType(List<Type> result, Type findType, Type type)
        {
            if (type.IsInterface || type.IsAbstract)
                return;
            if (findType.IsAssignableFrom(type) == false && MatchGeneric(findType, type) == false)
                return;
            result.Add(type);
        }


        private static bool MatchGeneric(Type findType, Type type)
        {
            if (findType.IsGenericTypeDefinition == false)
                return false;
            var definition = findType.GetGenericTypeDefinition();
            foreach (var implementedInterface in type.FindInterfaces((filter, criteria) => true, null))
            {
                if (implementedInterface.IsGenericType == false)
                    continue;
                return definition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
            }
            return false;
        }


        public static List<TInterface> GetInstancesByInterface<TInterface>(params Assembly[] assemblies)
        {
            return FindTypes<TInterface>(assemblies)
                .Select(t => CreateInstance<TInterface>(t)).ToList();
        }


        public static T CreateInstance<T>(Type type, params object[] parameters)
        {
            return  QuickerConvert.To<T>(Activator.CreateInstance(type, parameters));
        }


        public static Assembly GetAssembly(string assemblyName)
        {
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        public static bool IsBool(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Boolean";
                case MemberTypes.Property:
                    return IsBool((PropertyInfo)member);
            }
            return false;
        }

        private static bool IsBool(PropertyInfo property)
        {
            return property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
        }

        public static bool IsEnum(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return ((TypeInfo)member).IsEnum;
                case MemberTypes.Property:
                    return IsEnum((PropertyInfo)member);
            }
            return false;
        }


        private static bool IsEnum(PropertyInfo property)
        {
            if (property.PropertyType.GetTypeInfo().IsEnum)
                return true;
            var value = Nullable.GetUnderlyingType(property.PropertyType);
            if (value == null)
                return false;
            return value.GetTypeInfo().IsEnum;
        }


        public static bool IsDate(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.DateTime";
                case MemberTypes.Property:
                    return IsDate((PropertyInfo)member);
            }
            return false;
        }


        private static bool IsDate(PropertyInfo property)
        {
            if (property.PropertyType == typeof(DateTime))
                return true;
            if (property.PropertyType == typeof(DateTime?))
                return true;
            return false;
        }


        public static bool IsInt(MemberInfo member)
        {
            if (member == null)
                return false;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Int32" || member.ToString() == "System.Int16" || member.ToString() == "System.Int64";
                case MemberTypes.Property:
                    return IsInt((PropertyInfo)member);
            }
            return false;
        }


        private static bool IsInt(PropertyInfo property)
        {
            if (property.PropertyType == typeof(int))
                return true;
            if (property.PropertyType == typeof(int?))
                return true;
            if (property.PropertyType == typeof(short))
                return true;
            if (property.PropertyType == typeof(short?))
                return true;
            if (property.PropertyType == typeof(long))
                return true;
            if (property.PropertyType == typeof(long?))
                return true;
            return false;
        }


        public static bool IsNumber(MemberInfo member)
        {
            if (member == null)
                return false;
            if (IsInt(member))
                return true;
            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Double" || member.ToString() == "System.Decimal" || member.ToString() == "System.Single";
                case MemberTypes.Property:
                    return IsNumber((PropertyInfo)member);
            }
            return false;
        }


        private static bool IsNumber(PropertyInfo property)
        {
            if (property.PropertyType == typeof(double))
                return true;
            if (property.PropertyType == typeof(double?))
                return true;
            if (property.PropertyType == typeof(decimal))
                return true;
            if (property.PropertyType == typeof(decimal?))
                return true;
            if (property.PropertyType == typeof(float))
                return true;
            if (property.PropertyType == typeof(float?))
                return true;
            return false;
        }

                /// <summary>
        /// 是否集合
        /// </summary>
        /// <param name="type"></param>
        public static bool IsCollection( Type type ) {
            if ( type.IsArray )
                return true;
            return IsGenericCollection( type );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public static bool IsGenericCollection( Type type ) {
            if ( !type.IsGenericType )
                return false;
            var typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof( IEnumerable<> )
                   || typeDefinition == typeof( IReadOnlyCollection<> )
                   || typeDefinition == typeof( IReadOnlyList<> )
                   || typeDefinition == typeof( ICollection<> )
                   || typeDefinition == typeof( IList<> )
                   || typeDefinition == typeof( List<> );
        }

        public static List<Assembly> GetAssemblies(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList()
                .Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"))
                .Select(path => Assembly.Load(new AssemblyName(path))).ToList();
        }

        
        public static List<Item> GetPublicProperties(object instance)
        {
            var properties = instance.GetType().GetProperties();
            return properties.ToList().Select(t => new Item(t.Name, t.GetValue(instance))).ToList();
        }

        public static Type GetTopBaseType<T>()
        {
            return GetTopBaseType(typeof(T));
        }

        public static Type GetTopBaseType(Type type)
        {
            if (type == null)
                return null;
            if (type.IsInterface)
                return type;
            if (type.BaseType == typeof(object))
                return type;
            return GetTopBaseType(type.BaseType);
        }

        public static Type GetElementType(Type type)
        {
            if (IsCollection(type) == false)
                return type;
            if (type.IsArray)
                return type.GetElementType();
            var genericArgumentsTypes = type.GetTypeInfo().GetGenericArguments();
            if (genericArgumentsTypes == null || genericArgumentsTypes.Length == 0)
                throw new ArgumentException("Get Element Error");
            return genericArgumentsTypes[0];
        }
        
        public static object CreateInstance(string strFullyQualifiedName)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null) return Activator.CreateInstance(type);
            //var assemblies = Bootstrapper.GetAssemblies(); //busca os assemblies
            //if (assemblies == null) assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //foreach (var asm in assemblies)
            //{
            //    //  type = asm.GetType(strFullyQualifiedName);
            //    type = GetType(asm, strFullyQualifiedName);
            //    if (type != null) return Activator.CreateInstance(type);
            //}
            return null;
        }
        public static TItem CreateInstance<TItem>()
        {
            var newItemCreator = new Lazy<Func<TItem>>(() => CreateNewItem<TItem>());
            return newItemCreator.Value();

        }
        public static Func<TItem> CreateNewItem<TItem>()
        {
            return Expression.Lambda<Func<TItem>>(Expression.New(typeof(TItem))).Compile();
        }

     
        /// <summary>
        /// Bulds an access expression for nested properties while checking for null values.
        /// </summary>
        /// <param name="item">Item that has the requested field name.</param>
        /// <param name="fieldName">Item field name.</param>
        /// <returns>Returns the requested field if it exists.</returns>
        private static Expression GetSafeField(Expression item, string fieldName)
        {
            var parts = fieldName.Split(new char[] { '.' }, 2);

            Expression field = Expression.PropertyOrField(item, parts[0]);

            if (parts.Length > 1)
                field = GetSafeField(field, parts[1]);

            // if the value type cannot be null there's no reason to check it for null
            if (!IsNullable(field.Type))
                return field;

            // check if field is null
            return Expression.Condition(Expression.Equal(item, Expression.Constant(null)),
                Expression.Constant(null, field.Type),
                field);
        }

        /// <summary>
        /// Checks if requested type can bu nullable.
        /// </summary>
        /// <param name="type">Object type.</param>
        /// <returns></returns>
        private static bool IsNullable(Type type)
        {
            if (type.IsClass)
                return true;

            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Bulds an access expression for nested properties or fields.
        /// </summary>
        /// <param name="item">Item that has the requested field name.</param>
        /// <param name="fieldName">Item field name.</param>
        /// <returns>Returns the requested field if it exists.</returns>
        private static Expression GetField(Expression item, string fieldName)
        {
            var parts = fieldName.Split(new char[] { '.' }, 2);

            Expression subProperty = Expression.PropertyOrField(item, parts[0]);

            if (parts.Length > 1)
                subProperty = GetField(subProperty, parts[1]);

            return subProperty;
        }

        public static Func<TItem, object> CreateValueGetter<TItem>(string fieldName)
        {
            var item = Expression.Parameter(typeof(TItem), "item");
            var property = GetSafeField(item, fieldName);
            return Expression.Lambda<Func<TItem, object>>(Expression.Convert(property, typeof(object)), item).Compile();
        }

        public static Func<Type> CreateValueTypeGetter<TItem>(string fieldName)
        {
            var item = Expression.Parameter(typeof(TItem));
            var property = GetField(item, fieldName);
            return Expression.Lambda<Func<Type>>(Expression.Constant(property.Type)).Compile();
        }

        public static Func<object> CreateDefaultValueByType<TItem>(string fieldName)
        {
            var item = Expression.Parameter(typeof(TItem));
            var property = GetField(item, fieldName);
            return Expression.Lambda<Func<object>>(Expression.Convert(Expression.Default(property.Type), typeof(object))).Compile();
        }

        public static Action<TItem, object> CreateValueSetter<TItem>(string fieldName)
        {
            var item = Expression.Parameter(typeof(TItem), "item");
            var value = Expression.Parameter(typeof(object), "value");

            // There's ne safe field setter because that should be a developer responsibility
            // to don't allow for null nested fields. 
            var field = GetField(item, fieldName);
            return Expression.Lambda<Action<TItem, object>>(Expression.Assign(field, Expression.Convert(value, field.Type)), item, value).Compile();
        }
    }
}

