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
    public class ExpressionCreatorTests
    {
        [Test]
        public void CreateConstant_ResultIsConstant() {
            // Arrange
            Mock<IObjectCreator> objectCreator = new Mock<IObjectCreator>();
            objectCreator.Setup(oc => oc.CanCreate(It.IsAny<ExpressionCreationConditions>(), It.IsAny<ExpressionCreationContext>())).Returns(true);
            objectCreator.Setup(oc => oc.CreateObject(It.IsAny<ExpressionCreationConditions>(), It.IsAny<ExpressionCreationContext>())).Returns(1);
            var creator = new ExpressionCreator {
                NewObjectCreator = objectCreator.Object
            };

            // Act
            var expression = creator.Create(ExpressionType.Constant);

            // Assert
            Assert.AreEqual(ExpressionType.Constant, expression.NodeType);
        }

        [Test]
        public void CreateExpression_NewExpressionCallbackIsCalled() {
            // Arrange
            var constant = ConstantExpression.Constant(1);
            var creator = new ExpressionCreator();
            creator.NewExpressionCallback = (notUsed1, notUsed2) => {
                return constant;
            };

            // Act
            var expresssion = creator.Create(ExpressionType.Subtract);

            // Assert
            Assert.AreEqual(ExpressionType.Subtract, expresssion.NodeType);
            Assert.AreSame(constant, ((BinaryExpression)expresssion).Left);
            Assert.AreSame(constant, ((BinaryExpression)expresssion).Right);
        }

        [Test]
        public void CreateExpression_RequestSpecificReturnType() {
            // Arrange
            var creator = new ExpressionCreator {
                NewExpressionCallback = (conditions, context) => {
                    if (context.RequestedReturnType == typeof(int)) {
                        return ConstantExpression.Constant(1);
                    }

                    throw new InvalidOperationException();
                }
            };

            // Act
            Expression expression = creator.Create(ExpressionType.Multiply, typeof(int));

            // Assert
            Assert.AreEqual(typeof(int), expression.Type);
        }

        [Test]
        public void CreateExpression_CanNotCreateObject() {
            // Arrange
            var objectCreator = new Mock<IObjectCreator>();
            objectCreator.Setup(oc => oc.CanCreate(It.IsAny<ExpressionCreationConditions>(), It.IsAny<ExpressionCreationContext>())).Returns(false).Verifiable();
            objectCreator.Setup(oc => oc.CreateObject(It.IsAny<ExpressionCreationConditions>(), It.IsAny<ExpressionCreationContext>())).Throws(new InvalidOperationException());
            var expressionCreator = new ExpressionCreator {
                NewObjectCreator = objectCreator.Object
            };

            // Act
            expressionCreator.Create(ExpressionType.Constant);

            // Assert
            objectCreator.Verify();
        }

        [Test]
        public void CreateExpression_CanNotCreateExpression() {
            // Arrange
            var objectCreator = new Mock<IObjectCreator>();
            var expressionCreator = new ExpressionCreator {
                NewExpressionCallback = null
            };

            // Act

            // Assert
        }

        [Test]
        [Ignore]
        public void ThrowExceptionIfSupportedTypesAreExhausted() {
            // Arrange
            var objectCreator = new Mock<IObjectCreator>();
            objectCreator.Setup(oc => oc.CanCreate(It.IsAny<ExpressionCreationConditions>(), It.IsAny<ExpressionCreationContext>())).Returns(false).Verifiable();
            var expressionCreator = new ExpressionCreator();
            expressionCreator.NewObjectCreator = objectCreator.Object;

            // Act
            Expression expression = expressionCreator.Create(ExpressionType.Multiply, typeof(int));

            // Assert
            Assert.AreEqual(typeof(int), expression.Type);
        }
    }
}
