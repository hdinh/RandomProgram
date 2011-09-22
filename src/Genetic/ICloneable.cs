namespace Dinh.RandomProgram
{
    using System;

    public interface ICloneable<T> : ICloneable
        where T : ICloneable<T>
    {
        /// <summary>
        /// Clones the object.
        /// </summary>
        /// <returns>The cloned object.</returns>
        new T Clone();
    }
}
