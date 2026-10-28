using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Metadata = System.Collections.Generic.List<InsideDarkStudio.Clients.IDS.Models.Entry>;
using GameId = EdjCase.ICP.Candid.Models.UnboundedInt;

namespace InsideDarkStudio.Clients.IDS.Models
{
	public class Game
	{
		[CandidName("coverImg")]
		public List<byte> CoverImg { get; set; }

		[CandidName("description")]
		public string Description { get; set; }

		[CandidName("id")]
		public GameId Id { get; set; }

		[CandidName("metadata")]
		public Metadata Metadata { get; set; }

		[CandidName("name")]
		public string Name { get; set; }

		public Game(List<byte> coverImg, string description, GameId id, Metadata metadata, string name)
		{
			this.CoverImg = coverImg;
			this.Description = description;
			this.Id = id;
			this.Metadata = metadata;
			this.Name = name;
		}

		public Game()
		{
		}
	}
}