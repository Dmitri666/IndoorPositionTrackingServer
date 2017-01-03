// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConversationsData.cs" company="">
//   
// </copyright>
// <summary>
//   The conversations data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Contracts.ViewModel.Chat
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The conversations data.
    /// </summary>
    public class ConversationsData
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationsData"/> class.
        /// </summary>
        public ConversationsData()
        {
            this.Messages = new List<ChatMessage>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the conversation id.
        /// </summary>
        public Guid ConversationId { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public List<ChatMessage> Messages { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        #endregion
    }
}