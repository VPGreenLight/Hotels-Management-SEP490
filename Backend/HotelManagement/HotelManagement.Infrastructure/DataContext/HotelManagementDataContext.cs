using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Models.Enums;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using NpgsqlTypes;

namespace HotelManagement.Infrastructure.DataContext
{
    /// <summary>
    /// DbContext chính của hệ thống quản lý khách sạn Tân Trường Sơn.
    /// Kế thừa từ IdentityDbContext để hỗ trợ ASP.NET Core Identity cho xác thực và phân quyền.
    /// Được cấu hình để sử dụng PostgreSQL làm database backend.
    /// </summary>
    public partial class HotelManagementDataContext
        : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>,
                UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>,
            IHotelManagementDataContext
    {
        #region Constructor

        /// <summary>
        /// Constructor khởi tạo DbContext với các options được cấu hình từ dependency injection.
        /// </summary>
        /// <param name="options">DbContextOptions chứa cấu hình connection string và provider settings</param>
        public HotelManagementDataContext(DbContextOptions<HotelManagementDataContext> options)
            : base(options)
        {
        }

        #endregion

        #region DbSets - Định nghĩa các bảng trong database

        // ========== Core Business Entities ==========

        /// <summary>
        /// Bảng chi nhánh khách sạn - Quản lý các cơ sở thuộc chuỗi Tân Trường Sơn
        /// </summary>
        public DbSet<Branch> Branches { get; set; } = null!;

        /// <summary>
        /// Bảng phòng - Quản lý thông tin phòng tại từng chi nhánh
        /// </summary>
        public DbSet<Room> Rooms { get; set; } = null!;

        /// <summary>
        /// Bảng loại phòng - Định nghĩa các hạng phòng (Standard, Deluxe, Suite...)
        /// </summary>
        public DbSet<RoomCategory> RoomCategories { get; set; } = null!;

        /// <summary>
        /// Bảng khách hàng - Lưu trữ thông tin khách hàng cá nhân và doanh nghiệp
        /// </summary>
        public DbSet<Customer> Customers { get; set; } = null!;

        /// <summary>
        /// Bảng khách hàng tiềm năng - Quản lý leads từ các kênh marketing
        /// </summary>
        public DbSet<Lead> Leads { get; set; } = null!;

        /// <summary>
        /// Bảng đặt phòng - Quản lý toàn bộ booking của khách hàng
        /// </summary>
        public DbSet<Booking> Bookings { get; set; } = null!;

        // ========== Service Management ==========

        /// <summary>
        /// Bảng dịch vụ - Danh sách các dịch vụ cung cấp tại chi nhánh
        /// (Spa, Massage, Laundry, Transportation, Room Service...)
        /// </summary>
        public DbSet<Service> Services { get; set; } = null!;

        /// <summary>
        /// Bảng sử dụng dịch vụ - Tracking việc khách hàng sử dụng dịch vụ trong kỳ lưu trú
        /// </summary>
        public DbSet<ServiceUsage> ServiceUsages { get; set; } = null!;

        /// <summary>
        /// Bảng menu món ăn/đồ uống - Quản lý F&B items
        /// </summary>
        public DbSet<MenuItem> MenuItems { get; set; } = null!;

        // ========== Financial Management ==========

        /// <summary>
        /// Bảng hóa đơn - Quản lý các loại hóa đơn (Deposit, Proforma, Final)
        /// </summary>
        public DbSet<Invoice> Invoices { get; set; } = null!;

        /// <summary>
        /// Bảng hợp đồng - Quản lý hợp đồng với khách đoàn/doanh nghiệp
        /// </summary>
        public DbSet<Contract> Contracts { get; set; } = null!;

        // ========== Inventory Management ==========

        /// <summary>
        /// Bảng template kiểm kê - Định nghĩa danh sách đồ vật chuẩn cho mỗi loại phòng
        /// </summary>
        public DbSet<RoomInventoryTemplate> RoomInventoryTemplates { get; set; } = null!;

        /// <summary>
        /// Bảng phiếu kiểm kê phòng - Header của phiếu kiểm kê
        /// </summary>
        public DbSet<RoomInventoryCheck> RoomInventoryChecks { get; set; } = null!;

        /// <summary>
        /// Bảng chi tiết kiểm kê - Detail từng item trong phiếu kiểm kê
        /// </summary>
        public DbSet<RoomInventoryCheckDetail> RoomInventoryCheckDetails { get; set; } = null!;

        /// <summary>
        /// Bảng nguyên liệu - Quản lý nguyên liệu F&B và housekeeping
        /// </summary>
        public DbSet<Ingredient> Ingredients { get; set; } = null!;

        // ========== Operations Management ==========

        /// <summary>
        /// Bảng công việc - Quản lý tasks được giao cho nhân viên
        /// (Cleaning, Serving, Maintenance, Repair...)
        /// </summary>
        public DbSet<Job> Jobs { get; set; } = null!;

        /// <summary>
        /// Bảng yêu cầu - Quản lý các yêu cầu về vật tư, sửa chữa, báo cáo sự cố
        /// </summary>
        public DbSet<Request> Requests { get; set; } = null!;

        /// <summary>
        /// Bảng chính sách bảo trì - Định kỳ bảo trì thiết bị và cơ sở vật chất
        /// </summary>
        public DbSet<MaintenancePolicy> MaintenancePolicies { get; set; } = null!;

        // ========== Staff Management ==========

        /// <summary>
        /// Bảng chấm công - Tracking giờ làm việc của nhân viên
        /// </summary>
        public DbSet<Attendance> Attendances { get; set; } = null!;

        /// <summary>
        /// Bảng ra vào - Logging người và phương tiện ra vào chi nhánh
        /// </summary>
        public DbSet<EntryExit> EntryExits { get; set; } = null!;

        // ========== System & Configuration ==========

        /// <summary>
        /// Bảng thông báo - Push notifications cho nhân viên
        /// </summary>
        public DbSet<Notification> Notifications { get; set; } = null!;

        /// <summary>
        /// Bảng quyền - Định nghĩa permissions cho hệ thống phân quyền
        /// </summary>
        public DbSet<Permission> Permissions { get; set; } = null!;

        /// <summary>
        /// Bảng audit log - Ghi lại mọi thay đổi dữ liệu trong hệ thống
        /// </summary>
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;

        /// <summary>
        /// Bảng template - Quản lý templates cho hợp đồng, email, báo cáo
        /// </summary>
        public DbSet<Template> Templates { get; set; } = null!;

        /// <summary>
        /// Bảng báo cáo - Metadata của các báo cáo được generate
        /// </summary>
        public DbSet<Report> Reports { get; set; } = null!;

        /// <summary>
        /// Bảng chỗ đỗ xe - Quản lý parking slots tại chi nhánh
        /// </summary>
        public DbSet<ParkSlot> ParkSlots { get; set; } = null!;

        // ========== Authentication ==========

        /// <summary>
        /// Bảng refresh token - Quản lý JWT refresh tokens
        /// </summary>
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        /// <summary>
        /// Bảng email confirmation - Quản lý mã xác thực email
        /// </summary>
        public DbSet<EmailConfirmation> EmailConfirmations { get; set; } = null!;

        #endregion

        #region Interface Implementation

        /// <summary>
        /// Override method Set để expose DbSet cho generic repository pattern
        /// </summary>
        public new DbSet<TEntity> Set<TEntity>() where TEntity : class => base.Set<TEntity>();

        /// <summary>
        /// Async wrapper cho SaveChanges
        /// </summary>
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        /// <summary>
        /// Expose Database property cho migration và raw SQL
        /// </summary>
        public override DatabaseFacade Database => base.Database;

        #endregion

        #region OnModelCreating - Cấu hình Database Schema

        /// <summary>
        /// Cấu hình toàn bộ entity relationships, constraints, indexes và conventions
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========== Cấu hình ASP.NET Core Identity Tables ==========
            ConfigureIdentityTables(modelBuilder);

            // ========== Cấu hình Entity Relationships ==========
            ConfigureEntityRelationships(modelBuilder);

            // ========== Cấu hình Indexes ==========
            ConfigureIndexes(modelBuilder);

            // ========== Cấu hình Unique Constraints ==========
            ConfigureUniqueConstraints(modelBuilder);

            // ========== Cấu hình Check Constraints ==========
            ConfigureCheckConstraints(modelBuilder);

            // ========== Cấu hình Default Values ==========
            ConfigureDefaultValues(modelBuilder);

            // ========== Cấu hình Soft Delete Global Filter ==========
            ConfigureSoftDeleteFilter(modelBuilder);

            // ========== Cấu hình PostgreSQL Specific Features ==========
            ConfigurePostgreSQLFeatures(modelBuilder);

            // ========== Seed Initial Data ==========
            SeedInitialData(modelBuilder);
        }

