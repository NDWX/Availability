using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability
{
	public interface ICheckerFactory
	{
		IChecker Create(IDictionary<string, string> parameters);
	}
}
