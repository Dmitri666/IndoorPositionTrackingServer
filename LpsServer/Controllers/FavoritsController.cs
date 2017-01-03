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
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Lps.Contracts.ViewModel;

    /// <summary>
    ///     The rooms controller.
    /// </summary>
    [RoutePrefix("api/Favorits")]
    [Authorize]
    public class FavoritsController : BaseApiController
    {

        /// <summary>
        ///     The get.
        /// </summary>
        /// <returns>
        ///     The <see cref="HttpResponseMessage" />.
        /// </returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                var rooms = this.Repository.GetFavorits();
                return this.Ok( rooms);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("names")]
        public IHttpActionResult GetFavoritNames()
        {
            try
            {
                var cities = this.Repository.GetFavoritNames();
                return this.Ok(cities);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("insert/{id:guid}")]
        public IHttpActionResult Insert(Guid id)
        {
            try
            {
                this.Repository.InsertFavorit(id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("delete/{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                this.Repository.DeleteFavorit(id);
                return this.Ok();
            }
            catch (Exception ex)
            {
                return this.InternalServerError(ex);
            }
        }
    }
}