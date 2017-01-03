namespace LpsServer.Data.Entities
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RoomTable")]
    public class RoomTable
    {
        public RoomTable()
        {
            //this.Bookings = new HashSet<Booking>();
            this.Tables = new HashSet<BookingRoomTable>();
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        ///// <summary>
        ///// Bookings
        ///// </summary>
        //public virtual ICollection<Booking> Bookings { get; set; }

        /// <summary>
        ///     Gets or sets the Tables
        /// </summary>
        public virtual ICollection<BookingRoomTable> Tables { get; set; }

        /// <summary>
        ///     Gets or sets the coordinates 360.
        /// </summary>
        [Required]
        public double Angle { get; set; }       

        /// <summary>
        ///     Gets or sets the x.
        /// </summary>
        [Required]
        public double X { get; set; }

        /// <summary>
        ///     Gets or sets the y.
        /// </summary>
        [Required]
        public double Y { get; set; }

        [Required]
        public double Width { get; set; }

        [Required]
        public double Height { get; set; }

        /// <summary>
        ///     Gets or sets the ClientName
        /// </summary>
        public string Description { get; set; }

        // Reference
        /// <summary>
        ///     Gets or sets the room.
        /// </summary>
        public virtual Room Room { get; set; }

        /// <summary>
        /// Gets or sets the heigth.
        /// </summary>
        [Required]
        public string Type { get; set; }
    }
}
