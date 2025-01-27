
using IKIEA.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials = new NetworkCredential("aliaatarek.route@gmail.com", "xsuvcmyketquafsj");
			Client.Send("aliaatarek.route@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}