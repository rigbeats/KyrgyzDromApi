﻿namespace KDrom.Persistance.Configuration;

public class SmtpOptions
{
	public string Host { get; set; }
	public int Port { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }
	public string SenderName { get; set; }
}
