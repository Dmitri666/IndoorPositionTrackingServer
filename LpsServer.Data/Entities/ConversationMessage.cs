// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConversationMessage.cs" company="">
//   
// </copyright>
// <summary>
//   The conversation message.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.Entities
{
    using System;

    /// <summary>
    /// The conversation message.
    /// </summary>
    public class ConversationMessage
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the conversation.
        /// </summary>
        public virtual Conversation Conversation { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        public virtual User Sender { get; set; }

        public DateTime Time { get; set; }

        #endregion
    }
}