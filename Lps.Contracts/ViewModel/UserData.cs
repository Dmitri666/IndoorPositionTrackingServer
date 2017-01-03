// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserData.cs" company="">
//   
// </copyright>
// <summary>
//   The user data.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Lps.Contracts.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using Files;
    /// <summary>
    /// The user data.
    /// </summary>
    public class UserData
    {
        public UserData()
        {
            this.Roles = new List<string>();
            this.Photos = new List<PhotoData>();
        }

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>        
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the Photo.
        /// </summary>                
        public string Photo { get; set; }

        /// <summary>
        ///     Gets or sets the MainUserRole.
        /// </summary>                
        public string MainUserRole { get; set; }

        /// <summary>
        ///     Gets or sets the Company.
        /// </summary>                
        public string Company { get; set; }

        /// <summary>
        ///     Gets or sets the role.
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        ///     Gets or sets the Photos.
        /// </summary>
        public List<PhotoData> Photos { get; set; }

        /// <summary>
        ///     Gets or sets the Company.
        /// </summary>    
        public string ProviderKey { get; set; }

        /// <summary>
        ///     Gets or sets the Company.
        /// </summary>    
        public string LoginProvider { get; set; }

        #endregion
    }
}