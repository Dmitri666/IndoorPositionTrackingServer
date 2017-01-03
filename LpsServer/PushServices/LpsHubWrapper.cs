// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LpsHubWrapper.cs" company="">
//   
// </copyright>
// <summary>
//   The position change manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LpsServer.PushServices
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;

    using Lps.Contracts.ViewModel;
    using Lps.Contracts.ViewModel.Booking;
    using Lps.Contracts.ViewModel.Chat;
    using Lps.Services;

    using Microsoft.AspNet.SignalR;

    /// <summary>
    /// The position change manager.
    /// </summary>
    public class LpsHubWrapper
    {
        // Singleton instance
        #region Static Fields

        /// <summary>
        /// The _instance.
        /// </summary>
        private static LpsHubWrapper _instance;

        #endregion

        #region Fields

        /// <summary>
        /// The _context.
        /// </summary>
        private readonly IHubContext _context;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LpsHubWrapper"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        private LpsHubWrapper(IHubContext context)
        {
            this._context = context;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static LpsHubWrapper GetInstance()
        {

            if (_instance == null)
            {
                _instance = new LpsHubWrapper(GlobalHost.ConnectionManager.GetHubContext<LpsHub>());
            }

            return _instance;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The on booking state changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="state">
        /// The state.
        /// </param>
        public void NotifyBookingStateChanged(BookingJoinRoomData booking)
        {
            try
            {
                this._context.Clients.Group(booking.UserName).bookingStateChanged(booking);

                var groupName = "ReservationModel:" + booking.RoomId;
                this._context.Clients.Group(groupName).changeTableReservationModel();
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        public void NotifyActorJoinChat(Guid localeId,Actor actor)
        {
            try
            {
                var groupName = "PositionConsumer:" + localeId;
                this._context.Clients.Group(groupName).joinChat(actor);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        public void NotifyActorLeaveChat(Guid localeId, Actor actor)
        {
            try
            {
                var groupName = "PositionConsumer:" + localeId;
                this._context.Clients.Group(groupName).leaveChat(actor);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        /// <summary>
        /// The on new booking.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="roomAndBooking">
        /// The room and booking.
        /// </param>
        public void NotifyNewBooking(BookingJoinRoomData bookingJoinRoomData)
        {
            try
            {
                string groupName = "Kassa:" + bookingJoinRoomData.RoomId;

                this._context.Clients.Group(groupName).NewBooking(bookingJoinRoomData);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }


        public void SendConversationMessage(string resipientName,ChatMessage message)
        {
            try
            {
                this._context.Clients.Group(resipientName).newchatmessage(message);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        /// <summary>
        /// The on position changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="actorPositions">
        /// The actor positions.
        /// </param>
        public void NotifyPositionChanged(Guid roomId, IList<DevicePosition> positions)
        {
            try
            {
                foreach (var position in positions)
                {
                    string groupName = "PositionConsumer:" + roomId;
                    this._context.Clients.Group(groupName).devicePositionChanged(position);
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        #endregion
    }
}