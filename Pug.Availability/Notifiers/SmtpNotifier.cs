using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Mail;

namespace Pug.Availability.Notifiers
{
	public class SmtpNotifier : INotifier
	{
		string host;
		int port;
		string username, password, senderEmailAddress, subject;
		bool enableSsl;
		ICollection<string> receiverEmailAddresses;

		public SmtpNotifier(string host, int port, string username, string password, bool useSsl, string senderEmailAddress, ICollection<string> receiverEmailAddresses)
		{
			this.host = host;
			this.port = port;
			this.username = username;
			this.password = password;
			this.senderEmailAddress = senderEmailAddress;
			//this.subject = subject;
			this.receiverEmailAddresses = receiverEmailAddresses;
			this.enableSsl = useSsl;
		}

		public void Notify(CheckResult checkResult, string configuration, string subject)
		{
			SmtpClient smtpClient = new SmtpClient(host, port);
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
			smtpClient.EnableSsl = enableSsl;

			MailMessage message = new MailMessage(senderEmailAddress, receiverEmailAddresses.First());

			message.Subject = subject;

			foreach (string emailAddress in receiverEmailAddresses.Skip(1))
			{
				message.To.Add(new MailAddress(emailAddress));
			}

			StringBuilder messageBody = new StringBuilder();

			messageBody.AppendFormat("Configuration : {0}", configuration);
			messageBody.AppendLine();
			messageBody.AppendFormat("Availability: {0}", checkResult.Available);
			messageBody.AppendLine();
			messageBody.AppendLine("Proofs :");
			messageBody.AppendLine();

			foreach( KeyValuePair<string, string> proof in checkResult.Proofs )
				messageBody.AppendFormat("{0} : {1}", proof.Key, proof.Value);

			message.Body = messageBody.ToString();

			try
			{
				smtpClient.Send(message);
			}
			catch(Exception exception)
			{
				throw;
			}
		}
	}
}
