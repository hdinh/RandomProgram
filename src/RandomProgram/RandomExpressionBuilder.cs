namespace Dinh.RandomProgram
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents the random creation of expressions.
    /// </summary>
    public sealed class RandomExpressionBuilder
    {
        private ExpressionCreator expressionCreator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomExpressionBuilder"/> class.
        /// </summary>
        public RandomExpressionBuilder() : this(-1) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomExpressionBuilder"/> class.
        /// </summary>
        /// <param name="seed">The random seed.</param>
        public RandomExpressionBuilder(int seed) {
            this.CreationConditions = ExpressionCreationConditions.None;
            this.NewExpressionTypeProvider = new WeightedExpressionTypeProvider(seed);
        }

        /// <summary>
        /// Gets or sets the creation conditions when creating expresssions.
        /// </summary>
        /// <remarks>The creation of expressions cannot be less loose than this.</remarks>
        /// <value>The creation conditions.</value>
        public ExpressionCreationConditions CreationConditions { get; set; }

        /// <summary>
        /// Gets or sets the expression creator that creates expression in this factory.
        /// </summary>
        /// <value>The expression creator.</value>
        public ExpressionCreator ExpressionCreator {
            get {
                if (this.expressionCreator == null) {
                    this.expressionCreator = new ExpressionCreator {
                        NewExpressionCallback = this.NewExpression
                    };
                }

                return this.expressionCreator;
            }

            set {
                this.expressionCreator = value;
            }
        }

        /// <summary>
        /// Gets or sets the new expression type provider.
        /// </summary>
        /// <value>The new expression type provider.</value>
        public IExpressionTypeProvider NewExpressionTypeProvider { get; set; }

        /// <summary>
        /// Creates a new random expression.
        /// </summary>
        /// <returns>New random expression.</returns>
        public Expression NewExpression() {
            return this.NewExpression(new ExpressionCreationContext());
        }

        /// <summary>
        /// Creates a new the expression with specific expression type.
        /// </summary>
        /// <param name="requestedType">The requested type of the root expression.</param>
        /// <returns>New random expression.</returns>
        public Expression NewExpression(ExpressionType requestedType) {
            return this.NewExpression(requestedType, ExpressionCreationConditions.None);
        }

        /// <summary>
        /// Creates a new the expression with specific expression type.
        /// </summary>
        /// <remarks>
        /// This method is really only meant for tests, so this will throw an exception if it can't succeed.
        /// </remarks>
        /// <param name="requestedType">The requested type of the root expression.</param>
        /// <param name="creationConditions">The creation conditions.</param>
        /// <returns>New random expression.</returns>
        public Expression NewExpression(ExpressionType requestedType, ExpressionCreationConditions creationConditions) {
            try {
                Expression result = this.NewExpressionInternal(requestedType, creationConditions, new ExpressionCreationContext());
                return result;
            } catch (Exception e) {
                throw new ExpressionCreationException(string.Format(CultureInfo.InvariantCulture, "Expression of type {0} could not be created", requestedType), e);
            }
        }

        /// <summary>
        /// Creates a new random expression.
        /// </summary>
        /// <param name="creationContext">The context of the creation evaluation.</param>
        /// <returns>New random expression.</returns>
        private Expression NewExpression(ExpressionCreationContext creationContext) {
            return this.NewExpression(ExpressionCreationConditions.None, creationContext);
        }

        /// <summary>
        /// Creates a new random expression.
        /// </summary>
        /// <remarks>
        /// Tries to feed all the expression types into the expresssion
        /// creator until an expresssion is successfully created.
        /// </remarks>
        /// <param name="conditions">The creation conditions.</param>
        /// <param name="creationContext">The context of the creation evaluation.</param>
        /// <returns>New random expression.</returns>
        private Expression NewExpression(ExpressionCreationConditions conditions, ExpressionCreationContext creationContext) {
            List<ExpressionType> failedExpressionTypes = null;
            ExpressionType randomType = default(ExpressionType);

            ExpressionCreationContext currentContext = creationContext.Clone();
            if (currentContext.RequestedReturnType == null) {
                // Our current expression context requested type is the builder's requested type.
                currentContext.RequestedReturnType = conditions.RequestedReturnType;
            }

            Expression newExpression;

            // TODO: Ignore: You WERE HERE !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            for (;;) {
                try {
                    for (;;) {
                        // TODO: This block will loop indefinitely unless we detect when valid expression types are exhausted.
                        randomType = this.NewExpressionTypeProvider.NextExpressionType();

                        /* If this expression type has not failed yet, try it. */
                        if (failedExpressionTypes == null || !failedExpressionTypes.Contains(randomType)) {
                            break;
                        }
                    }

                    newExpression = this.NewExpressionInternal(randomType, conditions, currentContext);
                    break;
                } catch (ExpressionCreationException) {
                    /* If failed, try another expression type. */
                    if (failedExpressionTypes == null) {
                        failedExpressionTypes = new List<ExpressionType>();
                    }

                    failedExpressionTypes.Add(randomType);
                }
            }

            return newExpression;
        }

        /// <summary>
        /// Creates a new the expression with specific expression type.
        /// </summary>
        /// <param name="requestedType">The requested type of the root expression.</param>
        /// <param name="creationConditions">The creation conditions.</param>
        /// <param name="creationContext">The context of the creation evaluation.</param>
        /// <returns>New random expression.</returns>
        private Expression NewExpressionInternal(ExpressionType requestedType, ExpressionCreationConditions creationConditions, ExpressionCreationContext creationContext) {
            ExpressionCreationConditions strictest = ExpressionCreationConditions.StrictestUnion(this.CreationConditions, creationConditions);
            Expression expression = this.ExpressionCreator.CreateInternal(requestedType, strictest, creationContext);
            return expression;
        }
    }
}
