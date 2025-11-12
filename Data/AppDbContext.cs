using System.Security.Claims;
using GuestHouseBookingApplication_Server.Models;
using GuestHouseBookingApplication_Server.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GuestHouseBookingApplication_Server.Data
{
    public class AppDbContext : DbContext
    {
        //public AppDbContext(DbContextOptions<AppDbContext> options)
        //    : base(options) { }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<GuestHouse> GuestHouses { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<Bed> Beds { get; set; } = null!;
        public DbSet<Booking> Bookings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //---------------------- User Table ----------------------
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("UserTable");
                entity.HasKey(u => u.UserId).HasName("PK_UserTable");
                entity.Property(u => u.UserId).HasColumnName("User_ID");
                entity
                    .Property(u => u.Username)
                    .HasColumnName("Username")
                    .HasMaxLength(150)
                    .IsRequired();
                entity
                    .Property(u => u.PasswordHash)
                    .HasColumnName("Password_Hash")
                    .HasMaxLength(255)
                    .IsRequired();
                entity
                    .Property(u => u.PasswordSalt)
                    .HasColumnName("Password_Sault")
                    .HasMaxLength(255)
                    .IsRequired();
                entity
                    .Property(u => u.EmailId)
                    .HasColumnName("Email_ID")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(u => u.ContactNumber).HasColumnName("Contact_Number");
                entity
                    .Property(u => u.FirstName)
                    .HasColumnName("First_Name")
                    .HasMaxLength(100)
                    .IsRequired();
                entity
                    .Property(u => u.LastName)
                    .HasColumnName("Last_Name")
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(u => u.Role).HasColumnName("Role").HasMaxLength(20);
                entity.Property(u => u.CreatedDate).HasColumnName("Created_Date");
                entity.Property(u => u.CreatedBy).HasColumnName("Created_By");
                entity.Property(u => u.ModifiedDate).HasColumnName("Modification_Date");
                entity.Property(u => u.ModifiedBy).HasColumnName("Modified_By");
                entity.Property(g => g.DeletedBy).HasColumnName("Deleted_By");
                entity.Property(g => g.DeletedDate).HasColumnName("Deleted_Date");

                entity
                    .Property(u => u.ActiveStatus)
                    .HasColumnName("Active_Status")
                    .HasMaxLength(20);
            });

            //----------------------------------------------------------------------------------------
            //---------------------- GuestHouse ----------------------
            /* ------------------- GUEST HOUSE ------------------- */
            modelBuilder.Entity<GuestHouse>(entity =>
            {
                entity.ToTable("GuestHouse");
                entity.HasKey(g => g.GuestHouseId).HasName("PK_GuestHouse");
                entity.Property(g => g.GuestHouseId).HasColumnName("Guest_House_ID");
                entity
                    .Property(g => g.GuestHouseName)
                    .HasColumnName("Guest_House_Name")
                    .HasMaxLength(255)
                    .IsRequired();
                entity
                    .Property(g => g.GuestHouseAddress)
                    .HasColumnName("Guest_House_Address")
                    .HasMaxLength(255)
                    .IsRequired();
                entity
                    .Property(g => g.ContactEmail)
                    .HasColumnName("Contact_Email")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.Property(g => g.ContactNumber).HasColumnName("Contact_Number");

                entity.Property(g => g.CreatedDate).HasColumnName("Created_Date");
                entity.Property(g => g.CreatedBy).HasColumnName("Created_By");
                entity.Property(g => g.ModifiedDate).HasColumnName("Modification_Date");
                entity.Property(g => g.ModifiedBy).HasColumnName("Modified_By");
                entity.Property(g => g.DeletedBy).HasColumnName("Deleted_By");
                entity.Property(g => g.DeletedDate).HasColumnName("Deleted_Date");

                entity
                    .Property(g => g.ActiveStatus)
                    .HasColumnName("Active_Status")
                    .HasMaxLength(20);

                entity
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey(g => g.CreatedBy)
                    .HasConstraintName("FK_GuestHouse_CreatedBy_UserTable");

                entity
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey(g => g.ModifiedBy)
                    .HasConstraintName("FK_GuestHouse_ModifiedBy_UserTable");
            });

            //----------------------------------------------------------------------------------------
            //---------------------- Room ----------------------
            /* ------------------- ROOM ------------------- */
            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");
                entity.HasKey(r => r.RoomId).HasName("PK_Room");
                entity.Property(r => r.RoomId).HasColumnName("Room_ID");

                entity.Property(r => r.GuestHouseId).HasColumnName("Guest_House_ID");

                entity
                    .Property(r => r.RoomNo)
                    .HasColumnName("Room_No")
                    .HasMaxLength(50)
                    .IsRequired();

                entity
                    .Property(r => r.RoomName)
                    .HasColumnName("Room_Name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity
                    .Property(r => r.RoomDescription)
                    .HasColumnName("Room_Description")
                    .HasMaxLength(255);

                entity.Property(r => r.CreatedBy).HasColumnName("Created_By");
                entity.Property(r => r.CreatedDate).HasColumnName("Created_Date");
                entity.Property(r => r.ModifiedBy).HasColumnName("Modified_By");
                entity.Property(r => r.ModifiedDate).HasColumnName("Modification_Date");
                entity.Property(g => g.DeletedBy).HasColumnName("Deleted_By");
                entity.Property(g => g.DeletedDate).HasColumnName("Deleted_Date");

                entity
                    .Property(r => r.ActiveStatus)
                    .HasColumnName("Active_Status")
                    .HasMaxLength(20);

                entity
                    .HasOne(r => r.GuestHouse)
                    .WithMany(g => g.Rooms)
                    .HasForeignKey(r => r.GuestHouseId)
                    .HasConstraintName("FK_Room_GuestHouse");
            });
            //----------------------------------------------------------------------------------------
            //---------------------- Bed ----------------------
            /* ------------------- BED ------------------- */
            modelBuilder.Entity<Bed>(entity =>
            {
                entity.ToTable("Bed");
                entity.HasKey(b => b.BedId).HasName("PK_Bed");
                entity.Property(b => b.BedId).HasColumnName("Bed_ID");

                entity.Property(b => b.RoomId).HasColumnName("Room_ID");

                entity.Property(b => b.BedNo).HasColumnName("Bed_No").HasMaxLength(50).IsRequired();

                entity
                    .Property(b => b.BedName)
                    .HasColumnName("Bed_Name")
                    .HasMaxLength(100)
                    .IsRequired();

                entity
                    .Property(b => b.BedDescription)
                    .HasColumnName("Bed_Description")
                    .HasMaxLength(255);

                entity.Property(b => b.CreatedBy).HasColumnName("Created_By");
                entity.Property(b => b.CreatedDate).HasColumnName("Created_Date");
                entity.Property(b => b.ModifiedBy).HasColumnName("Modified_By");
                entity.Property(b => b.ModifiedDate).HasColumnName("Modification_Date");
                entity.Property(g => g.DeletedBy).HasColumnName("Deleted_By");
                entity.Property(g => g.DeletedDate).HasColumnName("Deleted_Date");

                entity
                    .Property(b => b.ActiveStatus)
                    .HasColumnName("Active_Status")
                    .HasMaxLength(20);

                entity
                    .HasOne(b => b.Room)
                    .WithMany(r => r.Beds)
                    .HasForeignKey(b => b.RoomId)
                    .HasConstraintName("FK_Bed_Room");
            });

            //----------------------------------------------------------------------------------------
            //---------------------- Booking Table ----------------------
            /* ------------------- BOOKING ------------------- */
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");
                entity.HasKey(bk => bk.BookingId).HasName("PK_Booking");
                entity.Property(bk => bk.BookingId).HasColumnName("Booking_ID");

                entity.Property(bk => bk.UserId).HasColumnName("User_ID");
                entity.Property(bk => bk.BedId).HasColumnName("Bed_ID");

                entity.Property(bk => bk.CheckInDate).HasColumnName("Check_In_Date");
                entity.Property(bk => bk.CheckOutDate).HasColumnName("Check_Out_Date");
                entity
                    .Property(bk => bk.BookingStatus)
                    .HasColumnName("Booking_Status")
                    .HasMaxLength(50);

                entity.Property(bk => bk.CreatedBy).HasColumnName("Created_By");
                entity.Property(bk => bk.CreatedDate).HasColumnName("Created_Date");
                entity.Property(bk => bk.ModifiedBy).HasColumnName("Modified_By");
                entity.Property(bk => bk.ModifiedDate).HasColumnName("Modification_Date");
                entity.Property(g => g.DeletedBy).HasColumnName("Deleted_By");
                entity.Property(g => g.DeletedDate).HasColumnName("Deleted_Date");

                entity
                    .Property(bk => bk.ActiveStatus)
                    .HasColumnName("Active_Status")
                    .HasMaxLength(20);

                entity
                    .HasOne(bk => bk.User)
                    .WithMany()
                    .HasForeignKey(bk => bk.UserId)
                    .HasConstraintName("FK_Booking_User");

                entity
                    .HasOne(bk => bk.Bed)
                    .WithMany()
                    .HasForeignKey(bk => bk.BedId)
                    .HasConstraintName("FK_Booking_Bed");
            });
            //----------------------------------------------------------------------------------------

            // Configure other entities later (Audit).
        }
        //----------------------------------------------------------------------------------------

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();

            var entries = ChangeTracker.Entries<IAuditableEntity>();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.ActiveStatus ??= "Active";
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        entry.Entity.ModifiedBy = userId;
                        break;

                    case EntityState.Deleted:
                        // Perform soft delete
                        entry.Entity.DeletedDate = DateTime.UtcNow;
                        entry.Entity.DeletedBy = userId;
                        entry.State = EntityState.Modified;
                        entry.Entity.ActiveStatus = "Inactive";
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private int? GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return userId;
            return null;
        }

    }
}
