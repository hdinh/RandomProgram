namespace Dinh.RandomProgram
{
    using System;
    using System.ComponentModel;

    public sealed class WeightedTypeCriteria<T>
    {
        public WeightedTypeCriteria() {
            this.Weight = 1.0;
        }

        /// <summary>
        /// Gets or sets the condition whether to include the method of this type.
        /// </summary>
        /// <value>The match.</value>
        public Func<T, bool> Match { get; set; }

        /// <summary>
        /// Gets or sets the normalized weight which the type will be chosen.
        /// </summary>
        /// <value>The weight.</value>
        public double Weight { get; set; }
    }
}
