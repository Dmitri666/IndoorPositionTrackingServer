// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookingRequest.cs" company="">
//   BookingRequest
// </copyright>
// <summary>
//   The booking request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Contracts.ViewModel.Booking
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The booking request.
    /// </summary>
    public class BookingRequest
    {
        #region Public Properties

        ///// <summary>
        ///// Gets or sets the table id.
        ///// </summary>
        //public Guid TableId { get; set; }

        /// <summary>
        /// Gets or sets the People Count
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the Tables.
        /// </summary>
        public List<Guid> Tables { get; set; }

        #endregion
    }
}