        #endregion

        #region Configuration Methods

        /// <summary>
        /// Cấu hình tên bảng cho ASP.NET Core Identity
        /// </summary>
        private void ConfigureIdentityTables(ModelBuilder modelBuilder)
        {
            // Đổi tên bảng Identity sang lowercase theo convention PostgreSQL
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Role>().ToTable("roles");
            modelBuilder.Entity<UserRole>().ToTable("user_roles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");

            // Cấu hình composite key cho UserRole
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }

        /// <summary>
        /// Cấu hình các mối quan hệ giữa entities và cascade delete behavior
        /// </summary>
        private void ConfigureEntityRelationships(ModelBuilder modelBuilder)
        {
            // ========== Booking Relationships ==========

            // Booking -> Customer: Restrict (không xóa customer khi có booking)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking -> Room: Restrict (không xóa room khi có booking)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // Booking -> User (Receptionist): Restrict
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Receptionist)
                .WithMany(u => u.ReceptionistBookings)
                .HasForeignKey(b => b.ReceptionistId)
                .OnDelete(DeleteBehavior.Restrict);

            // ========== ServiceUsage Relationships ==========

            // ServiceUsage -> Booking: Cascade (xóa service usage khi xóa booking)
            modelBuilder.Entity<ServiceUsage>()
                .HasOne(su => su.Booking)
                .WithMany(b => b.ServiceUsages)
                .HasForeignKey(su => su.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // ServiceUsage -> Service: Restrict
            modelBuilder.Entity<ServiceUsage>()
                .HasOne(su => su.Service)
                .WithMany(s => s.ServiceUsages)
                .HasForeignKey(su => su.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // ServiceUsage -> Room: Restrict
            modelBuilder.Entity<ServiceUsage>()
                .HasOne(su => su.Room)
                .WithMany(r => r.ServiceUsages)
                .HasForeignKey(su => su.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // ServiceUsage -> User (RecordedBy): Restrict
            modelBuilder.Entity<ServiceUsage>()
                .HasOne(su => su.RecordedBy)
                .WithMany(u => u.RecordedServiceUsages)
                .HasForeignKey(su => su.RecordedById)
                .OnDelete(DeleteBehavior.Restrict);

            // ========== Invoice Relationships ==========

            // Invoice -> Booking: Restrict (không xóa booking khi có invoice)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Booking)
                .WithMany(b => b.Invoices)
                .HasForeignKey(i => i.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // Invoice -> Contract: SetNull (cho phép invoice tồn tại khi contract bị xóa)
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Contract)
                .WithMany()
                .HasForeignKey(i => i.ContractId)
                .OnDelete(DeleteBehavior.SetNull);

            // ========== Room Inventory Relationships ==========

            // RoomInventoryCheck -> Room: Restrict
            modelBuilder.Entity<RoomInventoryCheck>()
                .HasOne(ric => ric.Room)
                .WithMany(r => r.InventoryChecks)
                .HasForeignKey(ric => ric.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            // RoomInventoryCheck -> Booking: SetNull
            modelBuilder.Entity<RoomInventoryCheck>()
                .HasOne(ric => ric.Booking)
                .WithMany()
                .HasForeignKey(ric => ric.BookingId)
                .OnDelete(DeleteBehavior.SetNull);

            // RoomInventoryCheckDetail -> RoomInventoryCheck: Cascade
            modelBuilder.Entity<RoomInventoryCheckDetail>()
                .HasOne(ricd => ricd.RoomInventoryCheck)
                .WithMany(ric => ric.CheckDetails)
                .HasForeignKey(ricd => ricd.CheckId)
                .OnDelete(DeleteBehavior.Cascade);

            // RoomInventoryCheckDetail -> RoomInventoryTemplate: SetNull
            modelBuilder.Entity<RoomInventoryCheckDetail>()
                .HasOne(ricd => ricd.TemplateItem)
                .WithMany()
                .HasForeignKey(ricd => ricd.TemplateItemId)
                .OnDelete(DeleteBehavior.SetNull);

            // RoomInventoryCheckDetail -> Job: SetNull
            modelBuilder.Entity<RoomInventoryCheckDetail>()
                .HasOne(ricd => ricd.ActionJob)
                .WithMany()
                .HasForeignKey(ricd => ricd.ActionJobId)
                .OnDelete(DeleteBehavior.SetNull);

            // RoomInventoryTemplate -> RoomCategory: Cascade
            modelBuilder.Entity<RoomInventoryTemplate>()
                .HasOne(rit => rit.RoomCategory)
                .WithMany(rc => rc.InventoryTemplates)
                .HasForeignKey(rit => rit.RoomCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========== Branch Relationships ==========

            // Branch -> Room: Restrict
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Branch)
                .WithMany(b => b.Rooms)
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Branch -> Service: Cascade
            modelBuilder.Entity<Service>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Services)
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // ========== Job & Request Relationships ==========

            // Job -> User (AssignedTo): Restrict
            modelBuilder.Entity<Job>()
                .HasOne(j => j.AssignedTo)
                .WithMany(u => u.AssignedJobs)
                .HasForeignKey(j => j.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);

            // Job -> Branch: Restrict
            modelBuilder.Entity<Job>()
                .HasOne(j => j.Branch)
                .WithMany(b => b.Jobs)
                .HasForeignKey(j => j.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // Request -> User (RequestedBy): Restrict
            modelBuilder.Entity<Request>()
                .HasOne(r => r.RequestedBy)
                .WithMany()
                .HasForeignKey(r => r.RequestedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Request -> User (ApprovedBy): SetNull
            modelBuilder.Entity<Request>()
                .HasOne(r => r.ApprovedBy)
                .WithMany()
                .HasForeignKey(r => r.ApprovedById)
                .OnDelete(DeleteBehavior.SetNull);

            // ========== Lead Relationships ==========

            // Lead -> User (AssignedTo): SetNull
            modelBuilder.Entity<Lead>()
                .HasOne(l => l.AssignedTo)
                .WithMany(u => u.AssignedLeads)
                .HasForeignKey(l => l.AssignedToId)
                .OnDelete(DeleteBehavior.SetNull);

            // ========== Contract Relationships ==========

            // Contract -> Template: SetNull
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Template)
                .WithMany(t => t.Contracts)
                .HasForeignKey(c => c.TemplateId)
                .OnDelete(DeleteBehavior.SetNull);

            // ========== Attendance & EntryExit Relationships ==========

            // Attendance -> Branch: Restrict
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Branch)
                .WithMany(b => b.Attendances)
                .HasForeignKey(a => a.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            // EntryExit -> Branch: Restrict
            modelBuilder.Entity<EntryExit>()
                .HasOne(ee => ee.Branch)
                .WithMany(b => b.EntryExits)
                .HasForeignKey(ee => ee.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        /// <summary>
        /// Cấu hình indexes để tối ưu performance cho các query thường dùng
        /// </summary>
        private void ConfigureIndexes(ModelBuilder modelBuilder)
        {
            // ========== Booking Indexes ==========
            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.CustomerId)
                .HasDatabaseName("idx_booking_customer_id");

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.RoomId)
                .HasDatabaseName("idx_booking_room_id");

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.ReceptionistId)
                .HasDatabaseName("idx_booking_receptionist_id");

            // Composite index cho query booking theo ngày và status
            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.CheckInDate, b.CheckOutDate, b.Status })
                .HasDatabaseName("idx_booking_dates_status");

            // Index cho BookingCode để tìm kiếm nhanh
            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.BookingCode)
                .HasDatabaseName("idx_booking_code");

