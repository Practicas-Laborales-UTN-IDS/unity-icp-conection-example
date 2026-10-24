using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;

namespace My.Namespace.MyClient.Models
{
	public class SharedInventory
	{
		[CandidName("assetsID")]
		public List<UnboundedInt> AssetsID { get; set; }

		[CandidName("level")]
		public UnboundedUInt Level { get; set; }

		[CandidName("points")]
		public UnboundedUInt Points { get; set; }

		public SharedInventory(List<UnboundedInt> assetsID, UnboundedUInt level, UnboundedUInt points)
		{
			this.AssetsID = assetsID;
			this.Level = level;
			this.Points = points;
		}

		public SharedInventory()
		{
		}
	}
}