using EdjCase.ICP.Candid.Mapping;
using InsideDarkStudio.Clients.IDS.Models;

namespace InsideDarkStudio.Clients.IDS.Models
{
	public class MetricItem
	{
		[CandidName("name")]
		public string Name { get; set; }

		[CandidName("value")]
		public MetricKind Value { get; set; }

		public MetricItem(string name, MetricKind value)
		{
			this.Name = name;
			this.Value = value;
		}

		public MetricItem()
		{
		}
	}
}