// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookingJoinRoomData.cs" company="">
//   
// </copyright>
// <summary>
//   The booking join room data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Contracts.ViewModel.Booking
{
    using System;

    /// <summary>
    /// The booking join room data.
    /// </summary>
    public class BookingJoinRoomData : BookingData
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the room id.
        /// </summary>
        public Guid RoomId { get; set; }

        /// <summary>
        ///  Gets or sets the Main Photo.
        /// </summary>
        public string MainPhoto { get; set; }

        #endregion
    }
}