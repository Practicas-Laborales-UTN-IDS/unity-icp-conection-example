using EdjCase.ICP.Candid.Mapping;
using My.Namespace.MyClient.Models;
using System;

namespace My.Namespace.MyClient.Models
{
	[Variant]
	public class UserResult
	{
		[VariantTagProperty]
		public UserResultTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public UserResult(UserResultTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected UserResult()
		{
		}

		public static UserResult Err(string info)
		{
			return new UserResult(UserResultTag.Err, info);
		}

		public static UserResult Ok(User info)
		{
			return new UserResult(UserResultTag.Ok, info);
		}

		public string AsErr()
		{
			this.ValidateTag(UserResultTag.Err);
			return (string)this.Value!;
		}

		public User AsOk()
		{
			this.ValidateTag(UserResultTag.Ok);
			return (User)this.Value!;
		}

		private void ValidateTag(UserResultTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum UserResultTag
	{
		Err,
		Ok
	}
}