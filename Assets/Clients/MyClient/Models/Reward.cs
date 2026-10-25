using EdjCase.ICP.Candid.Mapping;
using My.Namespace.MyClient.Models;
using EdjCase.ICP.Candid.Models;
using System;

namespace My.Namespace.MyClient.Models
{
	[Variant]
	public class Reward
	{
		[VariantTagProperty]
		public RewardTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Reward(RewardTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Reward()
		{
		}

		public static Reward Asset(UnboundedInt info)
		{
			return new Reward(RewardTag.Asset, info);
		}

		public static Reward Custom(Entry info)
		{
			return new Reward(RewardTag.Custom, info);
		}

		public static Reward Points(UnboundedUInt info)
		{
			return new Reward(RewardTag.Points, info);
		}

		public static Reward Tokens(UnboundedUInt info)
		{
			return new Reward(RewardTag.Tokens, info);
		}

		public UnboundedInt AsAsset()
		{
			this.ValidateTag(RewardTag.Asset);
			return (UnboundedInt)this.Value!;
		}

		public Entry AsCustom()
		{
			this.ValidateTag(RewardTag.Custom);
			return (Entry)this.Value!;
		}

		public UnboundedUInt AsPoints()
		{
			this.ValidateTag(RewardTag.Points);
			return (UnboundedUInt)this.Value!;
		}

		public UnboundedUInt AsTokens()
		{
			this.ValidateTag(RewardTag.Tokens);
			return (UnboundedUInt)this.Value!;
		}

		private void ValidateTag(RewardTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum RewardTag
	{
		Asset,
		Custom,
		Points,
		Tokens
	}
}