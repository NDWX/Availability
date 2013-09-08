using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability
{
	public interface INotifierFactory
	{
		INotifier Create(IDictionary<string, string> parameters);
	}
}
