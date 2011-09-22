namespace Dinh.RandomProgram.Tests
{
    using System;
    using System.IO;
    using System.Linq.Expressions;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests for DynamicProgram.
    /// </summary>
    [TestFixture]
    public class DynamicProgramTests
    {
        [Test]
        [Ignore]
        public void BrainStorm() {
            IObjectCreator objectCreator = new MockTestObjectCreator(() => { return 7; });
            var factory = new RandomExpressionBuilder();
            factory.CreationConditions = new ExpressionCreationConditions(maxDepth: 5);
            factory.ExpressionCreator.NewObjectCreator = objectCreator;

            // Act
            var expression = factory.NewExpression();

            ////RandomExpressionBuilder builder = new RandomExpressionBuilder();
            ////builder.CreationConditions = new ExpressionCreationConditions(maxDepth: 3);
            ////Expression e = builder.NewExpression();
            LambdaExpression func = Expression.Lambda<Func<int>>(expression);

            DynamicProgram program = new DynamicProgram(func);
            string filename = Path.ChangeExtension(Path.GetRandomFileName(), "dll");
            program.Export(filename);
        }

        public void BrainStorm1() {
            var factory = new RandomExpressionBuilder();
            IObjectCreator objectCreator = new MockTestObjectCreator(() => { return 2; });
            factory.CreationConditions = new ExpressionCreationConditions(maxDepth: 2);
            factory.ExpressionCreator.NewObjectCreator = objectCreator;

            // Act
            var expression = factory.NewExpression(ExpressionType.Add);

            ////RandomExpressionBuilder builder = new RandomExpressionBuilder();
            ////builder.CreationConditions = new ExpressionCreationConditions(maxDepth: 3);
            ////Expression e = builder.NewExpression();
            LambdaExpression func = Expression.Lambda<Action>(expression);

            DynamicProgram program = new DynamicProgram(func);
            string filename = Path.ChangeExtension(Path.GetRandomFileName(), "dll");
            program.Export(filename);
        }
    }
}
