// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookingData.cs" company="">
//   BookingData
// </copyright>
// <summary>
//   BookingData
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lps.Contracts.ViewModel.Booking
{
    using System;

    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// BookingData
    /// </summary>
    public class BookingData
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the BookingId
        /// </summary>
        public Guid BookingId { get; set; }

        /// <summary>
        /// Gets or sets the People Count
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>        
        public int State { get; set; }
        
        ///// <summary>
        ///// Gets or sets the Tables.
        ///// </summary>
        //public List<Guid> TableIdList { get; set; }

        /// <summary>
        /// Gets or sets the RoomTableData.
        /// </summary>
        public List<RoomTableData> RoomTableDataList { get; set; }

        /// <summary>
        /// Gets or sets the Time
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        //[JsonIgnore]
        public string UserName { get; set; }      

        #endregion
    }
}