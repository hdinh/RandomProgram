namespace Dinh.RandomProgram.Tests
{
    using System;

    /// <summary>
    /// Convienance class that mocks IObjectCreator and allows for simple object creation.
    /// </summary>
    internal sealed class MockTestObjectCreator : IObjectCreator
    {
        private readonly Func<object> creationCallback;
        private readonly Func<ExpressionCreationContext, object> creationWithContextCallback;

        public MockTestObjectCreator(Func<object> creationCallback) {
            this.creationCallback = creationCallback;
        }

        public MockTestObjectCreator(Func<ExpressionCreationContext, object> creationWithContextCallback) {
            this.creationWithContextCallback = creationWithContextCallback;
        }

        public ITypeRepository TypeRepository { get; set; }

        public bool CanCreate(ExpressionCreationConditions conditions, ExpressionCreationContext context) {
            return true;
        }

        public object CreateObject(ExpressionCreationConditions conditions, ExpressionCreationContext context) {
            if (this.creationWithContextCallback != null) {
                return this.creationWithContextCallback(context);
            } else {
                return this.creationCallback();
            }
        }
    }
}
