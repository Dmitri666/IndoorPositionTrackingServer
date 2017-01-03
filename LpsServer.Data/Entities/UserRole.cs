using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.Entities
{
    public class UserRole
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        // Reference
        /// <summary>
        ///     Gets or sets the room.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        ///     Gets or sets the room type.
        /// </summary>
        public virtual RoleType RoleType { get; set; }
    }
}
