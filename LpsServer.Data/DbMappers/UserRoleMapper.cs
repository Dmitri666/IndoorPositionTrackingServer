using LpsServer.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LpsServer.Data.DbMappers
{    
    public class UserRoleMapper : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
                        
            //table  
            this.ToTable("dbo.UserRole");

            //relationship              
            this.HasRequired(e => e.RoleType).WithMany(e => e.UserRoleList).Map(s => s.MapKey("RoleTypeId")).WillCascadeOnDelete(false);
            this.HasRequired(e => e.User).WithMany(e => e.UserRoleList).Map(s => s.MapKey("UserId")).WillCascadeOnDelete(true);
        }
    }
}
