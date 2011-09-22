namespace Dinh.RandomProgram.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using NUnit.Framework;

    [TestFixture]
    public class WeightedExpressionTypeProviderTests
    {
        [Test]
        public void TypeDistribution_AllValuesStartedAreEqual() {
            // Arrange
            var weighted = new WeightedExpressionTypeProvider();
            var allExpressionTypes = Enum.GetValues(typeof(ExpressionType));
            var supportedExpressionTypes = new List<ExpressionType>();

            foreach (ExpressionType expressionType in allExpressionTypes) {
                if (!WeightedExpressionTypeProvider.UnsupportedTypes.Contains(expressionType)) {
                    supportedExpressionTypes.Add(expressionType);
                }
            }

            // Act
            // Assert
            for (int i = 1; i < supportedExpressionTypes.Count; ++i) {
                Assert.AreEqual(
                    weighted.GetTypeDistribution((ExpressionType)supportedExpressionTypes[i]),
                    weighted.GetTypeDistribution((ExpressionType)supportedExpressionTypes[i - 1]));
            }
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldNotAllowNegativeWeight() {
            // Arrange
            var weighted = new WeightedExpressionTypeProvider();

            // Act
            // Assert
            weighted.SetTypeDistribution(ExpressionType.Subtract, -1);
        }

        [Test]
        public void TypeDistribution_SelectUnlikelyExpressionType() {
            // Arrange
            var weighted = new WeightedExpressionTypeProvider();
            foreach (ExpressionType type in Enum.GetValues(typeof(ExpressionType))) {
                weighted.SetTypeDistribution(type, 0);
            }

            // Act
            weighted.SetTypeDistribution(ExpressionType.Add, 100000000);
            weighted.SetTypeDistribution(ExpressionType.Subtract, 0.00001);
            var expressionType = weighted.NextExpressionType();

            // Assert
            Assert.AreEqual(ExpressionType.Add, expressionType);
        }

        [Test]
        public void OnlyReturnPositiveExpressionTypes() {
            // Arrange
            var weighted = new WeightedExpressionTypeProvider();
            foreach (ExpressionType type in Enum.GetValues(typeof(ExpressionType))) {
                weighted.SetTypeDistribution(type, 0);
            }

            ExpressionType[] allowed = {
                                           ExpressionType.Add,
                                           ExpressionType.Subtract,
                                           ExpressionType.Constant
                                       };

            foreach (ExpressionType type in allowed) {
                weighted.SetTypeDistribution(type, 1);
            }

            // Act
            // Assert
            for (int i = 0; i < 10; i++) {
                var expressionType = weighted.NextExpressionType();

                bool typeFound = false;

                foreach (ExpressionType type in allowed) {
                    if (expressionType == type) {
                        typeFound = true;
                        continue;
                    }
                }

                Assert.IsTrue(typeFound);
            }
        }
    }
}
