﻿namespace Library.Persistance.Configuration
{
	public class SmtpSettings
	{
		public string Host { get; set; }
		public int Port { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string SenderName { get; set; }
	}
}
