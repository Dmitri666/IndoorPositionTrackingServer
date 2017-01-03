// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatController.cs" company="">
//   
// </copyright>
// <summary>
//   The chat controller.
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

    using Lps.Contracts.ViewModel.Chat;
    using Lps.Services;

    using LpsServer.PushServices;

    /// <summary>
    /// The chat controller.
    /// </summary>
    [RoutePrefix("api/Chat")]
    public class ChatController : BaseApiController
    {
        #region Public Methods and Operators

        /// <summary>
        /// The create conversation.
        /// </summary>
        /// <param name="resipientId">
        /// The resipient id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        [Route("CreateConversation/{resipientId}")]
        [Authorize]
        public HttpResponseMessage CreateConversation(Guid resipientId)
        {
            ConversationsData conversationData = this.Repository.CreateConversation(resipientId);

            return this.Request.CreateResponse(HttpStatusCode.OK, conversationData);
        }

        /// <summary>
        /// The delete message.
        /// </summary>
        /// <param name="messageId">
        /// The message id.
        /// </param>
        [HttpDelete]
        [Route("ConversationMessage/{messageId:guid}")]
        [Authorize]

        // DELETE: api/MapData/5
        public void DeleteMessage(Guid messageId)
        {
            this.Repository.DeleteConversationMessage(messageId);

        }

        /// <summary>
        /// The get actors.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        [Route("Actors/{roomId:guid}")]
        [Authorize]
        public HttpResponseMessage GetActorsInLocale(Guid roomId)
        {
            var actorsLeavingLocale = this.Repository.GetActorsLeavingLocale();
            foreach (var actor in actorsLeavingLocale)
            {
                LpsHubWrapper.GetInstance().NotifyActorLeaveChat(roomId, actor);
            }

            var model = this.Repository.GetActorsModel(roomId);
            
            return this.Request.CreateResponse(HttpStatusCode.OK, model);
        }

        [HttpGet]
        [Route("Beacons/{roomId:guid}")]
        [Authorize]
        public HttpResponseMessage GetBeaconsInLocale(Guid roomId)
        {
            using (var ctx = new LpsServer.Data.LpsContext())
            {
                var room = ctx.Rooms.FirstOrDefault(m => m.Id == roomId);
                var model = new 
                {
                    Wight = room.RoomWidth,
                    Height = room.RoomHeight,
                    realScaleFactor = room.RealScaleFactor * room.LayoutZoom,
                    Beacons = room.BeaconList.Select( x => new  {
                        Id1 = x.Identifier1,
                        Id2 = x.Identifier2,
                        Id3 = x.Identifier3,
                        X = x.X,
                        Y = x.Y
                    } )
                };
                
                return this.Request.CreateResponse(HttpStatusCode.OK, model);
            }
               

            
        }

        [HttpPost]
        [Route("SetPosition")]
        [Authorize]
        public HttpResponseMessage SetPosition(DevicePosition position)
        {
            var isNew = this.Repository.SetPosition(position);
            if (isNew)
            {
                var actor = this.Repository.GetActorByDeviceId(position.DeviceId);
                if (actor != null)
                {
                    actor.Position = position;
                    LpsHubWrapper.GetInstance().NotifyActorJoinChat(position.RoomId, actor);
                }
            }
            LpsHubWrapper.GetInstance().NotifyPositionChanged(position.RoomId, new List<DevicePosition>() { position });
            
            
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("RemovePosition")]
        [Authorize]
        public HttpResponseMessage RemovePosition(DevicePosition position)
        {
            this.Repository.RemovePosition(position);
            var actor = this.Repository.GetActorByDeviceId(position.DeviceId);
            if (actor != null)
            {
                actor.Position = position;
                LpsHubWrapper.GetInstance().NotifyActorLeaveChat(position.RoomId, actor);
            }
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        /// <summary>
        /// The get conversation.
        /// </summary>
        /// <param name="resipientId">
        /// The resipient id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        [Route("GetConversation/{resipientId}")]
        [Authorize]
        public HttpResponseMessage GetConversation(Guid resipientId)
        {
            ConversationsData conversationData = this.Repository.GetConversation(resipientId);

            return this.Request.CreateResponse(HttpStatusCode.OK, conversationData);
        }

        /// <summary>
        /// The get conversations.
        /// </summary>
        /// <param name="roomId">
        /// The room id.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpGet]
        [Route("Conversations/{roomId:guid?}")]
        [Authorize]
        public HttpResponseMessage GetConversations(Guid? roomId)
        {
            List<ConversationsData> conversations = this.Repository.GetConversations(roomId);

            return this.Request.CreateResponse(HttpStatusCode.OK, conversations);
        }

        // PUT: api/MapData/5
        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// The send message.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="HttpResponseMessage"/>.
        /// </returns>
        [HttpPost]
        [Route("SendMessage")]
        [Authorize]
        public HttpResponseMessage SendMessage(ChatMessage message)
        {
            try
            {
                var resipientName = this.Repository.SaveConversationMessage(message);
                message.IsMe = false;
                LpsHubWrapper.GetInstance().SendConversationMessage(resipientName, message);
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        #endregion
    }
}