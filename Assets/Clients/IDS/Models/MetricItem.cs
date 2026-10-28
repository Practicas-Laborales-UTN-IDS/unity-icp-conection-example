using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;

namespace InsideDarkStudio.Clients.IDS.Models
{
	public class MetricItem
	{
		[CandidName("name")]
		public string Name { get; set; }

		[CandidName("value")]
		public UnboundedInt Value { get; set; }

		public MetricItem(string name, UnboundedInt value)
		{
			this.Name = name;
			this.Value = value;
		}

		public MetricItem()
		{
		}
	}
}