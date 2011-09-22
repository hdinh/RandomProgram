namespace Dinh.RandomProgram
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    public sealed class WeightedTypeRepository : ITypeRepository
    {
        private readonly List<Type> typeRepository = new List<Type>();
        private readonly List<MethodInfo> methodRepository = new List<MethodInfo>();
        private readonly Dictionary<Type, List<MethodInfoWeight>> typeToMethod = new Dictionary<Type, List<MethodInfoWeight>>();

        public WeightedTypeRepository() {
            this.OrderingTransformation = InternalMethodOrderingStrategies.ShuffledByWeight;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Exposed only internally (maybe). Maybe make internal.")]
        public Func<IEnumerable<MethodInfoWeight>, IEnumerable<MethodInfo>> OrderingTransformation {
            get;
            set;
        }

        /// <summary>
        /// Adds the type to the repository.
        /// </summary>
        /// <param name="type">The type to add.</param>
        public void AddType(Type type) {
            var criteria = new WeightedTypeCriteria<MethodInfo> {
                Match = (method) => { return true; },
                Weight = 1.0
            };

            this.AddType(type, criteria);
        }

        public void AddType(Type type, WeightedTypeCriteria<MethodInfo> criteria) {
            Contract.Requires<ArgumentNullException>(criteria.Match != null);

            if (!this.typeRepository.Contains(type)) {
                this.typeRepository.Add(type);
            }

            var typeMethods = type.GetMethods();
            foreach (MethodInfo method in typeMethods) {
                if (criteria.Match(method)) {
                    this.AddMethodInternal(method, criteria.Weight);
                }
            }
        }

        public void AddMethod(Delegate method) {
            this.AddMethod(method, 1.0);
        }

        public void AddMethod(Delegate method, double weight) {
            this.AddMethodInternal(method.Method, weight);
        }

        public void AddAssembly(Assembly assembly) {
            var criteria = new WeightedTypeCriteria<Type> {
                Match = (type) => { return true; },
                Weight = 1.0
            };

            this.AddAssembly(assembly, criteria);
        }

        public void AddAssembly(Assembly assembly, WeightedTypeCriteria<Type> criteria) {
            Contract.Requires<ArgumentNullException>(criteria.Match != null);

            foreach (Type type in assembly.GetTypes()) {
                if (criteria.Match(type)) {
                    this.AddType(type);
                }
            }
        }

        public bool HasType(Type type) {
            return this.typeToMethod.ContainsKey(type);
        }

        /// <summary>
        /// Gets the methods that reduce to this type. ORDER DOES MATTER.
        /// </summary>
        /// <param name="returnType">Type of the return.</param>
        /// <returns>Methods that return type.</returns>
        public IEnumerable<MethodInfo> GetMethods(Type returnType) {
            if (!this.typeToMethod.ContainsKey(returnType)) {
                yield break;
            }

            if (this.OrderingTransformation == null) {
                throw new InvalidOperationException("OrderingTransformation not specified.");
            }

            foreach (MethodInfo method in this.OrderingTransformation(this.typeToMethod[returnType])) {
                yield return method;
            }
        }

        private void AddMethodInternal(MethodInfo method, double weight) {
            /* If method hasn't been added yet, add it. */
            if (!this.methodRepository.Contains(method)) {
                /* For now, guard condition to make sure method is callable without object state. */
                if (method.IsStatic) {
                    this.methodRepository.Add(method);

                    /* Add interfaces and  return type */
                    var methodsToAdd = method.ReturnType.GetBaseTypes();

                    foreach (Type methodToAdd in methodsToAdd) {
                        if (!this.typeToMethod.ContainsKey(methodToAdd)) {
                            this.typeToMethod.Add(methodToAdd, new List<MethodInfoWeight>());
                        }

                        var methodWeightPair = new MethodInfoWeight(method, weight);
                        this.typeToMethod[methodToAdd].Add(methodWeightPair);
                    }
                }
            }
        }

        /// <summary>
        /// Pair of Expression Type and weight.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible", Justification = "Only needed for OrderingTransformation property.")]
        public sealed class MethodInfoWeight
        {
            public MethodInfoWeight(MethodInfo method, double weight) {
                this.Method = method;
                this.Weight = weight;
            }

            /// <summary>
            /// Gets the method info associated with this instance.
            /// </summary>
            /// <value>The expression type.</value>
            public MethodInfo Method { get; private set; }

            /// <summary>
            /// Gets or sets the weight associated with this method info.
            /// </summary>
            /// <value>The weight.</value>
            public double Weight { get; set; }
        }

        private static class InternalMethodOrderingStrategies
        {
            internal static IEnumerable<MethodInfo> ShuffledByWeight(IEnumerable<MethodInfoWeight> original) {
                Random randomEngine = new Random(Environment.TickCount);
                double totalWeight = 0;
                List<MethodInfoWeight> visited = new List<MethodInfoWeight>();
                foreach (MethodInfoWeight methodWeight in original) {
                    totalWeight += methodWeight.Weight;
                }

                if (totalWeight == 0) {
                    Debug.Fail("No weight for types.");
                    yield break;
                }

                List<MethodInfoWeight> remainingCandidates = new List<MethodInfoWeight>(original);

                while (remainingCandidates.Count > 0) {
                    MethodInfoWeight pickedMethodWeight = null;
                    double runningWeight = 0;
                    double normalizedIndex = randomEngine.NextDouble();

                    foreach (MethodInfoWeight methodWeight in remainingCandidates) {
                        runningWeight += methodWeight.Weight;
                        double normalizedWeight = runningWeight / totalWeight;

                        if (normalizedWeight > normalizedIndex) {
                            yield return methodWeight.Method;
                            pickedMethodWeight = methodWeight;
                            break;
                        }
                    }

                    /* Bookkeeping. */
                    Debug.Assert(pickedMethodWeight != null, "Impossible");
                    remainingCandidates.Remove(pickedMethodWeight);
                    totalWeight -= pickedMethodWeight.Weight;
                }
            }
        }
    }
}
