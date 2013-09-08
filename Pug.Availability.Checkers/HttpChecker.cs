using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;

namespace Pug.Availability.Checkers
{
	public class HttpChecker : IChecker
	{
		public class Factory : ICheckerFactory
		{
			public IChecker Create(IDictionary<string, string> parameters)
			{
				Uri url;

				if (!parameters.ContainsKey("URL") || string.IsNullOrEmpty(parameters["URL"]))
					throw new MissingMemberException("HttpChecker", "Url");

				url = new Uri(parameters["URL"]);

				if (!(url.Scheme.ToUpper() == "HTTP" || url.Scheme.ToUpper() == "HTTPS"))
					throw new ArgumentException("HttpChecker only support HTTP and HTTPS");

				if (!(url.HostNameType == UriHostNameType.Dns || url.HostNameType == UriHostNameType.IPv4 || url.HostNameType == UriHostNameType.IPv6))
					throw new ArgumentException("HttpChecker.Url hostname not in known format");

				return new HttpChecker(url);
			}
		}

		Uri url;

		public HttpChecker(Uri url) 
		{
			this.url = url;
		}

		public CheckResult Check()
		{
			bool available = false;
			Dictionary<string, string> proof = new Dictionary<string, string>();

			IPAddress hostIPAddress = null;

			if( url.HostNameType == UriHostNameType.Dns )
			{
				IPAddress[] ipAddresses = null;

				try
				{
					ipAddresses = Dns.GetHostAddresses(url.Host);
				}
				catch (SocketException socketException)
				{
					available = false;
					proof.Add("Error.Message", socketException.Message);

					return new CheckResult(available, proof);
				}

				if (ipAddresses == null || ipAddresses.Length == 0)
				{
					return new CheckResult(available, proof);
				}
				else
				{
					hostIPAddress = ipAddresses[0];
					proof.Add("Host.IP.Address", hostIPAddress.ToString());
				}

			}
			
			WebRequest webRequest = HttpWebRequest.Create(url);

			WebResponse webResponse = null;

			DateTime startTimestamp = DateTime.Now, endTimestamp;

			try
			{
				startTimestamp = DateTime.Now;

				webResponse = webRequest.GetResponse();

				HttpWebResponse httpResponse = (HttpWebResponse)webResponse;
				proof.Add("HttpResponse.StatusCode", ((int)httpResponse.StatusCode).ToString());
				proof.Add("HttpResponse.ContentLength", httpResponse.ContentLength.ToString());
				proof.Add("HttpResponse.ContentType", httpResponse.ContentType);
				proof.Add("HttpResponse.Server", httpResponse.Server);
				proof.Add("HttpResponse.ResponseUri", httpResponse.ResponseUri.AbsoluteUri);

				available = true;
			}
			catch (WebException webException)
			{
				available = false;
				proof.Add("Error.Message", webException.Message);
			}
			finally
			{
				endTimestamp = DateTime.Now;

				if (webResponse != null)
					webResponse.Close();
			}

			proof.Add("Start.Timestamp", startTimestamp.ToString("s"));
			proof.Add("End.Timestamp", endTimestamp.ToString("s"));

			return new CheckResult(available, proof);
		}
	}
}
