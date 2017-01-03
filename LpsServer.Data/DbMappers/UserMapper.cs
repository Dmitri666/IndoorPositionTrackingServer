// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserMapper.cs" company="">
//   
// </copyright>
// <summary>
//   The user mapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    /// <summary>
    /// The user mapper.
    /// </summary>
    public class UserMapper : EntityTypeConfiguration<User>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserMapper"/> class.
        /// </summary>
        public UserMapper()
        {
            this.Property(t => t.Name).HasColumnAnnotation(IndexAnnotation.AnnotationName,
                                                new IndexAnnotation(new IndexAttribute("IX_Name", 1) { IsUnique = true }));

            
            this.HasOptional<Device>(u => u.CurrentDevice).WithOptionalDependent(c => c.User).Map(p => p.MapKey("DeviceId"));
            //this.HasMany<Conversation>(u => u.Conversations).WithOptional(c => c.User1).Map(p => p.MapKey("User1_Id"));
            //this.HasMany<Conversation>(u => u.Conversations).WithOptional(c => c.User2).Map(p => p.MapKey("User2_Id"));
        }


        #endregion
    }
}