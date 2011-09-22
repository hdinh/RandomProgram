namespace Dinh.RandomProgram
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Allows expression types to be weighted when generating random expression types.
    /// </summary>
    public sealed class WeightedExpressionTypeProvider : IExpressionTypeProvider
    {
        private static readonly List<ExpressionType> unsupportedTypes = new List<ExpressionType> {
                ExpressionType.Call,
                ExpressionType.Convert,
                ExpressionType.ConvertChecked,
                ExpressionType.Lambda,
                ExpressionType.ListInit,
                ExpressionType.MemberAccess,
                ExpressionType.MemberInit,
                ExpressionType.New,
                ExpressionType.NewArrayInit,
                ExpressionType.NewArrayBounds,
                ExpressionType.Parameter,
                ExpressionType.TypeAs,
                ExpressionType.TypeIs,
                ExpressionType.DebugInfo,
                ExpressionType.Dynamic,
                ExpressionType.Default,
                ExpressionType.Extension,
                ExpressionType.Goto,
                ExpressionType.Index,
                ExpressionType.Label,
                ExpressionType.RuntimeVariables,
                ExpressionType.Try,
                ExpressionType.Unbox,
                ExpressionType.AddAssign,
                ExpressionType.AndAssign,
                ExpressionType.DivideAssign,
                ExpressionType.ExclusiveOrAssign,
                ExpressionType.LeftShiftAssign,
                ExpressionType.ModuloAssign,
                ExpressionType.MultiplyAssign,
                ExpressionType.OrAssign,
                ExpressionType.PowerAssign,
                ExpressionType.RightShiftAssign,
                ExpressionType.SubtractAssign,
                ExpressionType.AddAssignChecked,
                ExpressionType.MultiplyAssignChecked,
                ExpressionType.SubtractAssignChecked,
                ExpressionType.PreIncrementAssign,
                ExpressionType.PreDecrementAssign,
                ExpressionType.PostIncrementAssign,
                ExpressionType.PostDecrementAssign,
                ExpressionType.TypeEqual
        };

        private readonly Random randomEngine;
        private readonly List<ExpressionTypeWeight> edgeToExpressionType;
        private readonly Dictionary<ExpressionType, ExpressionTypeWeight> expressionToEdge;
        private double totalWeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightedExpressionTypeProvider"/> class.
        /// </summary>
        public WeightedExpressionTypeProvider()
            : this(-1) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightedExpressionTypeProvider"/> class.
        /// </summary>
        /// <param name="seed">The random seed. -1 if completely random.</param>
        public WeightedExpressionTypeProvider(int seed) {
            if (seed == -1) {
                seed = Environment.TickCount;
            }

            this.edgeToExpressionType = new List<ExpressionTypeWeight>();
            this.expressionToEdge = new Dictionary<ExpressionType, ExpressionTypeWeight>();
            Array allExpressionTypes = Enum.GetValues(typeof(ExpressionType));

            foreach (ExpressionType expType in allExpressionTypes) {
                double typeWeight = 0.0;
                if (!unsupportedTypes.Contains(expType)) {
                    typeWeight = 1.0;
                }

                var expressionValuePair = new ExpressionTypeWeight(expType, typeWeight);
                this.edgeToExpressionType.Add(expressionValuePair);
                this.expressionToEdge.Add(expType, expressionValuePair);
                this.totalWeight += typeWeight;
            }

            this.randomEngine = new Random(seed);
        }

        /// <summary>
        /// Gets the unsupported types.
        /// </summary>
        /// <value>The unsupported types.</value>
        public static IList<ExpressionType> UnsupportedTypes {
            get {
                return unsupportedTypes;
            }
        }

        /// <summary>
        /// Gets the type distribution weight.
        /// </summary>
        /// <param name="expressionType">Type of the expression.</param>
        /// <returns>Type distribution weight.</returns>
        public double GetTypeDistribution(ExpressionType expressionType) {
            return this.expressionToEdge[expressionType].Weight;
        }

        /// <summary>
        /// Sets the type distribution weight.
        /// </summary>
        /// <param name="expressionType">Type of the expression.</param>
        /// <param name="newWeight">The new weight.</param>
        public void SetTypeDistribution(ExpressionType expressionType, double newWeight) {
            if (newWeight < 0) {
                throw new InvalidOperationException("Weight cannot be negative");
            }

            this.totalWeight += newWeight - this.expressionToEdge[expressionType].Weight;
            var typeWeight = this.expressionToEdge[expressionType];
            typeWeight.Weight = newWeight;
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        /// <returns>Random expression type.</returns>
        public ExpressionType NextExpressionType() {
            double normalizedIndex = this.randomEngine.NextDouble();

            double runningWeight = 0.0;
            foreach (ExpressionTypeWeight expPair in this.edgeToExpressionType) {
                runningWeight += expPair.Weight;
                double normalizedWeight = runningWeight / this.totalWeight;

                if (normalizedWeight > normalizedIndex) {
                    return expPair.Type;
                }
            }

            throw new InvalidOperationException("Impossible.");
        }

        /// <summary>
        /// Pair of Expression Type and weight.
        /// </summary>
        private sealed class ExpressionTypeWeight
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="WeightedExpressionTypeProvider.ExpressionTypeWeight"/> class.
            /// </summary>
            /// <param name="type">The type associated with this instance.</param>
            /// <param name="weight">The weight associated with this expresssion type.</param>
            public ExpressionTypeWeight(ExpressionType type, double weight) {
                this.Type = type;
                this.Weight = weight;
            }

            /// <summary>
            /// Gets the type associated with this instance.
            /// </summary>
            /// <value>The expression type.</value>
            public ExpressionType Type { get; private set; }

            /// <summary>
            /// Gets or sets the weight associated with this expresssion type.
            /// </summary>
            /// <value>The weight.</value>
            public double Weight { get; set; }
        }
    }
}
