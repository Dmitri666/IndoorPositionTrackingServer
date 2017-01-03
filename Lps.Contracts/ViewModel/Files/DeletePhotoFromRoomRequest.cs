namespace Lps.Contracts.ViewModel.Files
{
    using System;

    public class DeletePhotoFromRoomRequest : DeletePhotoBase
    {
        /// <summary>
        /// Gets or sets the RoomId
        /// </summary>
        public Guid RoomId { get; set; }        
    }
}
