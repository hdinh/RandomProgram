namespace Dinh.RandomProgram
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a class that can create objects from types.
    /// </summary>
    public interface IExpressionCreator
    {
        /// <summary>
        /// Determines whether this instance can create expression under the specified context.
        /// </summary>
        /// <param name="conditions">The conditions for the expression context.</param>
        /// <param name="context">The context for the expression context.</param>
        /// <returns>
        /// <c>true</c> if this instance can create the expression under the specified conditions; otherwise, <c>false</c>.
        /// </returns>
        bool CanCreate(ExpressionCreationConditions conditions, ExpressionCreationContext context);

        /// <summary>
        /// Creates the expression.
        /// </summary>
        /// <param name="conditions">The conditions for the expression context.</param>
        /// <param name="context">The context for the expression context.</param>
        /// <returns>The constructed expression.</returns>
        Expression CreateExpression(ExpressionCreationConditions conditions, ExpressionCreationContext context);
    }
}
