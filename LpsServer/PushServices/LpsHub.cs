// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LpsHub.cs" company="">
//   
// </copyright>
// <summary>
//   The position hub.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LpsServer.PushServices
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Lps.Services;

    using Microsoft.AspNet.SignalR;

    /// <summary>
    /// The position hub.
    /// </summary>
    public class LpsHub : Hub
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LpsHub"/> class.
        /// </summary>
        public LpsHub()
        {
            
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The join group.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task JoinGroup(string groupName)
        {
            return this.Groups.Add(this.Context.ConnectionId, groupName);
        }

        /// <summary>
        /// The leave group.
        /// </summary>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task LeaveGroup(string groupName)
        {
            return this.Groups.Remove(this.Context.ConnectionId, groupName);
        }

        /// <summary>
        /// The on connected.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task OnConnected()
        {
            this.Groups.Add(this.Context.ConnectionId, this.Context.User.Identity.Name);
            return base.OnConnected();
        }

        /// <summary>
        /// The on disconnected.
        /// </summary>
        /// <param name="stopCalled">
        /// The stop called.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// The on reconnected.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public void ConversationMessageDelivered(Guid conversationMessageId, string response)
        {
            using (var repo = new LpsRepository())
            {
                repo.DeleteConversationMessage(conversationMessageId);
            }
        }

        #endregion
    }
}