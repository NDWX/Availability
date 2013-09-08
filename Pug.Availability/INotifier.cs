using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability
{
	public interface INotifier
	{
		void Notify(CheckResult checkResult, string configuration);
	}

}
