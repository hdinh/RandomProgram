namespace Dinh.RandomProgram
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents creator of expressions.
    /// </summary>
    public sealed class ExpressionCreator
    {
        private static readonly Dictionary<ExpressionType, Type> ExpressionTypeToClassType;

        /// <summary>
        /// Initializes static members of the <see cref="ExpressionCreator"/> class.
        /// </summary>
        static ExpressionCreator() {
            ExpressionTypeToClassType = new Dictionary<ExpressionType, Type>();

            ExpressionTypeToClassType[ExpressionType.Add] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.AddChecked] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.And] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.AndAlso] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.ArrayLength] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.ArrayIndex] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Call] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Coalesce] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Conditional] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Constant] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Convert] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.ConvertChecked] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Divide] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Equal] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.ExclusiveOr] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.GreaterThan] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.GreaterThanOrEqual] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Invoke] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Lambda] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.LeftShift] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.LessThan] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.LessThanOrEqual] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.ListInit] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.MemberAccess] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.MemberInit] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Modulo] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Multiply] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.MultiplyChecked] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Negate] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.UnaryPlus] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.NegateChecked] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.New] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.NewArrayInit] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.NewArrayBounds] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Not] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.NotEqual] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Or] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.OrElse] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Parameter] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Power] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Quote] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.RightShift] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Subtract] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.SubtractChecked] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.TypeAs] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.TypeIs] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Assign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Block] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.DebugInfo] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Decrement] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Dynamic] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Default] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Extension] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Goto] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Increment] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Index] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Label] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.RuntimeVariables] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Loop] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Switch] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Throw] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Try] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.Unbox] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.AddAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.AndAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.DivideAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.ExclusiveOrAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.LeftShiftAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.ModuloAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.MultiplyAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.OrAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.PowerAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.RightShiftAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.SubtractAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.AddAssignChecked] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.MultiplyAssignChecked] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.SubtractAssignChecked] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.PreIncrementAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.PreDecrementAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.PostIncrementAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.PostDecrementAssign] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.TypeEqual] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.OnesComplement] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.IsTrue] = typeof(BinaryExpression);
            ExpressionTypeToClassType[ExpressionType.IsFalse] = typeof(BinaryExpression);
        }

        /// <summary>
        /// Gets or sets the new expression callback.
        /// </summary>
        /// <value>The new expression callback.</value>
        public Func<ExpressionCreationConditions, ExpressionCreationContext, Expression> NewExpressionCallback { get; set; }

        /// <summary>
        /// Gets or sets the new object creator. Gets called when constructed a Constant Expression.
        /// </summary>
        /// <value>The new object callback.</value>
        public IObjectCreator NewObjectCreator { get; set; }

        /// <summary>
        /// Creates expression that is of the specified expression type.
        /// </summary>
        /// <param name="requestedExpressionType">The expresion type to create.</param>
        /// <returns>Created expression type.</returns>
        public Expression Create(ExpressionType requestedExpressionType) {
            return this.Create(requestedExpressionType, new ExpressionCreationContext());
        }

        /// <summary>
        /// Creates expression that is of the specified requested return type.
        /// </summary>
        /// <param name="requestedExpressionType">Type of the requested expression.</param>
        /// <param name="requestedReturnType">Type of the requested return.</param>
        /// <returns>Created expression type.</returns>
        public Expression Create(ExpressionType requestedExpressionType, Type requestedReturnType) {
            var context = new ExpressionCreationContext();
            context.RequestedReturnType = requestedReturnType;
            return this.Create(requestedExpressionType, context);
        }

        /// <summary>
        /// Internal method that creates the expression that is of the specified expression type.
        /// </summary>
        /// <param name="expressionType">The expresion type to create.</param>
        /// <param name="conditions">The creation conditions.</param>
        /// <param name="creationContext">The context of the creation evaluation.</param>
        /// <returns>Created expression type.</returns>
        internal Expression CreateInternal(ExpressionType expressionType, ExpressionCreationConditions conditions, ExpressionCreationContext creationContext) {
            // If we have reached our max depth, then we can only create a depth that gaurentees 1 depth.
            if (conditions.MaxDepth == creationContext.CurrentDepth) {
                expressionType = ExpressionType.Constant;
            }

            /* Increment depth count. */
            creationContext.CurrentDepth++;

            try {
                switch (expressionType) {
                    case ExpressionType.Add:
                        return this.CreateExpression(Expression.Add, conditions, creationContext);
                        ////return Expression.Add(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.AddChecked:
                        return this.CreateExpression(Expression.AddChecked, conditions, creationContext);
                        ////return Expression.AddChecked(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.And:
                        return Expression.And(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.AndAlso:
                        return Expression.AndAlso(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.ArrayLength:
                        return Expression.ArrayLength(this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.ArrayIndex:
                        return Expression.ArrayIndex(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.Call:
                    ////    ////return Expression.Call(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.Coalesce:
                        return Expression.Coalesce(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Conditional:
                        return Expression.Condition(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Constant:
                        return this.CreateObjectExpression(conditions, creationContext);
                    ////case ExpressionType.Convert:
                    ////    ////return Expression.Convert(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.ConvertChecked:
                    ////    ////return Expression.ConvertChecked(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.Divide:
                        return Expression.Divide(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Equal:
                        return Expression.Equal(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.ExclusiveOr:
                        return Expression.ExclusiveOr(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.GreaterThan:
                        return Expression.GreaterThan(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.GreaterThanOrEqual:
                        return Expression.GreaterThanOrEqual(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Invoke:
                        return Expression.Invoke(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.Lambda:
                    ////    ////return Expression.Lambda(this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.LeftShift:
                        return Expression.LeftShift(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.LessThan:
                        return Expression.LessThan(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.LessThanOrEqual:
                        return Expression.LessThanOrEqual(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.ListInit:
                    ////    ////return Expression.ListInit(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.MemberAccess:
                    ////    ////return Expression.MakeMemberAccess(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.MemberInit:
                    ////    ////return Expression.MemberInit(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.Modulo:
                        return Expression.Modulo(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Multiply:
                        return Expression.Multiply(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.MultiplyChecked:
                        return Expression.MultiplyChecked(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Negate:
                        return Expression.Negate(this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.UnaryPlus:
                        return Expression.UnaryPlus(this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.NegateChecked:
                        return Expression.NegateChecked(this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.New:
                    ////    ////return Expression.New(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.NewArrayInit:
                    ////    ////return Expression.NewArrayInit(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.NewArrayBounds:
                    ////    ////return Expression.NewArrayBounds(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.Not:
                        return Expression.Not(this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.NotEqual:
                        return Expression.NotEqual(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Or:
                        return Expression.Or(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.OrElse:
                        return Expression.OrElse(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.Parameter:
                    ////    ////return Expression.Parameter(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.Power:
                        return Expression.Power(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Quote:
                        return Expression.Quote(this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.RightShift:
                        return Expression.RightShift(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Subtract:
                        return Expression.Subtract(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.SubtractChecked:
                        return Expression.SubtractChecked(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.TypeAs:
                    ////    ////return Expression.TypeAs(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.TypeIs:
                    ////    ////return Expression.TypeIs(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.Assign:
                        return Expression.Assign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Block:
                        return Expression.Block(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.DebugInfo:
                    ////    ////return Expression.DebugInfo(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.Decrement:
                        return Expression.Decrement(this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.Dynamic:
                    ////    ////return Expression.Dynamic(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.Default:
                    ////    ////return Expression.Default(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.Extension:
                    ////    ////return Expression(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.Goto:
                    ////    ////return Expression.Goto(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.Increment:
                        return Expression.Increment(this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.Index:
                    ////    ////return Expression.MakeIndex(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.Label:
                    ////    ////return Expression.Label(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.RuntimeVariables:
                    ////    ////return Expression.RuntimeVariables(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.Loop:
                        return Expression.Loop(this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Switch:
                        return Expression.Switch(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Throw:
                        return Expression.Throw(this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.Try:
                    ////    ////return Expression.MakeTry(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.Unbox:
                    ////    ////return Expression.Unbox(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    ////case ExpressionType.AddAssign:
                    ////    return Expression.AddAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.AndAssign:
                    ////    return Expression.AndAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.DivideAssign:
                    ////    return Expression.DivideAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.ExclusiveOrAssign:
                    ////    return Expression.ExclusiveOrAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.LeftShiftAssign:
                    ////    return Expression.LeftShiftAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.ModuloAssign:
                    ////    return Expression.ModuloAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.MultiplyAssign:
                    ////    return Expression.MultiplyAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.OrAssign:
                    ////    return Expression.OrAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.PowerAssign:
                    ////    return Expression.PowerAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.RightShiftAssign:
                    ////    return Expression.RightShiftAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.SubtractAssign:
                    ////    return Expression.SubtractAssign(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.AddAssignChecked:
                    ////    return Expression.AddAssignChecked(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.MultiplyAssignChecked:
                    ////    return Expression.MultiplyAssignChecked(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.SubtractAssignChecked:
                    ////    return Expression.SubtractAssignChecked(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.PreIncrementAssign:
                    ////    return Expression.PreIncrementAssign(this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.PreDecrementAssign:
                    ////    return Expression.PreDecrementAssign(this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.PostIncrementAssign:
                    ////    return Expression.PostIncrementAssign(this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.PostDecrementAssign:
                    ////    return Expression.PostDecrementAssign(this.NewExpressionCallback(conditions, creationContext));
                    ////case ExpressionType.TypeEqual:
                    ////    ////return Expression.TypeEqual(this.NewExpressionCallback(conditions, creationContext), this.NewExpressionCallback(conditions, creationContext));
                    ////    throw new NotImplementedException();
                    case ExpressionType.OnesComplement:
                        return Expression.OnesComplement(this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.IsTrue:
                        return Expression.IsTrue(this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.IsFalse:
                        return Expression.IsFalse(this.NewExpressionCallback(conditions, creationContext));
                    case ExpressionType.Call:
                    case ExpressionType.Convert:
                    case ExpressionType.ConvertChecked:
                    case ExpressionType.Lambda:
                    case ExpressionType.ListInit:
                    case ExpressionType.MemberAccess:
                    case ExpressionType.MemberInit:
                    case ExpressionType.New:
                    case ExpressionType.NewArrayInit:
                    case ExpressionType.NewArrayBounds:
                    case ExpressionType.Parameter:
                    case ExpressionType.TypeAs:
                    case ExpressionType.TypeIs:
                    case ExpressionType.DebugInfo:
                    case ExpressionType.Dynamic:
                    case ExpressionType.Default:
                    case ExpressionType.Extension:
                    case ExpressionType.Goto:
                    case ExpressionType.Index:
                    case ExpressionType.Label:
                    case ExpressionType.RuntimeVariables:
                    case ExpressionType.Try:
                    case ExpressionType.Unbox:
                    case ExpressionType.AddAssign:
                    case ExpressionType.AndAssign:
                    case ExpressionType.DivideAssign:
                    case ExpressionType.ExclusiveOrAssign:
                    case ExpressionType.LeftShiftAssign:
                    case ExpressionType.ModuloAssign:
                    case ExpressionType.MultiplyAssign:
                    case ExpressionType.OrAssign:
                    case ExpressionType.PowerAssign:
                    case ExpressionType.RightShiftAssign:
                    case ExpressionType.SubtractAssign:
                    case ExpressionType.AddAssignChecked:
                    case ExpressionType.MultiplyAssignChecked:
                    case ExpressionType.SubtractAssignChecked:
                    case ExpressionType.PreIncrementAssign:
                    case ExpressionType.PreDecrementAssign:
                    case ExpressionType.PostIncrementAssign:
                    case ExpressionType.PostDecrementAssign:
                    case ExpressionType.TypeEqual:
                    // Fine.
                    default:
                        throw new InvalidOperationException();
                }
            } catch (Exception e) {
                throw new ExpressionCreationException(string.Format(CultureInfo.InvariantCulture, "Expression of type {0} could not be created", expressionType), e);
            }
        }

        /// <summary>
        /// Creates expression that is the specified expression type.
        /// </summary>
        /// <param name="expressionType">The expresion type to create.</param>
        /// <param name="creationContext">The context of the creation evaluation.</param>
        /// <returns>Created expression type.</returns>
        private Expression Create(ExpressionType expressionType, ExpressionCreationContext creationContext) {
            return this.CreateInternal(expressionType, ExpressionCreationConditions.None, creationContext);
        }

        private Expression CreateObjectExpression(ExpressionCreationConditions conditions, ExpressionCreationContext creationContext) {
            /* Check that we can create the object (for performance reasons), and create the object. */
            if (this.NewObjectCreator.CanCreate(conditions, creationContext)) {
                return Expression.Constant(this.NewObjectCreator.CreateObject(conditions, creationContext));
            }

            /*
             * By default, return null if we can't create an object.
             * There is also not way to alert the caller that this call couldn't create the object.
             * Future: We could throw an exception, but the caller code could recover.
             */
            return Expression.Constant(null);
        }

        private Expression CreateExpression(Func<Expression, Expression, Expression> expressionMethod, ExpressionCreationConditions conditions, ExpressionCreationContext creationContext) {
            Expression arg1 = this.NewExpressionCallback(conditions, creationContext);
            creationContext.EvaluatedDataTypes.Add(arg1.Type);
            Expression arg2 = this.NewExpressionCallback(conditions, creationContext);
            return expressionMethod(arg1, arg2);
        }
    }
}
