namespace Dinh.RandomProgram.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using NUnit.Framework;

    /// <summary>
    /// Tests for Standard Object Creator interface.
    /// </summary>
    [TestFixture]
    public class DefaultObjectCreatorTests
    {
        [Test]
        public void AddTypeToRepository() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 10);
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(DateTime) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act
            objectCreator.TypeRepository.AddType(typeof(DateTime));
            var created = objectCreator.CreateObject(conditions, context);

            // Assert
            Assert.IsInstanceOf<DateTime>(created);
        }

        [Test]
        public void AddAssemblyToRepository() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 3);
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(GotoExpression) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act
            objectCreator.TypeRepository.AddAssembly(typeof(GotoExpression).Assembly); // System.Core
            var created = objectCreator.CreateObject(conditions, context);

            // Assert
            Assert.IsInstanceOf<GotoExpression>(created);
        }

        [Test]
        public void AddParameterlessMethodToRepository() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 10);
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(int) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act
            objectCreator.TypeRepository.AddMethod(new Func<int>(() => { return 5; }));
            var created = objectCreator.CreateObject(conditions, context);

            // Assert
            Assert.AreEqual(5, created);
        }

        [Test]
        public void AddParameterMethodToRepository() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 10);
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(long) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act
            objectCreator.TypeRepository.AddMethod(new Func<DateTime, DateTime, long>((time1, time2) => { return time1.Ticks + time1.Ticks; }));
            objectCreator.TypeRepository.AddMethod(new Func<DateTime>(() => { return DateTime.Now; }));
            var created = objectCreator.CreateObject(conditions, context);

            // Assert
            Assert.IsInstanceOf<long>(created);
        }

        [Test]
        public void RecoverFromThrownException() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 10);
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(long) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act
            objectCreator.TypeRepository.AddMethod(new Func<long>(() => { throw new InvalidOperationException(); })); // Should recover
            objectCreator.TypeRepository.AddMethod(new Func<long>(() => { return 10; }));
            var created = objectCreator.CreateObject(conditions, context);

            // Assert
            Assert.AreEqual(10, created);
            Assert.IsInstanceOf<Exception>(context.EvaluationExceptions[0]);
        }

        [Test]
        public void StopEvaluatingAppropriatelyWithContext() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 3);
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(int) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act
            objectCreator.TypeRepository.AddMethod(new Func<int, int, int>((int1, int2) => { return int1 + int2; }));
            objectCreator.TypeRepository.AddMethod(new Func<int>(() => { return 1; }));
            var created = objectCreator.CreateObject(conditions, context);

            // Assert
            // - - - - 4 - - - (Depth 1)
            // - - 2 - - - 2 - (Depth 2)
            // - 1 - 1 - 1 - 1 (Depth 3)
            Assert.AreEqual(4, created);
        }

        [Test]
        public void NextTest() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 3);
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(int) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act

            // Assert
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateObjectWithNullContext() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 10);
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();
            objectCreator.TypeRepository.AddType(typeof(DateTime));

            // Act
            objectCreator.CreateObject(conditions, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateObjectWithNullConditions() {
            // Arrange
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(long) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();
            objectCreator.TypeRepository.AddType(typeof(DateTime));

            // Act
            objectCreator.CreateObject(null, context);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTypeNullType() {
            // Arrange
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act
            objectCreator.TypeRepository.AddType(null);
        }

        [Test]
        [ExpectedException(typeof(ObjectCreationException))]
        public void CreateObjectWithoutAnyTypes() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 10);
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(DateTime) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act
            // Assert
            var created = objectCreator.CreateObject(conditions, context);
        }

        [Test]
        [ExpectedException(typeof(ObjectCreationException))]
        public void CreateObjectWithoutSufficientTypes() {
            // Arrange
            var conditions = new ExpressionCreationConditions(maxDepth: 10);
            var context = new ExpressionCreationContext { RequestedReturnType = typeof(int) };
            var objectCreator = new DefaultObjectCreator();
            objectCreator.TypeRepository = GetMockRepository();

            // Act
            objectCreator.TypeRepository.AddMethod(new Func<DateTime, int>((time) => { throw new InvalidOperationException(); }));
            var created = objectCreator.CreateObject(conditions, context);
        }

        private static ITypeRepository GetMockRepository()
        {
            // Setting the seed to make WeightedTypeRepository deterministic.
            var repository = new WeightedTypeRepository();
            repository.OrderingTransformation = DoNotTransform;
            return repository;
        }

        private static IEnumerable<MethodInfo> DoNotTransform(IEnumerable<WeightedTypeRepository.MethodInfoWeight> original) {
            foreach (WeightedTypeRepository.MethodInfoWeight weight in original) {
                yield return weight.Method;
            }
        }
    }
}
