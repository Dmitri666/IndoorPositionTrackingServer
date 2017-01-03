namespace Lps.Contracts.ViewModel.Booking
{
    using System.Collections.Generic;

    public class TimeStampJoinKassa: Timestamp
    {
        /// <summary>
        /// Gets or sets the SubColumns.
        /// </summary>
        public IList<Timestamp> SubColumns { get; set; }

        /// <summary>
        /// Gets or sets the BookingData.
        /// </summary>
        public BookingData BookingData { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeStampJoinKassa"/> class.
        /// </summary>
        public TimeStampJoinKassa()
        {
            this.SubColumns = new List<Timestamp>();         
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeStampJoinKassa"/> class.
        /// </summary>
        public TimeStampJoinKassa(Timestamp timestamp)
        {
            this.date = timestamp.date;
            this.timestamp = timestamp.timestamp;
            this.available = timestamp.available;
            this.dateTime = timestamp.dateTime;
            this.BookingData = new BookingData();

            this.SubColumns = new List<Timestamp>();
        }
    }
}
