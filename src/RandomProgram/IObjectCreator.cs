namespace Dinh.RandomProgram
{
    using System;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Represents a class that can create objects from types.
    /// </summary>
    public interface IObjectCreator
    {
        ITypeRepository TypeRepository { get; set; }

        /// <summary>
        /// Determines whether this instance can create an object of the specified type.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        /// <c>true</c> if this instance an object of the specified type; otherwise, <c>false</c>.
        /// </returns>
        bool CanCreate(ExpressionCreationConditions conditions, ExpressionCreationContext context);

        /// <summary>
        /// Creates the object of the specified type.
        /// </summary>
        /// <param name="conditions">The conditions.</param>
        /// <param name="context">The context of expression evaluation when creating expression.</param>
        /// <returns>Object of type.</returns>
        object CreateObject(ExpressionCreationConditions conditions, ExpressionCreationContext context);
    }
}
