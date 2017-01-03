// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookingStateRequest.cs" company="">
//   
// </copyright>
// <summary>
//   The booking state request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lps.Contracts.ViewModel.Booking
{
    using System;

    /// <summary>
    /// The booking state request.
    /// </summary>
    public class TableReservationModelRequest
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the room id.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }

        #endregion
    }
}