namespace Dinh.RandomProgram.Tests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Net;
    using System.Reflection;
    using NUnit.Framework;

    [TestFixture]
    public class WeightedTypeRepositoryTests
    {
        [Test]
        public void AddTypeWithMatchCriteria_GetMethodBack() {
            // Arrange
            var repository = new WeightedTypeRepository();
            var criteria = new WeightedTypeCriteria<MethodInfo> {
                Match = (method) => {
                    if (method.Name == "Method3") {
                        return true;
                    }

                    return false;
                }
            };

            // Act
            repository.AddType(typeof(MockType), criteria);
            var methods = new List<MethodInfo>(repository.GetMethods(typeof(int)));

            // Assert
            Assert.AreEqual(1, methods.Count);
            Assert.AreEqual("Method3", methods[0].Name);
        }

        [Test]
        public void GetTypeMethodsByInterface() {
            // Arrange
            var repository = new WeightedTypeRepository();
            repository.AddMethod(new Func<int>(() => { return 5; }));

            // Act
            var methods = new List<MethodInfo>(repository.GetMethods(typeof(IComparable<int>)));

            // Assert
            Assert.AreEqual(1, methods.Count);
        }

        [Test]
        public void GetTypeMethodsByBaseType() {
            // Arrange
            var repository = new WeightedTypeRepository();
            repository.AddMethod(new Func<WebClient>(() => { return new WebClient(); }));

            // Act
            var methods = new List<MethodInfo>(repository.GetMethods(typeof(Component)));

            // Assert
            Assert.AreEqual(1, methods.Count);
        }

        [Test]
        public void AddAssemblyWithCriteria() {
            // Arrange
            var repository = new WeightedTypeRepository();
            var criteria = new WeightedTypeCriteria<Type> {
                Match = (type) => {
                    if (type == typeof(MockType)) {
                        return true;
                    }

                    return false;
                }
            };

            // Act
            repository.AddAssembly(typeof(MockType).Assembly, criteria);
            var methods = new List<MethodInfo>(repository.GetMethods(typeof(int)));

            // Assert
            Assert.AreEqual(3, methods.Count);
        }

        [Test]
        public void OrderMethodsByWeightProbability() {
            // Arrange
            var repository = new WeightedTypeRepository();

            // Act
            repository.AddMethod(new Func<int>(MockType.Method1), 0.00001);
            repository.AddMethod(new Func<int>(MockType.Method2), 100000000);
            var methods = new List<MethodInfo>(repository.GetMethods(typeof(int)));

            // Assert
            Assert.AreEqual(2, methods.Count);
            Assert.AreEqual("Method2", methods[0].Name);
            Assert.AreEqual("Method1", methods[1].Name);
        }

        private sealed class MockType
        {
            public static int Method1() {
                return 1;
            }

            public static int Method2() {
                return 2;
            }

            public static int Method3() {
                return 3;
            }
        }
    }
}
