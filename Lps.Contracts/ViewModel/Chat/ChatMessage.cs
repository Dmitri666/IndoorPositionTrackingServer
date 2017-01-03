// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatMessage.cs" company="">
//   
// </copyright>
// <summary>
//   The chat message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Lps.Contracts.ViewModel.Chat
{
    using System;

    /// <summary>
    /// The chat message.
    /// </summary>
    public class ChatMessage
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the conversation id.
        /// </summary>
        public Guid ConversationId { get; set; }

        /// <summary>
        /// Gets or sets the conversation message id.
        /// </summary>
        public Guid ConversationMessageId { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }

        public bool IsMe { get; set; }

        #endregion
    }
}