            // ========== Room Indexes ==========
            modelBuilder.Entity<Room>()
                .HasIndex(r => r.BranchId)
                .HasDatabaseName("idx_room_branch_id");

            modelBuilder.Entity<Room>()
                .HasIndex(r => r.CategoryId)
                .HasDatabaseName("idx_room_category_id");

            // Composite index cho tìm phòng trống theo chi nhánh
            modelBuilder.Entity<Room>()
                .HasIndex(r => new { r.BranchId, r.CurrentStatus })
                .HasDatabaseName("idx_room_branch_status");

            // ========== ServiceUsage Indexes ==========
            modelBuilder.Entity<ServiceUsage>()
                .HasIndex(su => su.BookingId)
                .HasDatabaseName("idx_service_usage_booking_id");

            modelBuilder.Entity<ServiceUsage>()
                .HasIndex(su => su.ServiceId)
                .HasDatabaseName("idx_service_usage_service_id");

            modelBuilder.Entity<ServiceUsage>()
                .HasIndex(su => su.UsageDate)
                .HasDatabaseName("idx_service_usage_date");

            // ========== Invoice Indexes ==========
            modelBuilder.Entity<Invoice>()
                .HasIndex(i => i.BookingId)
                .HasDatabaseName("idx_invoice_booking_id");

