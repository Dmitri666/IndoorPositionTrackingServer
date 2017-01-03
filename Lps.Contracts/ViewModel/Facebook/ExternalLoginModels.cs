namespace Lps.Contracts.ViewModel.Facebook
{
    using System.ComponentModel.DataAnnotations;

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class RegisterExternalBindingModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Provider { get; set; }

        [Required]
        public string ExternalAccessToken { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>        
        [Required]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>                
        [Required]
        public string Email { get; set; }

    }

    public class ParsedExternalAccessToken
    {
        public string user_id { get; set; }
        public string app_id { get; set; }
    }
}
