﻿namespace HotelManagement.Infrastructure.Email
{
    public interface IEmailService
    {
        Task<IEnumerable<EmailData>> GetEmailsFromSenderAsync(string sender);
        Task SendEmailAsync(string to, string subject, string body);
    }
}