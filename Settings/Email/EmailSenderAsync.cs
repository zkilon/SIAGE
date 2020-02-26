using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Email
{
	public class EmailSenderAsync
	{
		public static Task SendEmailAsync(string email, string senha)
		{
			try
			{
				Execute(email, senha).Wait();
				return Task.FromResult(0);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static async Task Execute(string email, string senha)
		{
			try
			{
				string toEmail = email;

				MailMessage mail = new MailMessage()
				{
					From = new MailAddress(EmailSettings.UsernameEmail, "Sistema de Envio de Email")
				};

				mail.To.Add(new MailAddress(toEmail));

				mail.Subject = "Sistema de Amostras";
				StringBuilder sb = new StringBuilder();

				sb.AppendLine(@"Olá, Bem Vindo ao Sistema de Amostras");
				sb.AppendLine();
				sb.AppendLine("Seus dados de acesso são: ");
				sb.AppendLine();
				sb.AppendLine(string.Concat("Seu Email: ", toEmail));
				sb.AppendLine(string.Concat("Sua Senha: ", senha));

				mail.Body = sb.ToString();
				mail.IsBodyHtml = false;
				mail.Priority = MailPriority.High;

				using (SmtpClient smtp = new SmtpClient(EmailSettings.PrimaryDomain, EmailSettings.PrimaryPort))
				{
					smtp.Credentials = new NetworkCredential(EmailSettings.UsernameEmail, EmailSettings.UserPassword);
					smtp.EnableSsl = true;
					await smtp.SendMailAsync(mail);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