            modelBuilder.Entity<Invoice>()
                .HasIndex(i => new { i.IssueDate, i.Status })
                .HasDatabaseName("idx_invoice_date_status");

            // ========== RoomInventoryCheck Indexes ==========
            modelBuilder.Entity<RoomInventoryCheck>()
                .HasIndex(ric => ric.RoomId)
                .HasDatabaseName("idx_inventory_check_room_id");

            modelBuilder.Entity<RoomInventoryCheck>()
                .HasIndex(ric => ric.BookingId)
                .HasDatabaseName("idx_inventory_check_booking_id");

            modelBuilder.Entity<RoomInventoryCheck>()
                .HasIndex(ric => new { ric.CheckDate, ric.Status })
                .HasDatabaseName("idx_inventory_check_date_status");

            // ========== Job Indexes ==========
            modelBuilder.Entity<Job>()
                .HasIndex(j => j.AssignedToId)
                .HasDatabaseName("idx_job_assigned_to_id");

            modelBuilder.Entity<Job>()
                .HasIndex(j => j.BranchId)
                .HasDatabaseName("idx_job_branch_id");

            modelBuilder.Entity<Job>()
                .HasIndex(j => new { j.Status, j.Priority, j.DueDate })
                .HasDatabaseName("idx_job_status_priority_due");

