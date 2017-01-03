// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Conversation.cs" company="">
//   
// </copyright>
// <summary>
//   The conversation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// The conversation.
    /// </summary>
    public class Conversation
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Conversation"/> class.
        /// </summary>
        public Conversation()
        {
            this.Messages = new HashSet<ConversationMessage>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public virtual ICollection<ConversationMessage> Messages { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public DateTime Time { get; set; }

        public Guid User1_Id { get; set; }

        public Guid User2_Id { get; set; }

        /// <summary>
        /// Gets or sets the user 1.
        /// </summary>
        [ForeignKey("User1_Id")]
        [InverseProperty("MyConversations")]
        public virtual User User1 { get; set; }

        /// <summary>
        /// Gets or sets the user 2.
        /// </summary>
        [ForeignKey("User2_Id")]
        [InverseProperty("TheirsConversations")]
        public virtual User User2 { get; set; }

        #endregion
    }
}