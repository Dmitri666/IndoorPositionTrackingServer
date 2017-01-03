namespace LpsServer.Data.Entities
{
    using System;

    public class BookingRoomTable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingRoomTable"/> class.
        /// </summary>
        public BookingRoomTable()
        {         
        }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Booking.
        /// </summary>
        public virtual Booking Booking { get; set; }

        /// <summary>
        /// Gets or sets the KitchenMenuType.
        /// </summary>
        public virtual RoomTable RoomTable { get; set; }
    }
}
