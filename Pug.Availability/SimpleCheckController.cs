using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability
{
	public class SimpleCheckController
	{
		string identifier;
		IChecker checker;
		ICollection<INotifyResultFilter> notifyResultFilters;
		INotifier notifier;

		protected IList<INotifyResultFilter> passedFilters;
		CheckResult result;

		public SimpleCheckController(string identifier, IChecker checker, ICollection<INotifyResultFilter> notifyResultFilters, INotifier notifier)
		{
			this.identifier = identifier;
			this.checker = checker;
			this.notifyResultFilters = notifyResultFilters;
			this.notifier = notifier;

			passedFilters = new List<INotifyResultFilter>();
		}

		public string Identifier
		{
			get { return identifier; }
			protected set { identifier = value; }
		}

		public IChecker Checker
		{
			get { return checker; }
			protected set { checker = value; }
		}

		public ICollection<INotifyResultFilter> NotifyResultFilters
		{
			get { return notifyResultFilters; }
			protected set { notifyResultFilters = value; }
		}

		public INotifier Notifier
		{
			get { return notifier; }
			protected set { notifier = value; }
		}

		public virtual void Begin()
		{
			result = checker.Check();

			if (notifyResultFilters != null)
				foreach (INotifyResultFilter filter in notifyResultFilters)
					if (filter.Evaluate(result))
						passedFilters.Add(filter);
					else
						return;

			if (notifier != null)
				notifier.Notify(result, identifier);
		}

		public ICollection<INotifyResultFilter> PassedFilters
		{
			get
			{
				return passedFilters;
			}
		}

		public CheckResult Result
		{
			get
			{
				return result;
			}
			protected set
			{
				this.result = value;
			}
		}
	}
}