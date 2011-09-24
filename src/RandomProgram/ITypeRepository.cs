namespace Dinh.RandomProgram
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    /// <summary>
    /// Provides a repository of types that can be consumed to create objects.
    /// </summary>
    public interface ITypeRepository
    {
        void AddType(Type type);

        void AddMethod(Delegate method);

        void AddAssembly(Assembly assembly);

        bool HasType(Type type);

        IEnumerable<MethodInfo> GetMethods(Type returnType);
    }
}
