using Quicker.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quicker.Reflection
{
    public abstract class SingletonInstance<TClass> where TClass : class
    {
        private static readonly Lazy<TClass> lazy = new Lazy<TClass>(() => QuickerReflection.CreateInstance<TClass>());     
        public static TClass Instance { get { return lazy.Value; } }
    }
}
