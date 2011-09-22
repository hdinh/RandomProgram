namespace Dinh.RandomProgram
{
    using System;

    /// <summary>
    /// Represents the conditions when creating new expressions.
    /// </summary>
    public sealed class ExpressionCreationConditions
    {
        private static readonly ExpressionCreationConditions none = new ExpressionCreationConditions(maxDepth: int.MaxValue);

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionCreationConditions"/> class.
        /// </summary>
        /// <param name="maxDepth">The max depth.</param>
        public ExpressionCreationConditions(int maxDepth)
            : this(maxDepth, null) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionCreationConditions"/> class.
        /// </summary>
        /// <param name="maxDepth">The max depth.</param>
        /// <param name="requestedType">Type of the requested.</param>
        public ExpressionCreationConditions(int maxDepth, Type requestedType) {
            this.MaxDepth = maxDepth;
            this.RequestedReturnType = requestedType;
        }

        /// <summary>
        /// Gets instance of expression that has no conditions.
        /// </summary>
        /// <value>The none conditions.</value>
        public static ExpressionCreationConditions None {
            get {
                return none;
            }
        }

        /// <summary>
        /// Gets the max depth to create the expression depth.
        /// </summary>
        /// <value>The max depth.</value>
        public int MaxDepth { get; private set; }

        /// <summary>
        /// Gets the type of the expression to return.
        /// </summary>
        /// <value>The type of the requested return.</value>
        public Type RequestedReturnType { get; private set; }

        /// <summary>
        /// Returns the strictest ExpressionCreationConditions union from the two values.
        /// </summary>
        /// <param name="val1">The first value.</param>
        /// <param name="val2">The second value.</param>
        /// <returns>Strictest ExpressionCreationConditions made from two values.</returns>
        internal static ExpressionCreationConditions StrictestUnion(ExpressionCreationConditions val1, ExpressionCreationConditions val2) {
            var min = new ExpressionCreationConditions(maxDepth: Math.Min(val1.MaxDepth, val2.MaxDepth)) {
                 RequestedReturnType = val1.RequestedReturnType ?? val2.RequestedReturnType
            };
            return min;
        }
    }
}
