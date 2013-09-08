using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability.NotifyResultFilters
{
	public class NotifyResultProxyFilter : INotifyResultFilter
	{
		public delegate bool FilterDelegate(CheckResult checkResult);

		string identifier;
		Func<CheckResult, bool> filter;

		public NotifyResultProxyFilter(string identifier, Func<CheckResult, bool> filter)
		{
			this.identifier = identifier;
			this.filter = filter;
		}

		public NotifyResultProxyFilter(string identifier, FilterDelegate filter)
		{
			this.identifier = identifier;
			this.filter = new Func<CheckResult, bool>(filter);
		}

		public string Identifier
		{
			get { return this.identifier; }
		}

		public bool Evaluate(CheckResult result)
		{
			return filter(result);
		}
	}
}
