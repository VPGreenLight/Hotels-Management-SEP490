using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedingdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Existing deletions
            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("0af7aaa0-c958-49b5-8509-74b3e7b20d5d"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("255d0786-e2a9-42cf-8427-0b00f67f4417"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("5c6b8b3a-44f3-44b0-a0c9-c5ceeb3a07b7"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("dce1872d-a7aa-4e8a-9b48-44e6d87efa95"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("efabfa51-d56d-486e-981f-6e028ec23123"));

            migrationBuilder.DeleteData(
                table: "room_categories",
                keyColumn: "id",
                keyValue: new Guid("392ef1b0-ba50-426a-bb6b-9e938b3dee05"));

            migrationBuilder.DeleteData(
                table: "room_categories",
                keyColumn: "id",
                keyValue: new Guid("3a3994ed-22f5-4bb1-a98c-3d89e052d70d"));

            migrationBuilder.DeleteData(
                table: "room_inventory_templates",
                keyColumn: "id",
                keyValue: new Guid("5f5b33c5-2398-4ae9-bc79-146c3a63b4ce"));

            migrationBuilder.DeleteData(
                table: "room_inventory_templates",
                keyColumn: "id",
                keyValue: new Guid("841d998d-bf16-4c4a-a46f-bf31e21381f5"));

            migrationBuilder.DeleteData(
                table: "room_inventory_templates",
                keyColumn: "id",
                keyValue: new Guid("aba0ff20-2f47-44f9-bf16-cab1bb0cfb8e"));

            migrationBuilder.DeleteData(
                table: "room_categories",
                keyColumn: "id",
                keyValue: new Guid("2ba38e93-808d-497e-b2f4-05030e3d8c01"));

            // Existing permission inserts
            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "code", "created_at", "created_by_id", "deleted_at", "description", "is_deleted", "name", "policy_condition", "updated_at", "updated_by_id" },
                values: new object[,]
                {
                    { new Guid("390b0ed1-d267-41ff-bc3b-02b8e14baf19"), "BOOKING_CREATE", new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(3870), null, null, "Quyền tạo mới booking", false, "Create Booking", null, null, null },
                    { new Guid("594b2e55-b031-41aa-822e-82caf927e701"), "BOOKING_VIEW", new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(4012), null, null, "Quyền xem danh sách booking", false, "View Booking", null, null, null },
                    { new Guid("d3688b0f-5ef0-47f4-81b6-3bf69b961896"), "BOOKING_CANCEL", new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(4014), null, null, "Quyền hủy booking", false, "Cancel Booking", null, null, null },
                    { new Guid("ef51120c-8d1a-4e16-b5fb-344b1c45395b"), "INVOICE_APPROVE", new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(4017), null, null, "Quyền phê duyệt hóa đơn", false, "Approve Invoice", null, null, null },
                    { new Guid("fc58c882-58bf-45d7-b31c-e59f6e4f5dcf"), "INVOICE_CREATE", new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(4015), null, null, "Quyền tạo hóa đơn", false, "Create Invoice", null, null, null }
                });

            // Existing role updates
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "concurrency_stamp",
                value: "92c348bc-6dbb-4f5c-a40b-aa0172b488e2");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "concurrency_stamp",
                value: "d6cfabcc-1fc6-4e77-9ff2-6255ba40cf97");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "concurrency_stamp",
                value: "03825bc9-08d8-4908-b457-7f4de1735159");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "concurrency_stamp",
                value: "6d29ea8d-de57-4329-b465-a45b0adb615f");

            // Existing room category inserts
            migrationBuilder.InsertData(
                table: "room_categories",
                columns: new[] { "id", "amenities", "base_price", "capacity", "created_at", "created_by_id", "deleted_at", "description", "is_deleted", "name", "updated_at", "updated_by_id" },
                values: new object[,]
                {
                    { new Guid("29338756-39eb-410f-9944-f0d31fc373e0"), "{\"wifi\": true, \"tv\": true, \"aircon\": true}", 500000m, 2, new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(7575), null, null, "Phòng tiêu chuẩn với đầy đủ tiện nghi cơ bản", false, "Standard Room", null, null },
                    { new Guid("d986b0a6-5284-4cc9-ab6c-961f82552ade"), "{\"wifi\": true, \"tv\": true, \"aircon\": true, \"minibar\": true, \"bathtub\": true}", 800000m, 3, new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(7582), null, null, "Phòng cao cấp với view đẹp và không gian rộng rãi", false, "Deluxe Room", null, null },
                    { new Guid("e29ca073-c5c6-40b1-8c30-7a9e62d375fc"), "{\"wifi\": true, \"tv\": true, \"aircon\": true, \"minibar\": true, \"bathtub\": true, \"kitchen\": true, \"balcony\": true}", 1500000m, 4, new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(7584), null, null, "Phòng suite sang trọng với phòng khách riêng biệt", false, "Suite Room", null, null }
                });

            // Existing room inventory template inserts
            migrationBuilder.InsertData(
                table: "room_inventory_templates",
                columns: new[] { "id", "category", "created_at", "created_by_id", "deleted_at", "description", "image_url", "is_active", "is_consumable", "is_deleted", "item_name", "replacement_cost", "room_category_id", "standard_quantity", "updated_at", "updated_by_id" },
                values: new object[,]
                {
                    { new Guid("26b07d8a-15ae-4e94-a66b-809a5b1028d9"), 2, new DateTime(2025, 10, 9, 14, 13, 11, 135, DateTimeKind.Utc), null, null, "Tivi màn hình phẳng 32 inch", null, true, false, false, "TV LCD 32 inch", 6000000m, new Guid("29338756-39eb-410f-9944-f0d31fc373e0"), 1, null, null },
                    { new Guid("d85046c7-b850-4b3d-9a27-d4c813e1a81d"), 0, new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(9990), null, null, "Giường đơn 1m2", null, true, false, false, "Giường đơn", 5000000m, new Guid("29338756-39eb-410f-9944-f0d31fc373e0"), 2, null, null },
                    { new Guid("ea506987-8bfd-4c0f-8539-d544f3d21b38"), 1, new DateTime(2025, 10, 9, 14, 13, 11, 134, DateTimeKind.Utc).AddTicks(9997), null, null, "Khăn tắm cotton 70x140cm", null, true, true, false, "Khăn tắm", 50000m, new Guid("29338756-39eb-410f-9944-f0d31fc373e0"), 4, null, null }
                });

            // Insert users with static security_stamp and concurrency_stamp
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] 
                { 
                    "id", 
                    "first_name", 
                    "last_name", 
                    "address", 
                    "date_of_birth", 
                    "gender", 
                    "avatar_url", 
                    "user_status", 
                    "user_name", 
                    "normalized_user_name", 
                    "email", 
                    "normalized_email", 
                    "email_confirmed", 
                    "password_hash", 
                    "security_stamp", 
                    "concurrency_stamp", 
                    "phone_number", 
                    "phone_number_confirmed", 
                    "two_factor_enabled", 
                    "lockout_end", 
                    "lockout_enabled", 
                    "access_failed_count" 
                },
                values: new object[,]
                {
                    {
                        new Guid("a1b2c3d4-e5f6-7890-abcd-1234567890ab"),
                        "John",
                        "Doe",
                        "123 Main Street, Hanoi",
                        new DateTime(1990, 5, 15, 0, 0, 0, DateTimeKind.Utc),
                        1, // Male
                        "https://example.com/avatars/john_doe.jpg",
                        1, // Active
                        "johndoe",
                        "JOHNDOE",
                        "john.doe@example.com",
                        "JOHN.DOE@EXAMPLE.COM",
                        true,
                        "AQAAAAEAACcQAAAAEB...==", // Placeholder password hash
                        "f1b2c3d4-e5f6-7890-abcd-1234567890ab", // Static security_stamp
                        "f2b3c4d5-f6a7-8901-bcde-2345678901bc", // Static concurrency_stamp
                        "+84912345678",
                        true,
                        false,
                        null,
                        true,
                        0
                    },
                    {
                        new Guid("b2c3d4e5-f6a7-8901-bcde-2345678901bc"),
                        "Jane",
                        "Smith",
                        "456 Elm Street, Ho Chi Minh City",
                        new DateTime(1985, 8, 22, 0, 0, 0, DateTimeKind.Utc),
                        2, // Female
                        "https://example.com/avatars/jane_smith.jpg",
                        1, // Active
                        "janesmith",
                        "JANESMITH",
                        "jane.smith@example.com",
                        "JANE.SMITH@EXAMPLE.COM",
                        true,
                        "AQAAAAEAACcQAAAAEB...==", // Placeholder password hash
                        "f3c4d5e6-a7b8-9012-cdef-3456789012cd", // Static security_stamp
                        "f4d5e6f7-b8c9-0123-def4-4567890123de", // Static concurrency_stamp
                        "+84987654321",
                        true,
                        false,
                        null,
                        true,
                        0
                    },
                    {
                        new Guid("c3d4e5f6-a7b8-9012-cdef-3456789012cd"),
                        "Admin",
                        "User",
                        "789 Admin Road, Da Nang",
                        new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        1, // Male
                        "https://example.com/avatars/admin_user.jpg",
                        1, // Active
                        "admin",
                        "ADMIN",
                        "admin@example.com",
                        "ADMIN@EXAMPLE.COM",
                        true,
                        "AQAAAAEAACcQAAAAEB...==", // Placeholder password hash
                        "f5e6f7a8-c9d0-1234-ef56-5678901234ef", // Static security_stamp
                        "f6f7a8b9-d0e1-2345-f678-7890123456f0", // Static concurrency_stamp
                        "+84911223344",
                        true,
                        false,
                        null,
                        true,
                        0
                    }
                });

            // Insert user roles
            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "user_id", "role_id" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-1234567890ab"), new Guid("33333333-3333-3333-3333-333333333333") }, // Receptionist
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-2345678901bc"), new Guid("44444444-4444-4444-4444-444444444444") }, // Housekeeping
                    { new Guid("c3d4e5f6-a7b8-9012-cdef-3456789012cd"), new Guid("11111111-1111-1111-1111-111111111111") } // Admin
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Delete user roles
            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "user_id", "role_id" },
                keyValues: new object[] 
                { 
                    new Guid("a1b2c3d4-e5f6-7890-abcd-1234567890ab"), 
                    new Guid("33333333-3333-3333-3333-333333333333") 
                });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "user_id", "role_id" },
                keyValues: new object[] 
                { 
                    new Guid("b2c3d4e5-f6a7-8901-bcde-2345678901bc"), 
                    new Guid("44444444-4444-4444-4444-444444444444") 
                });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "user_id", "role_id" },
                keyValues: new object[] 
                { 
                    new Guid("c3d4e5f6-a7b8-9012-cdef-3456789012cd"), 
                    new Guid("11111111-1111-1111-1111-111111111111") 
                });

            // Delete users
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("a1b2c3d4-e5f6-7890-abcd-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("b2c3d4e5-f6a7-8901-bcde-2345678901bc"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("c3d4e5f6-a7b8-9012-cdef-3456789012cd"));

            // Existing deletions
            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("390b0ed1-d267-41ff-bc3b-02b8e14baf19"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("594b2e55-b031-41aa-822e-82caf927e701"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("d3688b0f-5ef0-47f4-81b6-3bf69b961896"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("ef51120c-8d1a-4e16-b5fb-344b1c45395b"));

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: new Guid("fc58c882-58bf-45d7-b31c-e59f6e4f5dcf"));

            migrationBuilder.DeleteData(
                table: "room_categories",
                keyColumn: "id",
                keyValue: new Guid("d986b0a6-5284-4cc9-ab6c-961f82552ade"));

            migrationBuilder.DeleteData(
                table: "room_categories",
                keyColumn: "id",
                keyValue: new Guid("e29ca073-c5c6-40b1-8c30-7a9e62d375fc"));

            migrationBuilder.DeleteData(
                table: "room_inventory_templates",
                keyColumn: "id",
                keyValue: new Guid("26b07d8a-15ae-4e94-a66b-809a5b1028d9"));

            migrationBuilder.DeleteData(
                table: "room_inventory_templates",
                keyColumn: "id",
                keyValue: new Guid("d85046c7-b850-4b3d-9a27-d4c813e1a81d"));

            migrationBuilder.DeleteData(
                table: "room_inventory_templates",
                keyColumn: "id",
                keyValue: new Guid("ea506987-8bfd-4c0f-8539-d544f3d21b38"));

            migrationBuilder.DeleteData(
                table: "room_categories",
                keyColumn: "id",
                keyValue: new Guid("29338756-39eb-410f-9944-f0d31fc373e0"));

            // Existing permission inserts
            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "code", "created_at", "created_by_id", "deleted_at", "description", "is_deleted", "name", "policy_condition", "updated_at", "updated_by_id" },
                values: new object[,]
                {
                    { new Guid("0af7aaa0-c958-49b5-8509-74b3e7b20d5d"), "BOOKING_VIEW", new DateTime(2025, 10, 6, 15, 54, 35, 651, DateTimeKind.Utc).AddTicks(8058), null, null, "Quyền xem danh sách booking", false, "View Booking", null, null, null },
                    { new Guid("255d0786-e2a9-42cf-8427-0b00f67f4417"), "INVOICE_APPROVE", new DateTime(2025, 10, 6, 15, 54, 35, 651, DateTimeKind.Utc).AddTicks(8063), null, null, "Quyền phê duyệt hóa đơn", false, "Approve Invoice", null, null, null },
                    { new Guid("5c6b8b3a-44f3-44b0-a0c9-c5ceeb3a07b7"), "BOOKING_CREATE", new DateTime(2025, 10, 6, 15, 54, 35, 651, DateTimeKind.Utc).AddTicks(7921), null, null, "Quyền tạo mới booking", false, "Create Booking", null, null, null },
                    { new Guid("dce1872d-a7aa-4e8a-9b48-44e6d87efa95"), "BOOKING_CANCEL", new DateTime(2025, 10, 6, 15, 54, 35, 651, DateTimeKind.Utc).AddTicks(8060), null, null, "Quyền hủy booking", false, "Cancel Booking", null, null, null },
                    { new Guid("efabfa51-d56d-486e-981f-6e028ec23123"), "INVOICE_CREATE", new DateTime(2025, 10, 6, 15, 54, 35, 651, DateTimeKind.Utc).AddTicks(8061), null, null, "Quyền tạo hóa đơn", false, "Create Invoice", null, null, null }
                });

            // Existing role updates
            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "concurrency_stamp",
                value: "62002be3-08da-48e8-b98a-cfc616e2f8be");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "concurrency_stamp",
                value: "601426f9-c4cc-48e2-88d9-cfad38304beb");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "concurrency_stamp",
                value: "dad34b7d-5bb6-40ad-a5fa-6c9b88cf939c");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "concurrency_stamp",
                value: "1ac6a514-9fa8-4367-a99d-2a17a2d9dc45");

            // Existing room category inserts
            migrationBuilder.InsertData(
                table: "room_categories",
                columns: new[] { "id", "amenities", "base_price", "capacity", "created_at", "created_by_id", "deleted_at", "description", "is_deleted", "name", "updated_at", "updated_by_id" },
                values: new object[,]
                {
                    { new Guid("2ba38e93-808d-497e-b2f4-05030e3d8c01"), "{\"wifi\": true, \"tv\": true, \"aircon\": true}", 500000m, 2, new DateTime(2025, 10, 6, 15, 54, 35, 651, DateTimeKind.Utc).AddTicks(9159), null, null, "Phòng tiêu chuẩn với đầy đủ tiện nghi cơ bản", false, "Standard Room", null, null },
                    { new Guid("392ef1b0-ba50-426a-bb6b-9e938b3dee05"), "{\"wifi\": true, \"tv\": true, \"aircon\": true, \"minibar\": true, \"bathtub\": true}", 800000m, 3, new DateTime(2025, 10, 6, 15, 54, 35, 651, DateTimeKind.Utc).AddTicks(9163), null, null, "Phòng cao cấp với view đẹp và không gian rộng rãi", false, "Deluxe Room", null, null },
                    { new Guid("3a3994ed-22f5-4bb1-a98c-3d89e052d70d"), "{\"wifi\": true, \"tv\": true, \"aircon\": true, \"minibar\": true, \"bathtub\": true, \"kitchen\": true, \"balcony\": true}", 1500000m, 4, new DateTime(2025, 10, 6, 15, 54, 35, 651, DateTimeKind.Utc).AddTicks(9164), null, null, "Phòng suite sang trọng với phòng khách riêng biệt", false, "Suite Room", null, null }
                });

            // Existing room inventory template inserts
            migrationBuilder.InsertData(
                table: "room_inventory_templates",
                columns: new[] { "id", "category", "created_at", "created_by_id", "deleted_at", "description", "image_url", "is_active", "is_consumable", "is_deleted", "item_name", "replacement_cost", "room_category_id", "standard_quantity", "updated_at", "updated_by_id" },
                values: new object[,]
                {
                    { new Guid("5f5b33c5-2398-4ae9-bc79-146c3a63b4ce"), 0, new DateTime(2025, 10, 6, 15, 54, 35, 652, DateTimeKind.Utc).AddTicks(722), null, null, "Giường đơn 1m2", null, true, false, false, "Giường đơn", 5000000m, new Guid("2ba38e93-808d-497e-b2f4-05030e3d8c01"), 2, null, null },
                    { new Guid("841d998d-bf16-4c4a-a46f-bf31e21381f5"), 1, new DateTime(2025, 10, 6, 15, 54, 35, 652, DateTimeKind.Utc).AddTicks(728), null, null, "Khăn tắm cotton 70x140cm", null, true, true, false, "Khăn tắm", 50000m, new Guid("2ba38e93-808d-497e-b2f4-05030e3d8c01"), 4, null, null },
                    { new Guid("aba0ff20-2f47-44f9-bf16-cab1bb0cfb8e"), 2, new DateTime(2025, 10, 6, 15, 54, 35, 652, DateTimeKind.Utc).AddTicks(731), null, null, "Tivi màn hình phẳng 32 inch", null, true, false, false, "TV LCD 32 inch", 6000000m, new Guid("2ba38e93-808d-497e-b2f4-05030e3d8c01"), 1, null, null }
                });
        }
    }
}