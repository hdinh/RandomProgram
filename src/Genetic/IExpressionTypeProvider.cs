namespace Dinh.RandomProgram
{
    using System.Linq.Expressions;

    /// <summary>
    /// Provider new expression types.
    /// </summary>
    public interface IExpressionTypeProvider
    {
        /// <summary>
        /// Gets the next type of the expression.
        /// </summary>
        /// <returns>New expression Type.</returns>
        ExpressionType NextExpressionType();
    }
}
