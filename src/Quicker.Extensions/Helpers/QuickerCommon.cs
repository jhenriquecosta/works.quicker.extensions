using System;
using System.Runtime.InteropServices;

namespace Quicker.Helpers
{
    public static class QuickerCommon
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static Type GetType<T>()
        {
            return GetType(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public static Type GetType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Line => Environment.NewLine;

        public static bool IsLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        /// <summary>
        /// 
        /// </summary>
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        /// <summary>
        /// 
        /// </summary>
        public static bool IsOsx => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        /// <summary>
        /// 
        /// </summary>
        public static string System => IsWindows ? "Windows" : IsLinux ? "Linux" : IsOsx ? "OSX" : string.Empty;
    }
}
