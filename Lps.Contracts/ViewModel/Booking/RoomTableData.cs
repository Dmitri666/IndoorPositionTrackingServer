namespace Lps.Contracts.ViewModel.Booking
{
    using System;

    public class RoomTableData
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>      
        public Guid Id { get; set; }
        
        /// <summary>
        ///     Gets or sets the ClientName
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>      
        public string Type { get; set; }
    }
}
