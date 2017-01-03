namespace LpsServer.Data.DbMappers
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using LpsServer.Data.Entities;

    public class BusinessHoursMapper : EntityTypeConfiguration<BusinessHours>
    {
        public BusinessHoursMapper()
        {
            //Key 
            this.HasKey(c => c.Id);

            //Fields  
            this.Property(c => c.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(c => c.Id).IsRequired();
            
            this.Property(c => c.Day).IsRequired();
            this.Property(c => c.OpenTime).IsRequired();            
            this.Property(c => c.CloseTime).IsRequired();
            this.Property(c => c.PauseStart).IsOptional();
            this.Property(c => c.PauseEnd).IsOptional();
            this.Property(c => c.Close).IsRequired();

            //table  
            this.ToTable("dbo.BusinessHours");

            //relationship                          
            this.HasRequired(e => e.Room).WithMany(e => e.BusinessHoursList).Map(s => s.MapKey("RoomId")).WillCascadeOnDelete(true);
        }
    }
}
