using EdjCase.ICP.Candid.Mapping;
using My.Namespace.MyClient.Models;
using Key = System.String;

namespace My.Namespace.MyClient.Models
{
	public class Entry
	{
		[CandidTag(0U)]
		public Key F0 { get; set; }

		[CandidTag(1U)]
		public Value1 F1 { get; set; }

		public Entry(Key f0, Value1 f1)
		{
			this.F0 = f0;
			this.F1 = f1;
		}

		public Entry()
		{
		}
	}
}