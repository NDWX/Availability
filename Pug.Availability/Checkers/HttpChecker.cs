using System;
using System.Collections.Generic;
using System.Text;

using System.Net;

namespace Pug.Availability.Checkers
{
	public class HttpChecker : IChecker
	{
		string host;
		int port;

		public HttpChecker(string host, int port)
		{
			this.host = host;
			this.port = port;
		}

		public CheckResult Check()
		{
			bool available = false;
			Dictionary<string, string> proof = new Dictionary<string, string>();

			IPAddress hostIPAddress = null;

			bool hostIsAddress = IPAddress.TryParse(host, out hostIPAddress);

			if( !hostIsAddress )
			{
				IPAddress[] ipAddresses;

				ipAddresses = Dns.GetHostAddresses(host);

				if (ipAddresses.Length == 0)
				{
					throw new Exception("Unable to resolve host address");
				}

				hostIPAddress = ipAddresses[0];
				proof.Add("Host.IP.Address", hostIPAddress.ToString());
			}
			
			WebRequest webRequest = HttpWebRequest.Create(string.Format("http://{0}/", host));

			WebResponse webResponse = null;

			try
			{
				webResponse = webRequest.GetResponse();

				HttpWebResponse httpResponse = (HttpWebResponse)webResponse;
				proof.Add("HttpResponse.StatusCode", ((int)httpResponse.StatusCode).ToString());
				proof.Add("HttpResponse.ContentLength", httpResponse.ContentLength.ToString());
				proof.Add("HttpResponse.ContentType", httpResponse.ContentType);
				proof.Add("HttpResponse.Server", httpResponse.Server);
				proof.Add("HttpResponse.ResponseUri", httpResponse.ResponseUri.AbsoluteUri);
			}
			catch (WebException webException)
			{
				available = false;
			}
			finally
			{
				if (webResponse != null)
					webResponse.Close();
			}

			return new CheckResult(available, proof);
		}
	}
}
