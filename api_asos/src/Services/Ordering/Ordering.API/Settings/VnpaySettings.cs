﻿namespace Ordering.API.Settings;

public class VnpaySettings
{
	public string TmnCode { get; set; }
	public string HashSecret { get; set; }
	public string BaseUrl { get; set; }
	public string Command { get; set; }
	public string CurrCode { get; set; }
	public string Version { get; set; }
	public string Locale { get; set; }
	public string ReturnUrl { get; set; }
	public string TimeZoneId { get; set; }
}
