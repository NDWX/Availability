using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability.NotifyResultFilters
{
	public class NotifyResultNegativeFilter : INotifyResultFilter
	{
		public class Factory : INotifyResultFilterFactory
		{
			public INotifyResultFilter Create(IDictionary<string, string> parameters)
			{
				return new NotifyResultNegativeFilter();
			}
		}

		public string Identifier
		{
			get { return "NotifyResultNegativeFilter"; }
		}

		public bool Evaluate(CheckResult result)
		{
			return result.Available;
		}
	}
}