            // ========== Customer Indexes ==========
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .HasDatabaseName("idx_customer_email");

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PhoneNumber)
                .HasDatabaseName("idx_customer_phone");

            // ========== User Indexes ==========
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserStatus)
                .HasDatabaseName("idx_user_status");

            // ========== Audit Log Indexes ==========
            modelBuilder.Entity<AuditLog>()
                .HasIndex(al => al.UserId)
                .HasDatabaseName("idx_audit_log_user_id");

            modelBuilder.Entity<AuditLog>()
                .HasIndex(al => al.Timestamp)
                .HasDatabaseName("idx_audit_log_timestamp");

            modelBuilder.Entity<AuditLog>()
                .HasIndex(al => new { al.EntityType, al.EntityId })
                .HasDatabaseName("idx_audit_log_entity");

            // ========== Notification Indexes ==========
            modelBuilder.Entity<Notification>()
                .HasIndex(n => new { n.UserId, n.IsRead })
                .HasDatabaseName("idx_notification_user_read");
        }

        /// <summary>
        /// Cấu hình unique constraints để đảm bảo tính duy nhất của dữ liệu
        /// </summary>
        private void ConfigureUniqueConstraints(ModelBuilder modelBuilder)
        {
            // Booking Code phải unique
            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.BookingCode)
                .IsUnique()
                .HasDatabaseName("uk_booking_code");

            // Contract Number phải unique
            modelBuilder.Entity<Contract>()
                .HasIndex(c => c.ContractNumber)
                .IsUnique()
                .HasDatabaseName("uk_contract_number");

            // Room Number phải unique trong cùng Branch
            modelBuilder.Entity<Room>()
                .HasIndex(r => new { r.RoomNumber, r.BranchId })
                .IsUnique()
                .HasDatabaseName("uk_room_number_branch");

            // Permission Code phải unique
            modelBuilder.Entity<Permission>()
                .HasIndex(p => p.Code)
                .IsUnique()
                .HasDatabaseName("uk_permission_code");

            // Role Code phải unique
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Code)
                .IsUnique()
                .HasDatabaseName("uk_role_code");

            // ParkSlot Number phải unique trong cùng Branch
            modelBuilder.Entity<ParkSlot>()
                .HasIndex(ps => new { ps.SlotNumber, ps.BranchId })
                .IsUnique()
                .HasDatabaseName("uk_parkslot_number_branch");
        }

        /// <summary>
        /// Cấu hình check constraints để validate dữ liệu ở database level
        /// Tất cả column names sử dụng snake_case để match với PostgreSQL naming convention
        /// </summary>
        private void ConfigureCheckConstraints(ModelBuilder modelBuilder)
        {
            // ========== Booking Constraints ==========

            // Booking: CheckOutDate phải sau CheckInDate
            modelBuilder.Entity<Booking>()
                .ToTable(b => b.HasCheckConstraint(
                    "ck_booking_checkout_after_checkin",
                    "check_out_date > check_in_date"
                ));

            // Booking: NumberOfNights > 0
            modelBuilder.Entity<Booking>()
                .ToTable(b => b.HasCheckConstraint(
                    "ck_booking_nights_positive",
                    "number_of_nights > 0"
                ));

            // Booking: NumberOfGuests > 0
            modelBuilder.Entity<Booking>()
                .ToTable(b => b.HasCheckConstraint(
                    "ck_booking_guests_positive",
                    "number_of_guests > 0"
                ));

            // Booking: RoomRate >= 0
            modelBuilder.Entity<Booking>()
                .ToTable(b => b.HasCheckConstraint(
                    "ck_booking_roomrate_nonnegative",
                    "room_rate >= 0"
                ));

            // Booking: TotalRoomAmount >= 0
            modelBuilder.Entity<Booking>()
                .ToTable(b => b.HasCheckConstraint(
                    "ck_booking_totalroomamount_nonnegative",
                    "total_room_amount >= 0"
                ));

            // ========== Contract Constraints ==========

            // Contract: EndDate phải sau StartDate
            modelBuilder.Entity<Contract>()
                .ToTable(c => c.HasCheckConstraint(
                    "ck_contract_end_after_start",
                    "end_date > start_date"
                ));

            // Contract: GroupSize > 0
            modelBuilder.Entity<Contract>()
                .ToTable(c => c.HasCheckConstraint(
                    "ck_contract_groupsize_positive",
                    "group_size > 0"
                ));

            // Contract: TotalValue >= 0
            modelBuilder.Entity<Contract>()
                .ToTable(c => c.HasCheckConstraint(
                    "ck_contract_totalvalue_nonnegative",
                    "total_value >= 0"
                ));

            // ========== Invoice Constraints ==========

            // Invoice: TotalAmount >= 0
            modelBuilder.Entity<Invoice>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_invoice_totalamount_nonnegative",
                    "total_amount >= 0"
                ));

            // Invoice: PaidAmount >= 0
            modelBuilder.Entity<Invoice>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_invoice_paidamount_nonnegative",
                    "paid_amount >= 0"
                ));

            // Invoice: RoomCharges >= 0
            modelBuilder.Entity<Invoice>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_invoice_roomcharges_nonnegative",
                    "room_charges >= 0"
                ));

            // Invoice: ServiceCharges >= 0
            modelBuilder.Entity<Invoice>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_invoice_servicecharges_nonnegative",
                    "service_charges >= 0"
                ));

            // Invoice: DamageCharges >= 0
            modelBuilder.Entity<Invoice>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_invoice_damagecharges_nonnegative",
                    "damage_charges >= 0"
                ));

            // Invoice: OtherCharges >= 0
            modelBuilder.Entity<Invoice>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_invoice_othercharges_nonnegative",
                    "other_charges >= 0"
                ));

            // Invoice: Discount >= 0
            modelBuilder.Entity<Invoice>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_invoice_discount_nonnegative",
                    "discount >= 0"
                ));

            // Invoice: TaxAmount >= 0
            modelBuilder.Entity<Invoice>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_invoice_taxamount_nonnegative",
                    "tax_amount >= 0"
                ));

            // Invoice: RemainingAmount >= 0
            modelBuilder.Entity<Invoice>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_invoice_remainingamount_nonnegative",
                    "remaining_amount >= 0"
                ));

            // ========== Room Constraints ==========

            // Room: Floor > 0
            modelBuilder.Entity<Room>()
                .ToTable(r => r.HasCheckConstraint(
                    "ck_room_floor_positive",
                    "floor > 0"
                ));

            // ========== RoomCategory Constraints ==========

            // RoomCategory: BasePrice >= 0
            modelBuilder.Entity<RoomCategory>()
                .ToTable(rc => rc.HasCheckConstraint(
                    "ck_roomcategory_baseprice_nonnegative",
                    "base_price >= 0"
                ));

            // RoomCategory: Capacity > 0
            modelBuilder.Entity<RoomCategory>()
                .ToTable(rc => rc.HasCheckConstraint(
                    "ck_roomcategory_capacity_positive",
                    "capacity > 0"
                ));

            // ========== Service Constraints ==========

            // Service: Price >= 0
            modelBuilder.Entity<Service>()
                .ToTable(s => s.HasCheckConstraint(
                    "ck_service_price_nonnegative",
                    "price >= 0"
                ));

            // Service: EstimatedDurationMinutes >= 0 (nếu có giá trị)
            modelBuilder.Entity<Service>()
                .ToTable(s => s.HasCheckConstraint(
                    "ck_service_duration_nonnegative",
                    "estimated_duration_minutes IS NULL OR estimated_duration_minutes >= 0"
                ));

            // Service: MaxCapacity > 0 (nếu có giá trị)
            modelBuilder.Entity<Service>()
                .ToTable(s => s.HasCheckConstraint(
                    "ck_service_maxcapacity_positive",
                    "max_capacity IS NULL OR max_capacity > 0"
                ));

            // ========== ServiceUsage Constraints ==========

            // ServiceUsage: Quantity > 0
            modelBuilder.Entity<ServiceUsage>()
                .ToTable(su => su.HasCheckConstraint(
                    "ck_serviceusage_quantity_positive",
                    "quantity > 0"
                ));

            // ServiceUsage: PricePerUnit >= 0
            modelBuilder.Entity<ServiceUsage>()
                .ToTable(su => su.HasCheckConstraint(
                    "ck_serviceusage_priceperunit_nonnegative",
                    "price_per_unit >= 0"
                ));

            // ServiceUsage: TotalAmount >= 0
            modelBuilder.Entity<ServiceUsage>()
                .ToTable(su => su.HasCheckConstraint(
                    "ck_serviceusage_totalamount_nonnegative",
                    "total_amount >= 0"
                ));

            // ========== RoomInventoryCheckDetail Constraints ==========

            // RoomInventoryCheckDetail: ActualQuantity >= 0 AND ExpectedQuantity >= 0
            modelBuilder.Entity<RoomInventoryCheckDetail>()
                .ToTable(ricd => ricd.HasCheckConstraint(
                    "ck_inventorydetail_quantities_nonnegative",
                    "actual_quantity >= 0 AND expected_quantity >= 0"
                ));

            // RoomInventoryCheckDetail: VarianceQuantity hợp lệ (Actual - Expected)
            modelBuilder.Entity<RoomInventoryCheckDetail>()
                .ToTable(ricd => ricd.HasCheckConstraint(
                    "ck_inventorydetail_variance_valid",
                    "variance_quantity = (actual_quantity - expected_quantity)"
                ));

            // RoomInventoryCheckDetail: DamageCost >= 0 (nếu có)
            modelBuilder.Entity<RoomInventoryCheckDetail>()
                .ToTable(ricd => ricd.HasCheckConstraint(
                    "ck_inventorydetail_damagecost_nonnegative",
                    "damage_cost IS NULL OR damage_cost >= 0"
                ));

            // ========== RoomInventoryTemplate Constraints ==========

            // RoomInventoryTemplate: StandardQuantity > 0
            modelBuilder.Entity<RoomInventoryTemplate>()
                .ToTable(rit => rit.HasCheckConstraint(
                    "ck_inventorytemplate_quantity_positive",
                    "standard_quantity > 0"
                ));

            // RoomInventoryTemplate: ReplacementCost >= 0 (nếu có)
            modelBuilder.Entity<RoomInventoryTemplate>()
                .ToTable(rit => rit.HasCheckConstraint(
                    "ck_inventorytemplate_replacementcost_nonnegative",
                    "replacement_cost IS NULL OR replacement_cost >= 0"
                ));

            // ========== Ingredient Constraints ==========

            // Ingredient: QuantityInStock >= 0
            modelBuilder.Entity<Ingredient>()
                .ToTable(i => i.HasCheckConstraint(
                    "ck_ingredient_quantity_nonnegative",
                    "quantity_in_stock >= 0"
                ));

            // ========== Branch Constraints ==========

            // Branch: TotalRooms >= 0
            modelBuilder.Entity<Branch>()
                .ToTable(b => b.HasCheckConstraint(
                    "ck_branch_totalrooms_nonnegative",
                    "total_rooms >= 0"
                ));

            // Branch: ParkingSlotsCount >= 0
            modelBuilder.Entity<Branch>()
                .ToTable(b => b.HasCheckConstraint(
                    "ck_branch_parkingslots_nonnegative",
                    "parking_slots_count >= 0"
                ));

            // ========== MenuItem Constraints ==========

            // MenuItem: Price >= 0
            modelBuilder.Entity<MenuItem>()
                .ToTable(mi => mi.HasCheckConstraint(
                    "ck_menuitem_price_nonnegative",
                    "price >= 0"
                ));

            // ========== MaintenancePolicy Constraints ==========

            // MaintenancePolicy: IntervalDays > 0
            modelBuilder.Entity<MaintenancePolicy>()
                .ToTable(mp => mp.HasCheckConstraint(
                    "ck_maintenancepolicy_intervaldays_positive",
                    "interval_days > 0"
                ));

            // ========== Customer Constraints ==========

            // Customer: Email format validation (optional - basic check)
            modelBuilder.Entity<Customer>()
                .ToTable(c => c.HasCheckConstraint(
                    "ck_customer_email_format",
                    "email IS NULL OR email ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$'"
                ));

            // ========== RefreshToken Constraints ==========

            // RefreshToken: ExpiredTime phải sau CreateTime
            modelBuilder.Entity<RefreshToken>()
                .ToTable(rt => rt.HasCheckConstraint(
                    "ck_refreshtoken_expiredtime_after_createtime",
                    "expired_time > create_time"
                ));

            // ========== EmailConfirmation Constraints ==========

            // EmailConfirmation: ExpiresAt phải sau RequestedAt
            modelBuilder.Entity<EmailConfirmation>()
                .ToTable(ec => ec.HasCheckConstraint(
                    "ck_emailconfirmation_expiresat_after_requestedat",
                    "expires_at > requested_at"
                ));
        }

        /// <summary>
        /// Cấu hình default values cho các enum và boolean fields
        /// </summary>
        private void ConfigureDefaultValues(ModelBuilder modelBuilder)
        {
            // Booking Status default = Pending
            modelBuilder.Entity<Booking>()
                .Property(b => b.Status)
                .HasDefaultValue(BookingStatus.Pending);

            // Room Status default = Vacant
            modelBuilder.Entity<Room>()
                .Property(r => r.CurrentStatus)
                .HasDefaultValue(RoomStatus.Vacant);

            // Room HasInventoryIssues default = false
            modelBuilder.Entity<Room>()
                .Property(r => r.HasInventoryIssues)
                .HasDefaultValue(false);

            // Invoice Status default = Pending
            modelBuilder.Entity<Invoice>()
                .Property(i => i.Status)
                .HasDefaultValue(InvoiceStatus.Pending);

            // Branch Status default = Active
            modelBuilder.Entity<Branch>()
                .Property(b => b.Status)
                .HasDefaultValue(BranchStatus.Active);

            // Lead Status default = New
            modelBuilder.Entity<Lead>()
                .Property(l => l.Status)
                .HasDefaultValue(LeadStatus.New);

            // Job Status default = New
            modelBuilder.Entity<Job>()
                .Property(j => j.Status)
                .HasDefaultValue(JobStatus.New);

            // Request Status default = New
            modelBuilder.Entity<Request>()
                .Property(r => r.Status)
                .HasDefaultValue(RequestStatus.New);

            // RoomInventoryCheck Status default = InProgress
            modelBuilder.Entity<RoomInventoryCheck>()
                .Property(ric => ric.Status)
                .HasDefaultValue(InventoryCheckStatus.InProgress);

            // Service IsAvailable default = true
            modelBuilder.Entity<Service>()
                .Property(s => s.IsAvailable)
                .HasDefaultValue(true);

            // Service IsChargeable default = true
            modelBuilder.Entity<Service>()
                .Property(s => s.IsChargeable)
                .HasDefaultValue(true);

            // MenuItem IsAvailable default = true
            modelBuilder.Entity<MenuItem>()
                .Property(mi => mi.IsAvailable)
                .HasDefaultValue(true);

            // Template IsActive default = true
            modelBuilder.Entity<Template>()
                .Property(t => t.IsActive)
                .HasDefaultValue(true);

            // RoomInventoryTemplate IsActive default = true
            modelBuilder.Entity<RoomInventoryTemplate>()
                .Property(rit => rit.IsActive)
                .HasDefaultValue(true);

            // Notification IsRead default = false
            modelBuilder.Entity<Notification>()
                .Property(n => n.IsRead)
                .HasDefaultValue(false);

            // ServiceUsage IsCharged default = false
            modelBuilder.Entity<ServiceUsage>()
                .Property(su => su.IsCharged)
                .HasDefaultValue(false);

            // Customer IsGroupCustomer default = false
            modelBuilder.Entity<Customer>()
                .Property(c => c.IsGroupCustomer)
                .HasDefaultValue(false);

            // Attendance IsApproved default = false
            modelBuilder.Entity<Attendance>()
                .Property(a => a.IsApproved)
                .HasDefaultValue(false);
        }

        /// <summary>
        /// Cấu hình global query filter cho soft delete pattern
        /// Tất cả entities kế thừa BaseEntity sẽ tự động filter IsDeleted = false
        /// </summary>
        private void ConfigureSoftDeleteFilter(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

        /// <summary>
        /// Cấu hình các tính năng đặc thù của PostgreSQL
        /// </summary>
        private void ConfigurePostgreSQLFeatures(ModelBuilder modelBuilder)
        {
            // ========== Cấu hình Enum Types cho PostgreSQL ==========
            // PostgreSQL hỗ trợ native enum, mapping tự động từ C# enum

            // ========== Cấu hình JSONB cho PolicyCondition ==========
            modelBuilder.Entity<Permission>()
                .Property(p => p.PolicyCondition)
                .HasColumnType("jsonb");

            // ========== Cấu hình JSONB cho Template Variables ==========
            modelBuilder.Entity<Template>()
                .Property(t => t.Variables)
                .HasColumnType("jsonb");

            // ========== Cấu hình JSONB cho Service OperatingHours ==========
            modelBuilder.Entity<Service>()
                .Property(s => s.OperatingHours)
                .HasColumnType("jsonb");

            // ========== Cấu hình JSONB cho RoomCategory Amenities ==========
            modelBuilder.Entity<RoomCategory>()
                .Property(rc => rc.Amenities)
                .HasColumnType("jsonb");

            // ========== Cấu hình JSONB cho AuditLog Details ==========
            modelBuilder.Entity<AuditLog>()
                .Property(al => al.Details)
                .HasColumnType("jsonb");

            // ========== Cấu hình Full-Text Search cho Customer ==========
            // Tạo generated column cho tìm kiếm full-text
            modelBuilder.Entity<Customer>()
                .HasGeneratedTsVectorColumn(
                    c => c.SearchVector,
                    "english",
                    c => new { c.FullName, c.Email, c.PhoneNumber })
                .HasIndex(c => c.SearchVector)
                .HasMethod("GIN");
        }

        /// <summary>
        /// Seed dữ liệu ban đầu cho hệ thống
        /// </summary>
        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            // ========== Seed Default Roles ==========
            var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var branchManagerRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var receptionistRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            var housekeepingRoleId = Guid.Parse("44444444-4444-4444-4444-444444444444");

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = adminRoleId,
                    Name = "System Administrator",
                    NormalizedName = "SYSTEM ADMINISTRATOR",
                    Code = "ADMIN",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = branchManagerRoleId,
                    Name = "Branch Manager",
                    NormalizedName = "BRANCH MANAGER",
                    Code = "BRANCH_MGR",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = receptionistRoleId,
                    Name = "Receptionist",
                    NormalizedName = "RECEPTIONIST",
                    Code = "RECEPTIONIST",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                },
                new Role
                {
                    Id = housekeepingRoleId,
                    Name = "Housekeeping Staff",
                    NormalizedName = "HOUSEKEEPING STAFF",
                    Code = "HOUSEKEEPING",
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                }
            );

            // ========== Seed Default Permissions ==========
            modelBuilder.Entity<Permission>().HasData(
                // Booking permissions
                new Permission
                {
                    Id = Guid.NewGuid(),
                    Name = "Create Booking",
                    Code = "BOOKING_CREATE",
                    Description = "Quyền tạo mới booking",
                    CreatedAt = DateTime.UtcNow
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    Name = "View Booking",
                    Code = "BOOKING_VIEW",
                    Description = "Quyền xem danh sách booking",
                    CreatedAt = DateTime.UtcNow
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    Name = "Cancel Booking",
                    Code = "BOOKING_CANCEL",
                    Description = "Quyền hủy booking",
                    CreatedAt = DateTime.UtcNow
                },
                // Invoice permissions
                new Permission
                {
                    Id = Guid.NewGuid(),
                    Name = "Create Invoice",
                    Code = "INVOICE_CREATE",
                    Description = "Quyền tạo hóa đơn",
                    CreatedAt = DateTime.UtcNow
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    Name = "Approve Invoice",
                    Code = "INVOICE_APPROVE",
                    Description = "Quyền phê duyệt hóa đơn",
                    CreatedAt = DateTime.UtcNow
                }
                // TODO: Thêm các permissions khác
            );

            // ========== Seed Room Categories ==========
            var standardCategoryId = Guid.NewGuid();
            var deluxeCategoryId = Guid.NewGuid();
            var suiteCategoryId = Guid.NewGuid();

            modelBuilder.Entity<RoomCategory>().HasData(
                new RoomCategory
                {
                    Id = standardCategoryId,
                    Name = "Standard Room",
                    Description = "Phòng tiêu chuẩn với đầy đủ tiện nghi cơ bản",
                    BasePrice = 500000m,
                    Capacity = 2,
                    Amenities = "{\"wifi\": true, \"tv\": true, \"aircon\": true}",
                    CreatedAt = DateTime.UtcNow
                },
                new RoomCategory
                {
                    Id = deluxeCategoryId,
                    Name = "Deluxe Room",
                    Description = "Phòng cao cấp với view đẹp và không gian rộng rãi",
                    BasePrice = 800000m,
                    Capacity = 3,
                    Amenities =
                        "{\"wifi\": true, \"tv\": true, \"aircon\": true, \"minibar\": true, \"bathtub\": true}",
                    CreatedAt = DateTime.UtcNow
                },
                new RoomCategory
                {
                    Id = suiteCategoryId,
                    Name = "Suite Room",
                    Description = "Phòng suite sang trọng với phòng khách riêng biệt",
                    BasePrice = 1500000m,
                    Capacity = 4,
                    Amenities =
                        "{\"wifi\": true, \"tv\": true, \"aircon\": true, \"minibar\": true, \"bathtub\": true, \"kitchen\": true, \"balcony\": true}",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // ========== Seed Inventory Templates ==========
            // Standard Room Template
            modelBuilder.Entity<RoomInventoryTemplate>().HasData(
                new RoomInventoryTemplate
                {
                    Id = Guid.NewGuid(),
                    RoomCategoryId = standardCategoryId,
                    ItemName = "Giường đơn",
                    Description = "Giường đơn 1m2",
                    StandardQuantity = 2,
                    Category = InventoryItemCategory.Furniture,
                    IsConsumable = false,
                    ReplacementCost = 5000000m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new RoomInventoryTemplate
                {
                    Id = Guid.NewGuid(),
                    RoomCategoryId = standardCategoryId,
                    ItemName = "Khăn tắm",
                    Description = "Khăn tắm cotton 70x140cm",
                    StandardQuantity = 4,
                    Category = InventoryItemCategory.Linen,
                    IsConsumable = true,
                    ReplacementCost = 50000m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new RoomInventoryTemplate
                {
                    Id = Guid.NewGuid(),
                    RoomCategoryId = standardCategoryId,
                    ItemName = "TV LCD 32 inch",
                    Description = "Tivi màn hình phẳng 32 inch",
                    StandardQuantity = 1,
                    Category = InventoryItemCategory.Electronics,
                    IsConsumable = false,
                    ReplacementCost = 6000000m,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
                // TODO: Thêm các items khác cho Deluxe và Suite
            );
        }

        #endregion

        #region SaveChanges Override - Audit Trail & Auto-Update

        /// <summary>
        /// Override SaveChanges để tự động thêm audit trail và update timestamps
        /// </summary>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var auditEntries = new List<AuditLog>();
            var currentUserId = GetCurrentUserId(); // Lấy từ IHttpContextAccessor

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                // Auto-update timestamps
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedById = currentUserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedById = currentUserId;
                        // Prevent overwriting CreatedAt and CreatedById
                        entry.Property(nameof(BaseEntity.CreatedAt)).IsModified = false;
                        entry.Property(nameof(BaseEntity.CreatedById)).IsModified = false;
                        break;

                    case EntityState.Deleted:
                        // Implement soft delete
                        if (!entry.Entity.IsDeleted)
                        {
                            entry.State = EntityState.Modified;
                            entry.Entity.IsDeleted = true;
                            entry.Entity.DeletedAt = DateTime.UtcNow;
                            entry.Entity.UpdatedAt = DateTime.UtcNow;
                            entry.Entity.UpdatedById = currentUserId;
                        }

                        break;
                }

                // Create audit log entries
                if (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified ||
                    entry.State == EntityState.Deleted)
                {
                    var auditLog = new AuditLog
                    {
                        Id = Guid.NewGuid(),
                        UserId = currentUserId ?? Guid.Empty,
                        Timestamp = DateTime.UtcNow,
                        Action = entry.State.ToString(),
                        EntityType = entry.Entity.GetType().Name,
                        EntityId = entry.Entity.Id,
                        Details = SerializeEntityChanges(entry),
                        CreatedAt = DateTime.UtcNow
                    };

                    auditEntries.Add(auditLog);
                }
            }

            // Save changes
            var result = await base.SaveChangesAsync(cancellationToken);

            // Save audit logs in separate transaction
            if (auditEntries.Any())
            {
                await AuditLogs.AddRangeAsync(auditEntries, cancellationToken);
                await base.SaveChangesAsync(cancellationToken);
            }

            return result;
        }

        /// <summary>
        /// Serialize entity changes thành JSON cho audit log
        /// </summary>
        private string SerializeEntityChanges(Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entry)
        {
            var changes = new Dictionary<string, object>();

            if (entry.State == EntityState.Added)
            {
                changes["NewValues"] = entry.CurrentValues.ToObject();
            }
            else if (entry.State == EntityState.Modified)
            {
                var originalValues = new Dictionary<string, object>();
                var currentValues = new Dictionary<string, object>();

                foreach (var property in entry.Properties)
                {
                    if (property.IsModified)
                    {
                        originalValues[property.Metadata.Name] = property.OriginalValue;
                        currentValues[property.Metadata.Name] = property.CurrentValue;
                    }
                }

                changes["OriginalValues"] = originalValues;
                changes["CurrentValues"] = currentValues;
            }
            else if (entry.State == EntityState.Deleted)
            {
                changes["DeletedValues"] = entry.OriginalValues.ToObject();
            }

            return System.Text.Json.JsonSerializer.Serialize(changes);
        }

        /// <summary>
        /// Lấy UserId của user hiện tại từ HTTP context
        /// </summary>
        private Guid? GetCurrentUserId()
        {
            // TODO: Implement logic để lấy UserId từ IHttpContextAccessor
            // var httpContext = _httpContextAccessor.HttpContext;
            // if (httpContext?.User?.Identity?.IsAuthenticated == true)
            // {
            //     var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            //     if (Guid.TryParse(userIdClaim?.Value, out var userId))
            //     {
            //         return userId;
            //     }
            // }
            return null;
        }

        #endregion
    }
}