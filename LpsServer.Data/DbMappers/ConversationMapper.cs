// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConversationMapper.cs" company="">
//   
// </copyright>
// <summary>
//   The conversation mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    /// <summary>
    /// The conversation mapper.
    /// </summary>
    public class ConversationMapper : EntityTypeConfiguration<Conversation>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationMapper"/> class.
        /// </summary>
        public ConversationMapper()
        {
            // Key
            this.HasKey(s => s.Id);
            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(x => x.State).IsRequired();
            this.Property(x => x.Time).IsRequired();

            this.HasRequired(e => e.User1).WithMany(e => e.MyConversations).HasForeignKey(m => m.User1_Id);
            this.HasRequired(e => e.User2).WithMany(e => e.TheirsConversations).HasForeignKey(m => m.User2_Id).WillCascadeOnDelete(false);

            // table  
            this.ToTable("dbo.Conversation");
        }

        #endregion
    }
}