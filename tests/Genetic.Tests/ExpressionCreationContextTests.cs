namespace Dinh.RandomProgram.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using NUnit.Framework;

    /// <summary>
    /// Tests for Expression Creation Conditions.
    /// </summary>
    [TestFixture]
    public class ExpressionCreationContextTests
    {
        [Test]
        public void CloneReturnsEqualObject() {
            // Arrange
            var evaluatedDataTypes = new List<Type> { typeof(int), typeof(char), typeof(DateTime) };
            var context = new ExpressionCreationContext() {
                    CurrentDepth = 10,
                    RequestedReturnType = typeof(int),
                };

            // Use reflection to inject evaluatedDataTypes
            typeof(ExpressionCreationContext).GetField("evaluatedDataTypes", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(context, evaluatedDataTypes);

            // Act
            var cloned = context.Clone();

            // Assert
            Assert.AreNotSame(cloned, context); // Not the same reference.
            Assert.AreEqual(10, cloned.CurrentDepth);
            Assert.AreEqual(typeof(int), cloned.RequestedReturnType);
            Assert.AreEqual(evaluatedDataTypes, cloned.EvaluatedDataTypes);
        }
    }
}
