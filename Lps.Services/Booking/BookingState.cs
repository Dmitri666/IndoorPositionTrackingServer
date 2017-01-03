// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookingState.cs" company="">
//   
// </copyright>
// <summary>
//   The booking state enum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Services.Booking
{
    /// <summary>
    /// The booking state enum.
    /// </summary>
    public enum BookingStateEnum
    {
        /// <summary>
        /// The waiting.
        /// </summary>
        Waiting = 0, 

        /// <summary>
        /// The accepted.
        /// </summary>
        Accepted = 1, 

        /// <summary>
        /// The rejected.
        /// </summary>
        Rejected = 2, 

        /// <summary>
        /// The canceled.
        /// </summary>
        Canceled = 3,

        /// <summary>
        /// The BarOwner -Accepted.
        /// </summary>
        BarOwnerAccepted = 4
    }
}