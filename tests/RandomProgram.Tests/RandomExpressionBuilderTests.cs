namespace Dinh.RandomProgram.Tests
{
    using System;
    using System.Linq.Expressions;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests for Expression Helper.
    /// </summary>
    [TestFixture]
    public class RandomExpressionBuilderTests
    {
        [Test]
        public void CreateMultipleRandomExpressions_ExpressionsAreDifferent() {
            // Arrange
            MockTestObjectCreator objectCreator = new MockTestObjectCreator(() => { return 5; });
            var builder = new RandomExpressionBuilder();
            builder.ExpressionCreator.NewObjectCreator = objectCreator;

            // Act
            var exp1 = builder.NewExpression(ExpressionType.Constant);
            var exp2 = builder.NewExpression(ExpressionType.Constant);

            // Assert
            Assert.AreNotSame(exp1, exp2);
        }

        [Test]
        public void NewExpression_Regresssion1() {
            // Arrange
            MockTestObjectCreator objectCreator = new MockTestObjectCreator(() => { return 1; });
            var builder = new RandomExpressionBuilder(seed: 15465);
            builder.NewExpressionTypeProvider = CreateSignelTypeMockTypeProvider(ExpressionType.And);
            builder.ExpressionCreator.NewExpressionCallback = (notUsed1, notUsed2) => {
                return Expression.Constant(false);
            };

            builder.ExpressionCreator.NewObjectCreator = objectCreator;

            // Act
            Expression exp = builder.NewExpression();

            // Assert
            Assert.AreEqual(ExpressionType.And, exp.NodeType);
        }

        [Test]
        public void MaxDepthLimitsCreation() {
            // Arrange
            var builder = new RandomExpressionBuilder();
            builder.CreationConditions = new ExpressionCreationConditions(maxDepth: 2);
            builder.NewExpressionTypeProvider = CreateSignelTypeMockTypeProvider(ExpressionType.Add);
            MockTestObjectCreator objectCreator = new MockTestObjectCreator(() => { return 12; });
            builder.ExpressionCreator.NewObjectCreator = objectCreator;

            // Act
            var expression = builder.NewExpression();

            // Assert
            Assert.AreEqual(ExpressionType.Add, expression.NodeType);
            Assert.AreEqual(12, ((ConstantExpression)((BinaryExpression)expression).Left).Value);
            Assert.AreEqual(12, ((ConstantExpression)((BinaryExpression)expression).Right).Value);
        }

        [Test]
        public void NewExpression_MaxDepth1FromNewExpression_ShouldReturnDepth1() {
            // Arrange
            var builder = new RandomExpressionBuilder();
            MockTestObjectCreator objectCreator = new MockTestObjectCreator(() => { return 4; });
            builder.ExpressionCreator.NewObjectCreator = objectCreator;
            var conditions = new ExpressionCreationConditions(maxDepth: 1);

            // Act
            var expression = (ConstantExpression)builder.NewExpression(ExpressionType.Add, conditions);

            // Assert
            Assert.AreEqual(4, expression.Value);
        }

        [Test]
        public void NewExpression_MaxDepth1FromBuilder_ShouldReturnDepth1() {
            // Arrange
            var builder = new RandomExpressionBuilder();
            MockTestObjectCreator objectCreator = new MockTestObjectCreator(() => { return 123; });
            builder.ExpressionCreator.NewObjectCreator = objectCreator;
            builder.CreationConditions = new ExpressionCreationConditions(maxDepth: 1);
            var creationConditions = new ExpressionCreationConditions(maxDepth: 1000);

            // Act
            var expression = (ConstantExpression)builder.NewExpression(ExpressionType.Add, creationConditions);

            // Assert
            Assert.AreEqual(123, expression.Value);
        }

        [Test]
        [Explicit]
        public void NewExpression_Regresssion2() {
            // Arrange
            var builder = new RandomExpressionBuilder();
            builder.CreationConditions = new ExpressionCreationConditions(maxDepth: 3);
            MockTestObjectCreator objectCreator = new MockTestObjectCreator(() => { return DateTime.Now; });
            builder.ExpressionCreator.NewObjectCreator = objectCreator;

            // Act
            var expresssion = builder.NewExpression();

            // Assert
            Assert.IsNotNull(expresssion);
        }

        [Test]
        public void ExpressionTypeRegression_Add() {
            // Arrange
            var builder = new RandomExpressionBuilder();
            builder.CreationConditions = new ExpressionCreationConditions(maxDepth: 2);
            MockTestObjectCreator objectCreator = new MockTestObjectCreator((context) => {
                if (context.EvaluatedDataTypes.Count == 0) {
                    return 1;
                } else if (context.EvaluatedDataTypes[0] == typeof(int)) {
                    return 2;
                }

                throw new InvalidOperationException();
            });
            builder.ExpressionCreator.NewObjectCreator = objectCreator;

            // Act
            var expression = builder.NewExpression(ExpressionType.Add);

            // Assert
            Assert.AreEqual(ExpressionType.Add, expression.NodeType);
            Assert.AreEqual(1, ((ConstantExpression)((BinaryExpression)expression).Left).Value);
            Assert.AreEqual(2, ((ConstantExpression)((BinaryExpression)expression).Right).Value);
        }

        [Test]
        public void NewExpression_RequestExpressionFromBuilder_CreateSpecificType() {
            // Arrange
            var builder = new RandomExpressionBuilder();
            builder.CreationConditions = new ExpressionCreationConditions(maxDepth: 2, requestedType: typeof(double));

            // Act
            // Assert
            this.TestRequestReturnOfDoubleType(builder, ExpressionCreationConditions.None);
        }

        [Test]
        public void NewExpression_RequestExpressionFromNewExpresssion_CreateSpecificType() {
            // Arrange
            var builder = new RandomExpressionBuilder();
            var conditions = new ExpressionCreationConditions(maxDepth: 2, requestedType: typeof(double));

            // Act
            // Assert
            this.TestRequestReturnOfDoubleType(builder, conditions);
        }

        [Test]
        public void NewExpression_RequestExpressionFromBothBuilderAndNewExpresssion_OverrideWithBuilderConditions() {
            // Arrange
            var builder = new RandomExpressionBuilder();
            builder.CreationConditions = new ExpressionCreationConditions(maxDepth: 2, requestedType: typeof(double));
            var conditions = new ExpressionCreationConditions(maxDepth: 50, requestedType: typeof(string));

            // Act
            // Assert
            this.TestRequestReturnOfDoubleType(builder, conditions);
        }

        [Test]
        [Ignore]
        public void CreateValidDepth5Expression() {
            // Arrange
            var builder = new RandomExpressionBuilder();
            builder.CreationConditions = new ExpressionCreationConditions(maxDepth: 2);
            MockTestObjectCreator objectCreator = new MockTestObjectCreator(() => { return 2; });
            builder.ExpressionCreator.NewObjectCreator = objectCreator;

            // Act
            var expression = builder.NewExpression(ExpressionType.ArrayIndex);

            //// Assert
            ////Assert.AreEqual(123, expression.Value);
        }

        private static IExpressionTypeProvider CreateSignelTypeMockTypeProvider(ExpressionType wantedType) {
            Mock<IExpressionTypeProvider> mock = new Mock<IExpressionTypeProvider>();
            mock.Setup(typeProvider => typeProvider.NextExpressionType()).Returns(wantedType);
            return mock.Object;
        }

        private void TestRequestReturnOfDoubleType(RandomExpressionBuilder builder, ExpressionCreationConditions conditions) {
            // Arrange
            MockTestObjectCreator objectCreator = new MockTestObjectCreator((context) => {
                if (context.RequestedReturnType == typeof(double)) {
                    return 2.5;
                }

                throw new InvalidOperationException();
            });
            builder.ExpressionCreator.NewObjectCreator = objectCreator;

            // Act
            var expression = builder.NewExpression(ExpressionType.Subtract, conditions);

            // Assert
            Assert.AreEqual(ExpressionType.Subtract, expression.NodeType);
            Assert.AreEqual(typeof(double), expression.Type);
            Assert.AreEqual(typeof(double), ((ConstantExpression)((BinaryExpression)expression).Left).Type);
            Assert.AreEqual(typeof(double), ((ConstantExpression)((BinaryExpression)expression).Right).Type);
        }
    }
}
