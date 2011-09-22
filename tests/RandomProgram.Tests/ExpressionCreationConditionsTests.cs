namespace Dinh.RandomProgram.Tests
{
    using System.Linq.Expressions;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests for Expression Helper.
    /// </summary>
    [TestFixture]
    public class ExpressionCreationConditionsTests
    {
        [Test]
        public void NoneHasMaxDepthOfIntMaxValue() {
            // Arrange
            // Act
            var condition = ExpressionCreationConditions.None;

            // Assert
            Assert.AreEqual(int.MaxValue, condition.MaxDepth);
        }
    }
}
