using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pug.Availability
{
	public class CheckResult
	{
		bool available;
		IDictionary<string, string> proofs;

		public CheckResult(bool available, IDictionary<string, string> proofs)
		{
			this.available = available;
			this.proofs = proofs;
		}
		public bool Available
		{
			get
			{
				return available;
			}
		}
		public IDictionary<string, string> Proofs
		{
			get
			{
				return proofs;
			}
		}
	}
}
