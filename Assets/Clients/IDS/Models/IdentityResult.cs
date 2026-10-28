using EdjCase.ICP.Candid.Mapping;
using InsideDarkStudio.Clients.IDS.Models;
using EdjCase.ICP.Candid.Models;
using System;

namespace InsideDarkStudio.Clients.IDS.Models
{
	[Variant]
	public class IdentityResult
	{
		[VariantTagProperty]
		public IdentityResultTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public IdentityResult(IdentityResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected IdentityResult()
		{
		}

		public static IdentityResult External(Principal info)
		{
			return new IdentityResult(IdentityResultTag.External, info);
		}

		public static IdentityResult Internal(User info)
		{
			return new IdentityResult(IdentityResultTag.Internal, info);
		}

		public Principal AsExternal()
		{
			this.ValidateTag(IdentityResultTag.External);
			return (Principal)this.Value!;
		}

		public User AsInternal()
		{
			this.ValidateTag(IdentityResultTag.Internal);
			return (User)this.Value!;
		}

		private void ValidateTag(IdentityResultTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum IdentityResultTag
	{
		External,
		Internal
	}
}