using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability.NotifyResultFilters
{
	public class BasicNotifyResultFilter : INotifyResultFilter
	{
		public class Factory : INotifyResultFilterFactory
		{
			public INotifyResultFilter Create(IDictionary<string, string> parameters)
			{
				return new BasicNotifyResultFilter();
			}
		}

		public string Identifier
		{
			get { return "BasicNotifyResultFilter"; }
		}

		public bool Evaluate(CheckResult result)
		{
			return !result.Available;
		}
	}
}