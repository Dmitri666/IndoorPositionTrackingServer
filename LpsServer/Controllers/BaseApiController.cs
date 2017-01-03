// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="">
//   
// </copyright>
// <summary>
//   The base api controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LpsServer.Controllers
{
    using System.Web.Http;

    using Lps.Contracts.Interfaces;
    using Lps.Services;
    using Lps.Services.Helper;

    /// <summary>
    /// The base api controller.
    /// </summary>
    public class BaseApiController : ApiController
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// </summary>
        public BaseApiController()
        {
            this.Repository = new LpsRepository();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the repository.
        /// </summary>
        protected LpsRepository Repository { get; private set; }

        #endregion

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Repository != null)
                {
                    Repository.Dispose();
                    Repository = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}