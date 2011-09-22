namespace Dinh.RandomProgram
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Reflection;

    /// <summary>
    /// Constructs random objects using the types that are registered.
    /// </summary>
    public class DefaultObjectCreator : IObjectCreator
    {
        public ITypeRepository TypeRepository { get; set; }

        /// <summary>
        /// Determines whether this instance can create an object of the specified type.
        /// </summary>
        /// <param name="conditions">The conditions of expression evaluation when creating expression.</param>
        /// <param name="context">The context of expression evaluation when creating expression.</param>
        /// <returns>
        /// <c>true</c> if this instance an object of the specified type; otherwise, <c>false</c>.
        /// </returns>
        public bool CanCreate(ExpressionCreationConditions conditions, ExpressionCreationContext context) {
            return this.CanCreateInternal(context.RequestedReturnType);
        }

        /// <summary>
        /// Creates the object of the specified type.
        /// </summary>
        /// <param name="conditions">The conditions of expression evaluation when creating expression.</param>
        /// <param name="context">The context of expression evaluation when creating expression.</param>
        /// <returns>Object of type.</returns>
        public object CreateObject(ExpressionCreationConditions conditions, ExpressionCreationContext context) {
            if (!this.TypeRepository.HasType(context.RequestedReturnType)) {
                throw new ObjectCreationException();
            }

            foreach (MethodInfo method in this.TypeRepository.GetMethods(context.RequestedReturnType)) {
                ParameterInfo[] parameters = method.GetParameters();
                Func<object> callableAction = null;

                if (parameters.Length == 0) {
                    callableAction = () => {
                        return method.Invoke(null, null);
                    };
                } else if (conditions.MaxDepth > context.CurrentDepth) {
                    bool methodCreatable = true;

                    foreach (ParameterInfo parameter in parameters) {
                        if (!this.CanCreateInternal(parameter.ParameterType)) {
                            methodCreatable = false;
                            break;
                        }
                    }

                    if (methodCreatable) {
                        callableAction = () => {
                            object[] parameterObjects = new object[parameters.Length];
                            for (int i = 0; i < parameters.Length; i++) {
                                var evaluationContext = context.Clone();
                                evaluationContext.CurrentDepth++;
                                evaluationContext.RequestedReturnType = parameters[i].ParameterType;
                                parameterObjects[i] = this.CreateObject(conditions, evaluationContext);
                            }

                            return method.Invoke(null, parameterObjects);
                        };
                    }
                }

                if (callableAction != null) {
                    try {
                        /* Try to call method and return the result. */
                        object result = callableAction();
                        return result;
                    } catch (Exception ex) {
                        if (ex is TargetInvocationException || ex is ObjectCreationException) {
                            /* Add to our list of exceptions and continue looking for next. */
                            context.EvaluationExceptions.Add(ex);
                            continue;
                        } else {
                            // TODO
                            throw;
                        }
                    }
                }
            }

            throw new ObjectCreationException();
        }

        private bool CanCreateInternal(Type type) {
            if (this.TypeRepository.HasType(type)) {
                return true;
            }

            return false;
        }
    }
}
