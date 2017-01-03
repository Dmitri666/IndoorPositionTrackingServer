// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="">
//   
// </copyright>
// <summary>
//   The user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace LpsServer.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///     The user.
    /// </summary>
    [Table("User")]
    public class User
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.UserRoleList = new HashSet<UserRole>();
            this.Bookings = new HashSet<Booking>();
            this.Rooms = new HashSet<Room>();
            this.MyConversations = new HashSet<Conversation>();
            this.TheirsConversations = new HashSet<Conversation>();
            this.ConversationMessages = new HashSet<ConversationMessage>();
            this.RatingList = new HashSet<Rating>(); 
            this.FavoritsList = new HashSet<Favorits>();
            this.PhotoList = new HashSet<UserPhoto>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Bookings
        /// </summary>
        public virtual ICollection<Booking> Bookings { get; set; }

        /// <summary>
        /// Gets or sets the conversation messages.
        /// </summary>
        public virtual ICollection<ConversationMessage> ConversationMessages { get; set; }

        /// <summary>
        /// Gets or sets the conversations 1.
        /// </summary>
        public virtual ICollection<Conversation> MyConversations { get; set; }

        /// <summary>
        /// Gets or sets the conversations 2.
        /// </summary>
        public virtual ICollection<Conversation> TheirsConversations { get; set; }

        /// <summary>
        /// Gets or sets the user device.
        /// </summary>
        public virtual Device CurrentDevice { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        [MaxLength(20)]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>        
        [MaxLength(30)]
        public string Company { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [Required]
        [MaxLength(15)]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the Photo.
        /// </summary>        
        [MaxLength(200)]
        public string Photo { get; set; }

        /// <summary>
        ///     Gets or sets the ProviderKey.
        /// </summary>              
        public string ProviderKey { get; set; }

        /// <summary>
        ///     Gets or sets the ProviderKey.
        /// </summary>      
        [MaxLength(50)]
        public string LoginProvider { get; set; }

        

        /// <summary>
        /// Rooms
        /// </summary>
        public virtual ICollection<Room> Rooms { get; set; }     

        /// <summary>
        ///     Gets or sets the User Role List
        /// </summary>
        public virtual ICollection<UserRole> UserRoleList { get; set; }

        /// <summary>
        ///     Gets or sets the RatingList
        /// </summary>
        public virtual ICollection<Rating> RatingList { get; set; }

        /// <summary>
        /// Favorits
        /// </summary>
        public virtual ICollection<Favorits> FavoritsList { get; set; }

        /// <summary>
        /// User-Photos
        /// </summary>
        public virtual ICollection<UserPhoto> PhotoList { get; set; }

        #endregion
    }
}