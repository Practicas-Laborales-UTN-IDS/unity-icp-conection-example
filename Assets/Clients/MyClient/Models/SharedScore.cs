using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using Metadata = System.Collections.Generic.List<My.Namespace.MyClient.Models.Entry>;

namespace My.Namespace.MyClient.Models
{
	public class SharedScore
	{
		[CandidName("gamesPlayed")]
		public UnboundedUInt GamesPlayed { get; set; }

		[CandidName("lastPlayedAt")]
		public UnboundedInt LastPlayedAt { get; set; }

		[CandidName("metadata")]
		public Metadata Metadata { get; set; }

		[CandidName("totalPoints")]
		public UnboundedUInt TotalPoints { get; set; }

		public SharedScore(UnboundedUInt gamesPlayed, UnboundedInt lastPlayedAt, Metadata metadata, UnboundedUInt totalPoints)
		{
			this.GamesPlayed = gamesPlayed;
			this.LastPlayedAt = lastPlayedAt;
			this.Metadata = metadata;
			this.TotalPoints = totalPoints;
		}

		public SharedScore()
		{
		}
	}
}