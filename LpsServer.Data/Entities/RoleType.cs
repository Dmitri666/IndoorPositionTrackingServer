using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    public class RoleType
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        ///     Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        // Reference
        /// <summary>
        ///     Gets or sets the User Role List
        /// </summary>
        public virtual ICollection<UserRole> UserRoleList { get; set; }

         /// <summary>
        /// Initializes a new instance of the <see cref="RoleType"/> class.
        /// </summary>
        public RoleType()
        {
            this.UserRoleList = new HashSet<UserRole>();
        }
    }
}
