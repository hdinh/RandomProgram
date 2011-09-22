namespace Dinh.RandomProgram
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Represents the random creation of expressions.
    /// </summary>
    public static class ProgramGenerator
    {
        ////public DynamicProgram CreateProgram<TReturn, TParameter>() {
        ////    RandomExpressionBuilder expressionBuilder = new RandomExpressionBuilder();
        ////    Expression expression = expressionBuilder.NewExpression();

        ////    LambdaExpression lambda = Expression.Lambda<Func<TReturn, TParameter>>(expression);
        ////    DynamicProgram program = new DynamicProgram(lambda);
        ////    return program;
        ////    ////program.LinqExpression = Expression.Lambda<programDelegate>(expression);
        ////}

        public static DynamicProgram CreateProgram(Type delegateType) {
            MethodInfo info = delegateType.GetMethod("Invoke");
            ////ParameterInfo[] parameters = info.GetParameters();

            RandomExpressionBuilder expressionBuilder = new RandomExpressionBuilder();
            expressionBuilder.CreationConditions = new ExpressionCreationConditions(3, info.ReturnType);
            Expression expression = expressionBuilder.NewExpression();

            LambdaExpression lambda = Expression.Lambda(delegateType, expression);
            DynamicProgram program = new DynamicProgram(lambda);
            return program;
            ////program.LinqExpression = Expression.Lambda<programDelegate>(expression);
        }
    }
}
