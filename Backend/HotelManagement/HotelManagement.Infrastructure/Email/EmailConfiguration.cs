﻿namespace HotelManagement.Infrastructure.Email
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string TrackingEmail { get; set; }
        public string TrackingPassword { get; set; }
    }
}
