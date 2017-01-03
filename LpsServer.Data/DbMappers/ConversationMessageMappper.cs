// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConversationMessageMappper.cs" company="">
//   
// </copyright>
// <summary>
//   The conversation message mappper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using System.Security.Cryptography.X509Certificates;

    using LpsServer.Data.Entities;

    /// <summary>
    /// The conversation message mappper.
    /// </summary>
    public class ConversationMessageMappper : EntityTypeConfiguration<ConversationMessage>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationMessageMappper"/> class.
        /// </summary>
        public ConversationMessageMappper()
        {
            // Key
            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasRequired(e => e.Sender)
                .WithMany(e => e.ConversationMessages)
                .Map(s => s.MapKey("User_Id"))
                .WillCascadeOnDelete(false);
            this.HasRequired(e => e.Conversation).WithMany(x => x.Messages).Map(s => s.MapKey("Conversation_Id")).WillCascadeOnDelete(true);

            // table  
            this.ToTable("dbo.ConversationMessage");
        }

        #endregion
    }
}