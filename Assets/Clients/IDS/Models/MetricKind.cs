using EdjCase.ICP.Candid.Mapping;
using InsideDarkStudio.Clients.IDS.Models;
using EdjCase.ICP.Candid.Models;
using System;

namespace InsideDarkStudio.Clients.IDS.Models
{
	[Variant]
	public class MetricKind
	{
		[VariantTagProperty]
		public MetricKindTag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public MetricKind(MetricKindTag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected MetricKind()
		{
		}

		public static MetricKind Max(UnboundedInt info)
		{
			return new MetricKind(MetricKindTag.Max, info);
		}

		public static MetricKind Min(UnboundedInt info)
		{
			return new MetricKind(MetricKindTag.Min, info);
		}

		public static MetricKind Replace(UnboundedInt info)
		{
			return new MetricKind(MetricKindTag.Replace, info);
		}

		public static MetricKind Sum(UnboundedInt info)
		{
			return new MetricKind(MetricKindTag.Sum, info);
		}

		public UnboundedInt AsMax()
		{
			this.ValidateTag(MetricKindTag.Max);
			return (UnboundedInt)this.Value!;
		}

		public UnboundedInt AsMin()
		{
			this.ValidateTag(MetricKindTag.Min);
			return (UnboundedInt)this.Value!;
		}

		public UnboundedInt AsReplace()
		{
			this.ValidateTag(MetricKindTag.Replace);
			return (UnboundedInt)this.Value!;
		}

		public UnboundedInt AsSum()
		{
			this.ValidateTag(MetricKindTag.Sum);
			return (UnboundedInt)this.Value!;
		}

		private void ValidateTag(MetricKindTag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}
	}

	public enum MetricKindTag
	{
		Max,
		Min,
		Replace,
		Sum
	}
}