using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Mail;

namespace Pug.Availability.Notifiers
{
	public class SmtpNotifier : INotifier
	{
		public class Factory : INotifierFactory
		{
			public INotifier Create(IDictionary<string, string> parameters)
			{
				int port = -1;	
				bool useSsl = false;
				string[] receiverEmailAddresses = null;

				if (!parameters.ContainsKey("HOST") || string.IsNullOrEmpty(parameters["HOST"]))
					throw new MissingMemberException("SmtpNotifier", "Host");

				if (!parameters.ContainsKey("PORT") || string.IsNullOrEmpty(parameters["PORT"]))
					throw new MissingMemberException("SmtpNotifier", "Port");

				if (!parameters.ContainsKey("USERNAME") || string.IsNullOrEmpty(parameters["USERNAME"]))
					throw new MissingMemberException("SmtpNotifier", "USERNAME");

				if (!parameters.ContainsKey("PASSWORD") || string.IsNullOrEmpty(parameters["PASSWORD"]))
					throw new MissingMemberException("SmtpNotifier", "PASSWORD");

				if (!parameters.ContainsKey("SENDEREMAILADDRESS") || string.IsNullOrEmpty(parameters["SENDEREMAILADDRESS"]))
					throw new MissingMemberException("SmtpNotifier", "SENDEREMAILADDRESS");

				if (!parameters.ContainsKey("RECEIVEREMAILADDRESSES") || string.IsNullOrEmpty(parameters["RECEIVEREMAILADDRESSES"]))
					throw new MissingMemberException("SmtpNotifier", "RECEIVEREMAILADDRESSES");

				if (!parameters.ContainsKey("SUBJECT") || string.IsNullOrEmpty(parameters["SUBJECT"]))
					throw new MissingMemberException("SmtpNotifier", "SUBJECT");
					
				if (!int.TryParse(parameters["PORT"], out port))
					throw new ArgumentException("SmtpNotifier port is not in valid format.");
					
				if (parameters.ContainsKey("USESSL"))
					if( string.IsNullOrEmpty(parameters["USESSL"]))
						useSsl = true;
					else
						if (!bool.TryParse(parameters["USESSL"], out useSsl))
							throw new ArgumentException("SmtpNotifier USESSL is not in valid format.");

				if (!System.Text.RegularExpressions.Regex.IsMatch(parameters["RECEIVEREMAILADDRESSES"], @"[a-zA-Z0-9._%-]+@([a-zA-Z0-9.-]+\.)+[a-zA-Z]{2,4}(;[a-zA-Z0-9._%-]+@([a-zA-Z0-9.-]+\.)+[a-zA-Z]{2,4})*[;]*"))
					throw new ArgumentException("SmtpNotifier RECEIVEREMAILADDRESSES is not in valid format.");

				receiverEmailAddresses = parameters["RECEIVEREMAILADDRESSES"].Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

				return new SmtpNotifier(parameters["HOST"], port, parameters["USERNAME"], parameters["PASSWORD"], useSsl, parameters["SENDEREMAILADDRESS"], receiverEmailAddresses, parameters["SUBJECT"]);
			}
		}

		string host;
		int port;
		string username, password, senderEmailAddress, subject;
		bool enableSsl;
		ICollection<string> receiverEmailAddresses;
	
		public SmtpNotifier(string host, int port, string username, string password, bool useSsl, string senderEmailAddress, ICollection<string> receiverEmailAddresses, string subject)
		{
			this.host = host;
			this.port = port;
			this.username = username;
			this.password = password;
			this.senderEmailAddress = senderEmailAddress;
			this.subject = subject;
			this.receiverEmailAddresses = receiverEmailAddresses;
			this.enableSsl = useSsl;
		}

		public void Notify(CheckResult checkResult, string configuration)
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
			catch // (Exception exception)
			{
				throw;
			}
			finally
			{
				message.Dispose();
				smtpClient.Dispose();
			}
		}
	}
}
