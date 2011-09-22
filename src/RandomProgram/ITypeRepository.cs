namespace Dinh.RandomProgram
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    /// <summary>
    /// Provides a repository of types that can be consumed to create objects.
    /// </summary>
    [ContractClass(typeof(TypeRepositoryContract))]
    public interface ITypeRepository
    {
        void AddType(Type type);

        void AddMethod(Delegate method);

        void AddAssembly(Assembly assembly);

        bool HasType(Type type);

        IEnumerable<MethodInfo> GetMethods(Type returnType);
    }

    [ContractClassFor(typeof(ITypeRepository))]
    internal abstract class TypeRepositoryContract : ITypeRepository
    {
        public void AddType(Type type) {
            Contract.Requires<ArgumentNullException>(type != null);
        }

        public void AddMethod(Delegate method) {
            Contract.Requires<ArgumentNullException>(method != null);
        }

        public void AddAssembly(Assembly assembly) {
            Contract.Requires<ArgumentNullException>(assembly != null);
        }

        public bool HasType(Type type) {
            Contract.Requires<ArgumentNullException>(type != null);
            throw new NotImplementedException();
        }

        public IEnumerable<MethodInfo> GetMethods(Type type) {
            throw new NotImplementedException();
        }
    }
}
