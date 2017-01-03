namespace Lps.Contracts.ViewModel.Rooms
{
    using System;

    public class KitchenTypeData
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the Order
        /// </summary>
        public int Order { get; set; }
    }
}
