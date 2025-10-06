using System.ComponentModel.DataAnnotations;
using HotelManagement.Domain.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Domain.Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [MaxLength(100)]
        public required string LastName { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        [MaxLength(2048)]
        public string? AvatarUrl { get; set; }

        public UserStatus UserStatus { get; set; }

        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<EmailConfirmation>? ConfirmEmails { get; set; }
        public virtual ICollection<Booking>? ReceptionistBookings { get; set; }
        public virtual ICollection<Job>? AssignedJobs { get; set; }
        public virtual ICollection<Attendance>? Attendances { get; set; }
        public virtual ICollection<Lead>? AssignedLeads { get; set; }
        public virtual ICollection<ServiceUsage>? RecordedServiceUsages { get; set; }
    }
}
