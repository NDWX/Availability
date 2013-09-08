using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability
{
	public interface INotifyResultFilter
	{
		string Identifier
		{
			get;
		}

		bool Evaluate(CheckResult result);
	}
}