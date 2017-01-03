using Nest;

namespace LpsServer.Controllers
{
    using System;
    using System.Web.Http;

    using Lps.Contracts.MapEditorViewModel;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;

    using Lps.Contracts.Helper;
    using Lps.Contracts.ViewModel;
    using Lps.Contracts.ViewModel.Beacons;
    using Lps.Contracts.ViewModel.Kitchen;
    using Lps.Contracts.ViewModel.Rating;
    using Lps.Contracts.ViewModel.RoomPlan;
    using Lps.Contracts.ViewModel.Rooms;
    using Lps.Services;
    using Lps.Services.Helper;

    using LpsServer.Data;
    using LpsServer.Data.Entities;

    using Newtonsoft.Json;

    [RoutePrefix("api/room")]
    public class RoomController : BaseApiController
    {
        [HttpGet]
        [Route("GetTypeaheadLocation")]
        [AllowAnonymous]        
        public IHttpActionResult GetTypeaheadLocation(string query)
        {
            IList<TypeaheadLocationData> typeaheadDataList = Repository.GetTypeaheadLocation(query);
            var local = new Uri("http://localhost:9200");

            var settings = new ConnectionSettings(local);

            Func<BoolQueryDescriptor<RoomData>,IBoolQuery> f = (b) => b.Should(bs => bs.Term(p => p.Name, ""));
            var elastic = new ElasticClient(settings);
            elastic.Search<RoomData>(r => r.Query(q => q.Bool(f)));

            return this.Ok(typeaheadDataList);           
        }

        [HttpGet]
        [Route("GetTypeaheadCity")]
        [AllowAnonymous]
        public IHttpActionResult GetTypeaheadCity(string query)
        {
            IList<TypeaheadCityData> typeaheadDataList = Repository.GetTypeaheadCity(query);

            return this.Ok(typeaheadDataList);
        }

        [AllowAnonymous]
        [Route("GetAllLocations")]
        [AcceptVerbs("POST")]
        public IHttpActionResult GetAllLocations(RequestLocationData requestLocationData)
        {
            IList<RoomData>  locationDataList = Repository.GetLocations(requestLocationData);
            
            return this.Ok(locationDataList);         
        }

        [HttpPost]
        [Route("SaveRating")]
        [Authorize]                        
        public IHttpActionResult SaveRating(RatingData ratingData)
        {
            ResponseSaveRating responseSaveRating = Repository.SaveRating(ratingData);            

            return this.Ok(responseSaveRating);
        }

        [HttpGet]
        [Route("GetKitchenTypes")]        
        [AllowAnonymous]
        public IHttpActionResult GetKitchenTypes()
        {
            IList<KitchenTypeData> kitchenTypeDataList = Repository.GetKitchenTypes();            

            return this.Ok(kitchenTypeDataList);  
        }

        [HttpGet]
        [Route("GetKitchenInternationalTypes")]
        [AllowAnonymous]
        public IHttpActionResult GetKitchenInternationalTypes()
        {
            IList<KitchenInternationalTypeData> kitchenInternationalTypesDataList 
                                = Repository.GetKitchenInternationalTypes();
            
            return this.Ok(kitchenInternationalTypesDataList);
        }

        [HttpGet]
        [Route("GetSpecializationTypes")]
        [AllowAnonymous]
        public IHttpActionResult GetSpecializationTypes()
        {
            IList<SpecializationTypeData> specializationTypeDataList = Repository.GetSpecializationTypes();

            return this.Ok(specializationTypeDataList);
        }

        [HttpGet]        
        [Route("GetKitchenMenuTypes")]
        [AllowAnonymous]
        public IHttpActionResult GetKitchenMenuTypes()
        {
            IList<KitchenMenuTypeData> kitchenMenuTypeDataList = Repository.GetKitchenMenuTypes();
            
            return this.Ok(kitchenMenuTypeDataList);
        }             
               
        [HttpGet]
        [Route("GetRoomById/{id}")]
        [AllowAnonymous]        
        public IHttpActionResult GetRoomById(Guid id)
        {
            RoomData roomData = Repository.GetRoomById(id);            

            return this.Ok(roomData);
        }
                
        [HttpGet]
        [Route("getRoomListByUser")]
        [Authorize(Roles = Role.Administrator + "," + Role.BarOwner)]
        public IHttpActionResult GetRoomListByUser()
        {
            IList<RoomData> roomDataList = Repository.GetRoomListByUser();

            return this.Ok(roomDataList);
        }

        [HttpPost]        
        [Route("SaveRoom")]
        [Authorize(Roles = Role.Administrator + "," + Role.BarOwner)]
        public IHttpActionResult SaveRoom(RoomData roomData)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            ResponseSaveRoom responseSaveRoom = Repository.SaveRoom(roomData);            

            return this.Ok(responseSaveRoom);
        }

        [HttpGet]
        [Route("version")]
        [AllowAnonymous]
        public IHttpActionResult Version()
        {
            return this.Ok(StaticData.Version);
        }


        [HttpGet]
        [Route("GetJsonRoomModel/{id}")]
        [Authorize]
        public IHttpActionResult GetJsonRoomModel(Guid id)
        {
            try
            {
                var model = this.Repository.GetJsonRoomModel(id);
                return this.Ok(model);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("GetSvgRoomModel/{id}")]
        //[Authorize]
        [AllowAnonymous]
        public IHttpActionResult GetSvgRoomModel(Guid id)
        {
            try
            {
                var model = this.Repository.GetSvgRoomModel(id);
                return this.Ok(model);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }

        }

        [HttpPost]
        [Route("SaveJsonRoomModel")]
        [Authorize(Roles = Role.Administrator + "," + Role.BarOwner)]
        public IHttpActionResult SaveJsonRoomModel(RoomModel jsonModel)
        {
            try
            {
                this.Repository.SaveJsonRoomModel(jsonModel);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }

        }

        [HttpGet]
        [Route("GetBeaconsInRoom/{id}")]
        [AllowAnonymous]
        public HttpResponseMessage GetBeaconsInRoom(Guid id)
        {
            using (var ctx = new LpsContext())
            {
                var result =
                    ctx.Rooms.FirstOrDefault(x => x.Id == id)
                        .BeaconList.Select(
                            x => new { Id = x.Id, Identifier1 = x.Identifier1, Identifier2 = x.Identifier2, Identifier3 = x.Identifier3 });
                return this.Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        [HttpPost]
        [Route("UpdateBeaconIdentifier")]
        [AllowAnonymous]
        public IHttpActionResult UpdateBeaconIdentifier(BeaconsInRoom beaconsInRoom)
        {
            using (var ctx = new LpsContext())
            {
                var room = ctx.Rooms.FirstOrDefault(x => x.Id == beaconsInRoom.RoomId);

                var toDelete = new List<Guid>();
                foreach (var beacon in beaconsInRoom.Beacons)
                {
                    var toUpdate = room.BeaconList.FirstOrDefault(x => x.Identifier3 == beacon.Id3);
                    if (toUpdate != null)
                    {
                        toUpdate.Identifier1 = beacon.Id1;
                        toUpdate.Identifier2 = beacon.Id2;
                        toDelete.Add(beacon.Id);
                    }
                }

                foreach (var id in toDelete)
                {
                    var beacon = ctx.BeaconInRange.FirstOrDefault(x => x.Id == id);
                    ctx.BeaconInRange.Remove(beacon);

                }

                ctx.SaveChanges();
                return this.Ok();
            }

        }

    }
}