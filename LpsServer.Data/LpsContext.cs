namespace LpsServer.Data
{
    using System.Data.Entity;

    using LpsServer.Data.DbMappers;
    using LpsServer.Data.Entities;
    using LpsServer.Data.Migrations;

    public class LpsContext : DbContext
    {
        public LpsContext()
            : base("RoomContextConnection")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LpsContext, Configuration>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<LpsContext>());
        }

        public DbSet<Room> Rooms { get; set; }
        
        public DbSet<User> Users { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Logging> LoggingData { get; set; }

        public DbSet<Beacon> Beacons { get; set; }

        public DbSet<RoleType> RoleType { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<RoomTable> RoomTables { get; set; }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<ConversationMessage> ConversationMessages { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<KitchenType> KitchenType { get; set; }

        public DbSet<RoomKitchen> RoomKitchen { get; set; }

        public DbSet<BusinessHours> BusinessHours { get; set; }

        public DbSet<KitchenMenu> KitchenMenus { get; set; }

        public DbSet<KitchenMenuType> KitchenMenuTypes { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<KitchenInternationalType> KitchenInternationalType { get; set; }

        public DbSet<RoomKitchenInternational> RoomKitchenInternational { get; set; }

        public DbSet<Favorits> Favorits { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<SpecializationType> SpecializationTypes { get; set; }

        public DbSet<BeaconInRange> BeaconInRange { get; set; }

        public DbSet<UserPhoto> UserPhotos { get; set; }

        public DbSet<BookingRoomTable> BookingRoomTables { get; set; }

        public DbSet<PositionLog> PositionLogs { get; set; }

        //http://www.codeproject.com/Articles/796540/Relationship-in-Entity-Framework-Using-Code-First
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RoomMapper());
            modelBuilder.Configurations.Add(new PositionMapper());
            modelBuilder.Configurations.Add(new LoggingMapper());
            modelBuilder.Configurations.Add(new UserMapper());
            modelBuilder.Configurations.Add(new BeaconMapper());
            modelBuilder.Configurations.Add(new RoleTypeMapper());
            modelBuilder.Configurations.Add(new UserRoleMapper());
            modelBuilder.Configurations.Add(new RoomTableMapper());
            modelBuilder.Configurations.Add(new BookingMapper());
            modelBuilder.Configurations.Add(new ConversationMapper());
            modelBuilder.Configurations.Add(new ConversationMessageMappper());
            modelBuilder.Configurations.Add(new PhotoMapper());
            modelBuilder.Configurations.Add(new KitchenTypeMapper());
            modelBuilder.Configurations.Add(new RoomKitchenMapper());
            modelBuilder.Configurations.Add(new BusinessHoursMapper());
            modelBuilder.Configurations.Add(new KitchenMenuMapper());
            modelBuilder.Configurations.Add(new KitchenMenuTypeMapper());
            modelBuilder.Configurations.Add(new RatingMapper());
            modelBuilder.Configurations.Add(new KitchenInternationalTypeMapper());
            modelBuilder.Configurations.Add(new RoomKitchenInternationalMapper());
            modelBuilder.Configurations.Add(new FavoritsMapper());
            modelBuilder.Configurations.Add(new SpecializationTypeMapper());
            modelBuilder.Configurations.Add(new SpecializationMapper());
            modelBuilder.Configurations.Add(new UserPhotoMapper());
            modelBuilder.Configurations.Add(new BookingRoomTableMapper());
            modelBuilder.Configurations.Add(new PositionLogMapper());

            base.OnModelCreating(modelBuilder);
        }
    }
}