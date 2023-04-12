using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Quicker.Helpers
{
    public class QuickerInstance
    {
         
       
        public static ICollection<PropertyInfo> GetProperties(Type obj)
        {
            return  obj.GetProperties();
        }
        public static ICollection<PropertyInfo> GetProperties(string fullName)
        {
            return GetProperties(GetType(fullName));
        }
      

        public static bool Compare(string str1, string str2)
        {
            return string.Compare(str1, str2, true) == 0;
        }
        public static Type GetType(Assembly asm, string fullName)
        {
           return fullName.IndexOf('.') > 0 ? asm.GetTypes().SingleOrDefault(t => t.FullName.Equals(fullName)) :  asm.GetTypes().SingleOrDefault(t => t.FullName.Equals(t.Namespace + "." + fullName));
        }
        public static Type GetType(string fullName)
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                 var type = GetType(asm, fullName);
                 if (type != null) return type;
            }
            return null;
        }
        public static object Create(Assembly asm,string fullName)
        {
            var namespaces = asm.GetTypes().Select(t => t.Namespace);
            foreach (var item in namespaces)
            {
                var type = asm.GetType(item + "." + fullName);
                if (type != null) return Activator.CreateInstance(type);
            }
            return null;
        }
        public static object Create(string strFullyQualifiedName)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null) return Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                //  type = asm.GetType(strFullyQualifiedName);
                type = GetType(asm, strFullyQualifiedName);
                if (type != null) return Activator.CreateInstance(type);
            }
            return null;
        }
        /// <summary>
        /// Cria uma instância com tipo e parâmetros definidos.
        /// </summary>
        /// <param name="type">O tipo da instância a ser criada.</param>
        /// <param name="args">Os parâmetros da instância.</param>
        /// <returns>A instância. Uma conversão explicita será necessária.</returns>
        public static object Create(System.Type type, object[] args)
        {
            System.Type t = type.Assembly.GetType(type.FullName);
            return t.InvokeMember(string.Empty,
                BindingFlags.DeclaredOnly | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.CreateInstance, null, null, args);
        }
        /// <summary>
        /// Cria uma instância de uma classe presente em um assembly.
        /// </summary>
        /// <param name="assemblyFile">O assembly.</param>
        /// <param name="typeFullName">Nome completo do tipo.</param>
        /// <param name="args">Os argumentos.</param>
        /// <returns>A instância. Uma conversão explicita será necessária.</returns>
		public static object Create(string assemblyFile, string typeFullName, object[] args)
        {
            Assembly a = Assembly.LoadFrom(assemblyFile);
            System.Type type = a.GetType(typeFullName);
            return type.InvokeMember(string.Empty,
                BindingFlags.DeclaredOnly | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.CreateInstance, null, null, args);
        }
        /// <summary>
        /// Lê a o valor da propriedade em um objeto.
        /// </summary>
        /// <param name="obj">O objeto.</param>
        /// <param name="propertyName">Nome da propriedade.</param>
        /// <returns></returns>
		public static object Get(object obj, string propertyName)
        {
            System.Type t = obj.GetType();
            PropertyInfo[] property = t.GetProperties();
            return t.InvokeMember(propertyName,
                BindingFlags.DeclaredOnly | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.GetProperty, null, obj, null);
        }
        /// <summary>
        /// Grava um valor na propriedade de um objeto.
        /// </summary>
        /// <param name="obj">O objeto.</param>
        /// <param name="propertyName">Nome da propriedade.</param>
        /// <param name="value">O valor.</param>
		public static void Set(object obj, string propertyName, object value)
        {
            System.Type t = obj.GetType();
            PropertyInfo[] property = t.GetProperties();
            t.InvokeMember(propertyName,
                BindingFlags.DeclaredOnly | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.SetProperty, null, obj, new Object[] { value });
        }
        /// <summary>
        /// Executa um método de um objeto.
        /// </summary>
        /// <param name="obj">O objeto.</param>
        /// <param name="methodName">Nome do método.</param>
        /// <param name="args">Os parâmetros do método.</param>
        /// <returns>O retorto do método. Uma conversão explicita pode ser necessária.</returns>
		public static object Method(object obj, string methodName, object[] args)
        {
            System.Type t = obj.GetType();
            PropertyInfo[] property = t.GetProperties();
            return t.InvokeMember(methodName,
                BindingFlags.DeclaredOnly | BindingFlags.Public |
                BindingFlags.Instance | BindingFlags.InvokeMethod, null, obj, args);
        }

      

    }
}
