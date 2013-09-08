using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability
{
	public class BasicCheckController : CheckController
	{
		public BasicCheckController(string identifier, IChecker checker, ICollection<INotifyResultFilter> notifyResultFilters, INotifier notifier)
			: base(identifier, checker, notifyResultFilters, notifier)
		{
		}

		public override void Begin()
		{
			base.Check();
		}
	}
}
