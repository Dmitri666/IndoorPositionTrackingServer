// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RoomsController.cs" company="">
//   
// </copyright>
// <summary>
//   The rooms controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LpsServer.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Web.Http;

    using Lps.Contracts.ViewModel;
    using Lps.Contracts.ViewModel.Rooms;
    using Lps.Services;
    using Lps.Services.Helper;

    using LpsServer.Data;

    /// <summary>
    ///     The rooms controller.
    /// </summary>
    [RoutePrefix("api/RoomMobile")]
    public class RoomMobileController : BaseApiController
    {
        // GET: api/Rooms
        #region Public Methods and Operators

        /// <summary>
        ///     The get.
        /// </summary>
        /// <returns>
        ///     The <see cref="HttpResponseMessage" />.
        /// </returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var rooms = this.Repository.GetRoomInfos();
            return this.Request.CreateResponse(HttpStatusCode.OK, rooms);
        }

        [HttpGet]
        [Route("cities")]
        public HttpResponseMessage GetCities()
        {
            var cities = this.Repository.GetCities();
            return this.Request.CreateResponse(HttpStatusCode.OK, cities);
        }

        [HttpGet]
        [Route("names")]
        public HttpResponseMessage GetNames()
        {
            var names = this.Repository.GetLocaleNames();
            return this.Request.CreateResponse(HttpStatusCode.OK, names);
        }

        [HttpGet]
        [Route("types")]
        public HttpResponseMessage GetTypes()
        {
            var types = this.Repository.GetKitchenTypes().Select(x => new {Text = x.Name , Value = x.Id });
            return this.Request.CreateResponse(HttpStatusCode.OK, types);
        }

        [HttpGet]
        [Route("specializings/{id:guid}")]
        public HttpResponseMessage GetSpecializing(Guid typeId)
        {
            var types = this.Repository.GetKitchenMenuTypes().Select(x => new  { Text = x.Name, Value = x.Id });
            return this.Request.CreateResponse(HttpStatusCode.OK, types);
        }

        /// <summary>
        /// The get rooms.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpPost]
        [Authorize]
        public HttpResponseMessage GetRooms(RequestLocationData request)
        {
            var location = Help.CreatePoint(request.Latitude, request.Longitude);
            double radius = Help.ConvertMilesToMeters(request.Radius);

            using (var ctx = new LpsContext())
            {
                var currentUser = ctx.Users.FirstOrDefault(x => x.Name == ClaimsPrincipal.Current.Identity.Name);
                var result = ctx.Rooms.Where(
                        m =>
                        request.KitchenTypes.Count == 0
                        || request.KitchenTypes.All(
                            i => m.RoomKitchenList.Select(k => k.KitchenType.Id).Any(k => k == i)))
                        .Where(m => m.Location.Distance(location) <= radius)
                        .Select(
                            x =>
                            new {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Lat = x.Latitude,
                                    Lng = x.Longitude,
                                    ImageFileName = x.PhotoList.FirstOrDefault() != null ? x.PhotoList.FirstOrDefault().Image : null,
                                    IsChatExist = x.IsChatExist,
                                    City = x.City,
                                    IsFavorite = x.FavoritsList.Any(y => y.User.Id == currentUser.Id),
                                    Rating = x.RatingList.Average(y => (double?)y.State) ?? 0.0
                                })
                        .ToList();

                
                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        [HttpGet]
        [Route("GetTableModel/{id}")]
        [Authorize]
        public IHttpActionResult GetTableModel(Guid id)
        {
            try
            {
                using (var ctx = new LpsContext())
                {
                    var room = ctx.Rooms.FirstOrDefault(m => m.Id == id);
                    var model =
                        new
                            {
                                height = room.RoomHeight,
                                wight = room.RoomWidth,
                                backgroungImage = room.BackgroungImage,
                                tables = room.Tables.Select(x => new { id = x.Id, angle = x.Angle , x = x.X, y = x.Y, type = x.Type , wight  = x.Width, height = x.Height , description = x.Description })
                            };



                    return this.Ok(model);
                }

            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }

        }

        #endregion

        // DELETE: api/MapData/5
    }
}