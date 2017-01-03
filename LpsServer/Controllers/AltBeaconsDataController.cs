// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AltBeaconsDataController.cs" company="">
//   
// </copyright>
// <summary>
//   The alt beacons data controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Lps.Contracts.ViewModel;
    using Lps.Contracts.ViewModel.Beacons;
    using Lps.Services;

    using LpsServer.Data.Entities;

    /// <summary>
    /// The alt beacons data controller.
    /// </summary>

    [RoutePrefix("api/AltBeaconsData")]
    public class AltBeaconsDataController : BaseApiController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        public HttpResponseMessage Get()
        {
            using (var ctx = new LpsServer.Data.LpsContext())
            {
                var regions = ctx.Rooms.Where(x => x.BeaconList.Any()).Select(x => new { RoomId = x.Id , Identifirer1 = x.BeaconList.FirstOrDefault().Identifier1, Identifirer2 = x.BeaconList.FirstOrDefault().Identifier2 }).ToList();
                
                return this.Request.CreateResponse(HttpStatusCode.OK, regions);
            }
        }

        [HttpGet]
        [Route("GetBackgroundBeacons")]
        public HttpResponseMessage GetBackgroundBeacons()
        {
            using (var ctx = new LpsServer.Data.LpsContext())
            {
                var beacons = ctx.BeaconInRange.ToList();
                return this.Request.CreateResponse(HttpStatusCode.OK, beacons);
            }
        }

        
        [HttpPost]
        [Route("SaveBackgroundBeacons")]
        public HttpResponseMessage Post([FromBody] List<BeaconData> param)
        {
            using (var ctx = new LpsServer.Data.LpsContext())
            {
                foreach (var beacon in param)
                {
                    var beaconInRoom =
                        ctx.Beacons.FirstOrDefault(
                            x =>
                            x.Identifier1 == beacon.Id1 && x.Identifier2 == beacon.Id2 && x.Identifier3 == beacon.Id3);

                    if (beaconInRoom != null)
                    {
                        continue;
                    }

                    var backgroundBeacon =
                        ctx.BeaconInRange.FirstOrDefault(
                            x =>
                            x.Identifier1 == beacon.Id1 && x.Identifier2 == beacon.Id2 && x.Identifier3 == beacon.Id3);

                    if (backgroundBeacon == null)
                    {
                        var bgBeacon = new BeaconInRange() { Identifier1 = beacon.Id1 ,Identifier2 = beacon.Id2 , Identifier3 = beacon.Id3 } ;
                        ctx.BeaconInRange.Add(bgBeacon);
                    }


                }

                ctx.SaveChanges();
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPost]
        [Route("SavePositionLog")]
        public HttpResponseMessage SavePositionLog([FromBody] PositionLogData param)
        {
            using (var ctx = new LpsServer.Data.LpsContext())
            {
                var keys = new List<int> { param.Key1, param.Key2, param.Key3 };
                keys.Sort();


                var entity = ctx.PositionLogs.Create();
                entity.RoomId = param.RoomId;
                entity.DeviceId = param.DeviceId;
                entity.Key1 = keys[0];
                entity.Key2 = keys[1];
                entity.Key3 = keys[2];
                entity.X = param.X;
                entity.Y = param.Y;
                entity.Time = DateTime.Now;

                ctx.PositionLogs.Add(entity);

                ctx.SaveChanges();
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
        }
        #endregion
    }
}