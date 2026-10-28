using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Metadata = System.Collections.Generic.List<InsideDarkStudio.Clients.IDS.Models.Entry>;

namespace InsideDarkStudio.Clients.IDS.Models
{
	public class GameArgs
	{
		[CandidName("coverImg")]
		public List<byte> CoverImg { get; set; }

		[CandidName("description")]
		public string Description { get; set; }

		[CandidName("metadata")]
		public Metadata Metadata { get; set; }

		[CandidName("name")]
		public string Name { get; set; }

		public GameArgs(List<byte> coverImg, string description, Metadata metadata, string name)
		{
			this.CoverImg = coverImg;
			this.Description = description;
			this.Metadata = metadata;
			this.Name = name;
		}

		public GameArgs()
		{
		}
	}
}