// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Beacon.cs" company="">
//   
// </copyright>
// <summary>
//   The beacon.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LpsServer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///     The beacon.
    /// </summary>
    [Table("PositionLog")]
    public class PositionLog
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public string DeviceId { get; set; }

        /// <summary>
        ///     Gets or sets the identifier 1.
        /// </summary>
        public int Key1 { get; set; }

        /// <summary>
        ///     Gets or sets the identifier 2.
        /// </summary>
        public int Key2 { get; set; }

        public int Key3 { get; set; }

        /// <summary>
        ///     Gets or sets the identifier 3.
        /// </summary>
       

        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        
        public double X { get; set; }

        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        
        public double Y { get; set; }
        
        public DateTime Time { get; set; }
        #endregion
    }
}