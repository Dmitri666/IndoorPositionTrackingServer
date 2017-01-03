// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Booking.cs" company="">
//   
// </copyright>
// <summary>
//   The booking.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The booking.
    /// </summary>
    [Table("Booking")]
    public class Booking
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="KitchenMenuType"/> class.
        /// </summary>
        public Booking()
        {
            this.Tables = new HashSet<BookingRoomTable>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [Required]
        public int State { get; set; }
              
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        [Required]
        public DateTime Time { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>        
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the People Count
        /// </summary>
        [Required]
        public int PeopleCount { get; set; }

        /// <summary>
        ///     Gets or sets the Tables
        /// </summary>
        public virtual ICollection<BookingRoomTable> Tables { get; set; }

        #endregion
    }
}