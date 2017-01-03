namespace Lps.Contracts.ViewModel.Booking
{    
    using System.Collections.Generic;

    public class RoomTableJoinKassaData : RoomTableData
    {
        public List<TimeStampJoinKassa> TimeStampJoinKassaList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomTableJoinKassaData"/> class.
        /// </summary>
        public RoomTableJoinKassaData()
        {
            this.TimeStampJoinKassaList = new List<TimeStampJoinKassa>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoomTableJoinKassaData"/> class.
        /// </summary>
        public RoomTableJoinKassaData(RoomTableData roomTableData)
        {
            this.Id = roomTableData.Id;
            this.Description = roomTableData.Description;
            this.Type = roomTableData.Type;
            this.TimeStampJoinKassaList = new List<TimeStampJoinKassa>();
        }
    }
}
