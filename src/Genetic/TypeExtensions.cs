namespace Dinh.RandomProgram
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class TypeExtensions
    {
        /// <summary>
        /// Gets all interfaces and base class of type.
        /// </summary>
        /// <remarks>
        /// http://stackoverflow.com/questions/1823655/given-a-c-type-get-its-base-classes-and-implemented-interfaces
        /// Modified so that method gaurantees types are not repeated.
        /// </remarks>
        /// <param name="type">Type to get interfaces and base class from.</param>
        /// <returns>Collection of base types.</returns>
        internal static IEnumerable<Type> GetBaseTypes(this Type type) {
            if (type.BaseType == null) {
                return Enumerable.Repeat(type, 1)
                                 .Concat(type.GetInterfaces());
            }

            return Enumerable.Repeat(type, 1)
                             .Concat(Enumerable.Repeat(type.BaseType, 1))
                             .Concat(type.GetInterfaces())
                             .Concat(type.GetInterfaces().SelectMany<Type, Type>(GetBaseTypes))
                             .Concat(type.BaseType.GetBaseTypes())
                             .Distinct();
        }
    }
}
