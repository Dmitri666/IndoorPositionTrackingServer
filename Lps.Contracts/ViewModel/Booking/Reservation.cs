namespace Lps.Contracts.ViewModel.Booking
{
    using System;

    public class Reservation
    {
        /// <summary>
        /// Gets or sets the table id.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Gets or sets the People Count
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }
    }
}
