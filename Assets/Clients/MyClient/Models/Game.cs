using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using Metadata = System.Collections.Generic.List<My.Namespace.MyClient.Models.Entry>;
using GameId = EdjCase.ICP.Candid.Models.UnboundedInt;

namespace My.Namespace.MyClient.Models
{
	public class Game
	{
		[CandidName("config")]
		public Metadata Config { get; set; }

		[CandidName("coverImg")]
		public List<byte> CoverImg { get; set; }

		[CandidName("description")]
		public string Description { get; set; }

		[CandidName("id")]
		public GameId Id { get; set; }

		[CandidName("name")]
		public string Name { get; set; }

		public Game(Metadata config, List<byte> coverImg, string description, GameId id, string name)
		{
			this.Config = config;
			this.CoverImg = coverImg;
			this.Description = description;
			this.Id = id;
			this.Name = name;
		}

		public Game()
		{
		}
	}
}