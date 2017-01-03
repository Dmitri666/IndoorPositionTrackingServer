// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BookingController.cs" company="">
//   
// </copyright>
// <summary>
//   The booking controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Net;
using System.Web.Http;

namespace LpsServer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Lps.Contracts.ViewModel.Booking;
    using Lps.Services;
    using Lps.Services.Booking;

    using LpsServer.Data;
    using LpsServer.PushServices;

    using Microsoft.AspNet.Identity;

    /// <summary>
    /// The booking controller.
    /// </summary>
    [RoutePrefix("api/Booking")]
    public class BookingController : BaseApiController
    {
        #region Public Methods and Operators

        [HttpPost]
        [Route("GetReservation")]
        [AllowAnonymous]
        public HttpResponseMessage GetReservation(Reservation reservation)
        {
            using (var repo = this.Repository)
            {
                var reservationData = repo.GetReservation(reservation);

                return this.Request.CreateResponse(HttpStatusCode.OK, new { data = reservationData });
            }
        }

        [HttpPost]
        [Route("GetBookinMap")]
        [AllowAnonymous]
        public HttpResponseMessage GetBookinMap(Reservation reservation)
        {
            using (var repo = this.Repository)
            {
                var bookinMapData = repo.GetBookinMap(reservation);

                return this.Request.CreateResponse(HttpStatusCode.OK, bookinMapData);
            }
        }

        [HttpPost]
        [Authorize]
        //[AllowAnonymous]
        [Route("TableReservationModel")]
        public HttpResponseMessage Post([FromBody] TableReservationModelRequest param)
        {
            using (var context = new LpsContext())
            {
                var from = param.Time.AddHours(-2);
                var to = param.Time.AddHours(2);

                try
                {
                    var model = new Dictionary<Guid, TableStateEnum>();
                    var firstOrDefault = context.Rooms.FirstOrDefault(x => x.Id == param.RoomId);
                    if (firstOrDefault != null)
                    {
                        var tables = firstOrDefault.Tables.Select(x => x.Id).ToList();

                        foreach (var tableId in tables)
                        {
                            var bookings =
                                context.Bookings.Where(x => x.Tables.Any(m => m.RoomTable.Id == tableId))
                                    .Where(x => x.Time <= to && x.Time > @from)
                                    .ToList();

                            TableStateEnum state = TableStateEnum.Free;
                            if (bookings.Any())
                            {
                                var userId = new Guid(this.User.Identity.GetUserId());
                                // not anonymous
                                if (bookings.Last().User.Id == userId)
                                {
                                    if (bookings.Last().State == (int)BookingStateEnum.Accepted)
                                    {
                                        state = TableStateEnum.BookedForMe;
                                    }
                                    else if (bookings.Last().State == (int)BookingStateEnum.Rejected)
                                    {
                                        //state.State = (int)TableStateEnum.Free;
                                        state = TableStateEnum.Booked;
                                    }
                                    else if (bookings.Last().State == (int)BookingStateEnum.Canceled)
                                    {
                                        state = TableStateEnum.Free;
                                    }
                                    else
                                    {
                                        state = TableStateEnum.Waiting;
                                    }

                                }
                                else
                                {
                                    if (bookings.Last().State == (int)BookingStateEnum.Canceled)
                                    {
                                        state = TableStateEnum.Free;
                                    }
                                    else if (bookings.Last().State == (int)BookingStateEnum.Rejected)
                                    {
                                        state = TableStateEnum.Free;
                                    }
                                    else
                                    {
                                        state = TableStateEnum.Booked;
                                    }
                                }
                            }
                            model.Add(tableId,state);
                        }
                    }

                    return this.Request.CreateResponse(HttpStatusCode.OK, model.Select(x => new { TableId = x.Key, State = (int)x.Value }));

                }
                catch (Exception e)
                {
                    Logger.Log(e);
                }
                
            }
            return this.Request.CreateResponse(HttpStatusCode.OK);


        }

        /// <summary>
        /// The get booking history by user.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]        
        [Route("GetBookingHistoryByUser/{roomId:guid?}")]
        [Authorize]
        public HttpResponseMessage GetBookingHistoryByUser(Guid? roomId = null)
        {
            using (var repo = this.Repository)
            {
                var bookingHistory = repo.GetBookingHistoryByUser(roomId);
                return this.Request.CreateResponse(HttpStatusCode.OK, bookingHistory);
            }
        }

        /// <summary>
        /// The get booking history by user.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        [Route("GetBookingHistoryForKassaByRoom/{roomId:guid}")]
        [Authorize]
        public HttpResponseMessage GetBookingHistoryForKassaByRoom(Guid roomId)
        {
            using (var repo = this.Repository)
            {
                var bookingHistory = repo.GetBookingHistoryForKassaByRoom(roomId);

                return this.Request.CreateResponse(HttpStatusCode.OK, bookingHistory);
            }
        }

        // POST: api/Booking
        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        [HttpPost]
        [Authorize]
        [Route("Request")]
        public HttpResponseMessage Post([FromBody] BookingRequest value)
        {
            using (var repo = this.Repository)
            {
                BookingJoinRoomData bookingJoinRoomData = repo.CreateBooking(value);

                LpsHubWrapper.GetInstance().NotifyNewBooking(bookingJoinRoomData);

                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        // POST: api/Booking/Response
        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        [HttpPost]
        [Route("Response")]
        [Authorize]
        public HttpResponseMessage Post([FromBody] BookingResponse value)
        {
            using (var repo = this.Repository)
            {
                var model = repo.UpdateBooking(value);
                LpsHubWrapper.GetInstance().NotifyBookingStateChanged(model);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
        }


        
        #endregion
    }
}