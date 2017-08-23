namespace h2SygehusnordLoc
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class databaseContext : DbContext
    {
        public databaseContext()
            : base("name=databaseContext")
        {
        }

        public virtual DbSet<Building> Building { get; set; }
        public virtual DbSet<Consumption> Consumption { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Hall> Hall { get; set; }
        public virtual DbSet<Maintenance> Maintenance { get; set; }
        public virtual DbSet<MaintenanceType> MaintenanceType { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Security> Security { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Building>()
                .HasMany(e => e.Department)
                .WithOptional(e => e.Building)
                .HasForeignKey(e => e.building_ID);

            modelBuilder.Entity<Department>()
                .Property(e => e.department_name)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Hall)
                .WithOptional(e => e.Department)
                .HasForeignKey(e => e.department_ID);

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Room)
                .WithOptional(e => e.Hall)
                .HasForeignKey(e => e.hall_ID);

            modelBuilder.Entity<MaintenanceType>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<MaintenanceType>()
                .HasMany(e => e.Maintenance)
                .WithOptional(e => e.MaintenanceType)
                .HasForeignKey(e => e.type);

            modelBuilder.Entity<Patient>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Patient>()
                .Property(e => e.cpr_nr)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Consumption)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.room_ID);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Maintenance)
                .WithOptional(e => e.Room)
                .HasForeignKey(e => e.room_ID);
        }
    }
}
