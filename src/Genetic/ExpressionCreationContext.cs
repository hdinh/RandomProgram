namespace Dinh.RandomProgram
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the state of the epxression creation when it tries to create the expression.
    /// </summary>
    /// <remarks>Value Object</remarks>
    public class ExpressionCreationContext : ICloneable<ExpressionCreationContext>
    {
        private List<Type> evaluatedDataTypes;
        private List<Exception> evaluationExceptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionCreationContext"/> class.
        /// </summary>
        public ExpressionCreationContext() {
            this.CurrentDepth = 1;
            this.evaluatedDataTypes = new List<Type>();
        }

        /// <summary>
        /// Gets or sets the current depth of evaluation tree the current evaluation is at.
        /// </summary>
        /// <value>The current depth.</value>
        public int CurrentDepth { get; set; }

        /// <summary>
        /// Gets or sets the type of the requested return.
        /// </summary>
        /// <value>The type of the requested return.</value>
        public Type RequestedReturnType { get; set; }

        /// <summary>
        /// Gets the data types that were evaluated at the same level when evaluating this expression.
        /// </summary>
        /// <value>The data types.</value>
        public IList<Type> EvaluatedDataTypes {
            get {
                return this.evaluatedDataTypes;
            }
        }

        public IList<Exception> EvaluationExceptions {
            get {
                if (this.evaluationExceptions == null) {
                    this.evaluationExceptions = new List<Exception>();
                }

                return this.evaluationExceptions;
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public ExpressionCreationContext Clone() {
            return new ExpressionCreationContext {
                CurrentDepth = this.CurrentDepth,
                RequestedReturnType = this.RequestedReturnType,
                evaluatedDataTypes = new List<Type>(this.evaluatedDataTypes),
            };
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        object ICloneable.Clone() {
            return this.Clone();
        }
    }
}
