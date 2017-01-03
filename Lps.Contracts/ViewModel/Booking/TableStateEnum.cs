// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableState.cs" company="">
//   
// </copyright>
// <summary>
//   The table state.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Contracts.ViewModel.Booking
{
    using System;

    /// <summary>
    /// The booking state.
    /// </summary>
    public enum TableStateEnum
    {
        /// <summary>
        /// The free.
        /// </summary>
        Free = 0, 

        /// <summary>
        /// The booked.
        /// </summary>
        Booked = 1, 

        /// <summary>
        /// The booked for me.
        /// </summary>
        BookedForMe = 2, 

        /// <summary>
        /// The rejected.
        /// </summary>
        Rejected = 3, 

        /// <summary>
        /// The waiting.
        /// </summary>
        Waiting = 4
    }

   
    
}