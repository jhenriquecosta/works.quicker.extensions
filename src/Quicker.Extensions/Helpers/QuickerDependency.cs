using Quicker.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicker.Helpers
{
    public class QuickerDependency
    {
        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">Type of the object to get</typeparam>
        /// <returns>The instance object</returns>
        public static T Get<T>()
        {
            return IocManager.Instance.Resolve<T>();
        }
        public static object Get(Type type)
        {
            return IocManager.Instance.Resolve(type);

        }
    }
}
