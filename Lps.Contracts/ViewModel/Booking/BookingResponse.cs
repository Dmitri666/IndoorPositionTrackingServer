// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookingResponse.cs" company="">
//   
// </copyright>
// <summary>
//   The booking response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Contracts.ViewModel.Booking
{
    using System;

    /// <summary>
    /// The booking response.
    /// </summary>
    public class BookingResponse
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether avalable.
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        /// Gets or sets the table id.
        /// </summary>
        public Guid BookingId { get; set; }

        #endregion
    }
}