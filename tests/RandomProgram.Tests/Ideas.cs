////namespace Dinh.RandomProgram.Tests
////{
////    using System;
////    using System.Collections.Generic;
////    using System.Linq.Expressions;

////    public class Ideas
////    {
////        internal void CrossOver() {
////            throw new NotImplementedException();
////        }

////        internal void Mutate() {
////            throw new NotImplementedException();
////        }

////        internal void Simulate() {
////            throw new NotImplementedException();
////        }

////        public class RandomExpressionFactoryAA
////        {
////            public RandomExpressionContext Context { get; set; }

////            internal void Add(Type type, double p = 1) {
////                throw new NotImplementedException();
////            }

////            public Expression NewExpression() {
////                throw new NotImplementedException();
////            }
////        }

////        public class RandomExpressionContext
////        {
////            public Dictionary<ExpressionType, double> ExpressionDistribution { get; set; }
////            public IBranchDistribution BranchDistribution { get; set; }
////        }

////        public interface IBranchDistribution
////        {
////            BranchDistributionResult GetDistribution(Expression expression);
////        }

////        public class NomalBranchDistribution : IBranchDistribution
////        {
////            public BranchDistributionResult GetDistribution(Expression expression) {
////                return new BranchDistributionResult();
////            }
////        }

////        public class BranchDistributionResult
////        {
////            public double Left { get; set; }
////            public double Right { get; set; }
////        }

////        /// <summary>
////        /// First Test.
////        /// </summary>
////        public static void BrainStorming() {
////            var context = new RandomExpressionContext();
////            context.ExpressionDistribution = new Dictionary<ExpressionType, double>();
////            context.ExpressionDistribution[ExpressionType.Add] = 3.0;
////            context.ExpressionDistribution[ExpressionType.Subtract] = 10.0;

////            IBranchDistribution noramlDistribution = new NomalBranchDistribution();
////            context.BranchDistribution = noramlDistribution;

////            var factory = new RandomExpressionFactoryAA();
////            factory.Context = context;
////            factory.Add(typeof(Random));
////            factory.Add(typeof(Random), 2.0);

////            var newExpression = factory.NewExpression();

////            var population = new Ideas();
////            population.CrossOver();
////            population.Mutate();
////            population.Simulate();
////            //var x = Expression.Add(

////            //Expression<Func<int, bool>> jbj;
////            ////ExpressionType.Unbox
////            //// Arrange
////            //var creator = new ExpressionCreator();
////            //creator.Create(

////            //// Act
////            //ExpressionHelper.CreateExpression();

////            //// Assert
////            //Console.WriteLine("What up bitch");
////        }

////        public void A() {
////            var a = new ExpressionCreator();
////            Func<int> whatup = () => {
////                Console.WriteLine("Abc");
////                return 1;
////            };
////            //whatup.Method
////            Expression e = Expression.Call(whatup.Method);

////            Expression<Func<int>> i = Expression.Lambda<Func<int>>(e);
////            var answer = i.Compile()();

////            var creator = new ExpressionCreator {
////                NewObjectCallback = (notUsed) => {
////                    return 1;
////                }
////            };
////            creator.AddMethod(action.Method, 1);
////            Expression expression = creator.Create(ExpressionType.Call);

////            Action<int> action = (notUsed) => {
////            };

////            Delegate myDelegate;

////            ProgramGenerator lc = new ProgramGenerator();
////            DateTime mutable = new DateTime();
////            lc.AddType(typeof(Random));
////            lc.AddMethod(myDelegate.Method);
////            lc.AddInstance(mutable);

////            Func<int> created = lc.CreateLambda<Func<int>>();
////        }
////    }
////}
