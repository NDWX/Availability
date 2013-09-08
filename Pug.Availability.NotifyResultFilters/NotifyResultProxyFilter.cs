using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability.NotifyResultFilters
{
	public class NotifyResultProxyFilter : INotifyResultFilter
	{
		//public class Factory : INotifyResultFilterFactory
		//{
		//    public INotifyResultFilter Create(IDictionary<string, string> parameters)
		//    {
				
		//    }
		//}
		public delegate bool FilterDelegate(CheckResult checkResult);

		Func<CheckResult, bool> filter;

		public NotifyResultProxyFilter(Func<CheckResult, bool> filter)
		{
			this.filter = filter;
		}

		public NotifyResultProxyFilter(FilterDelegate filter)
		{
			this.filter = new Func<CheckResult, bool>(filter);
		}

		public string Identifier
		{
			get { return "NotifyResultProxyFilter"; }
		}

		public bool Evaluate(CheckResult result)
		{
			return filter(result);
		}
	}
}
