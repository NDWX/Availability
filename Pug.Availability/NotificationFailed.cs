using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability
{
	public class NotificationFailed : Exception
	{
		public NotificationFailed(string message) : base(message)
		{
		}
	}
}
