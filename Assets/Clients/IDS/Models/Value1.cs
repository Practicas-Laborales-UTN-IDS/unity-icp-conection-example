using EdjCase.ICP.Candid.Mapping;
using InsideDarkStudio.Clients.IDS.Models;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Models;
using System;

namespace InsideDarkStudio.Clients.IDS.Models
{
	[Variant]
	public class Value1
	{
		[VariantTagProperty]
		public Value1Tag Tag { get; set; }

		[VariantValueProperty]
		public object? Value { get; set; }

		public Value1(Value1Tag tag, object? value)
		{
			this.Tag = tag;
			this.Value = value;
		}

		protected Value1()
		{
		}

		public static Value1 Array(Value1.ArrayInfo info)
		{
			return new Value1(Value1Tag.Array, info);
		}

		public static Value1 Blob(List<byte> info)
		{
			return new Value1(Value1Tag.Blob, info);
		}

		public static Value1 Int(UnboundedInt info)
		{
			return new Value1(Value1Tag.Int, info);
		}

		public static Value1 Map(Value1.MapInfo info)
		{
			return new Value1(Value1Tag.Map, info);
		}

		public static Value1 Nat(UnboundedUInt info)
		{
			return new Value1(Value1Tag.Nat, info);
		}

		public static Value1 Text(string info)
		{
			return new Value1(Value1Tag.Text, info);
		}

		public Value1.ArrayInfo AsArray()
		{
			this.ValidateTag(Value1Tag.Array);
			return (Value1.ArrayInfo)this.Value!;
		}

		public List<byte> AsBlob()
		{
			this.ValidateTag(Value1Tag.Blob);
			return (List<byte>)this.Value!;
		}

		public UnboundedInt AsInt()
		{
			this.ValidateTag(Value1Tag.Int);
			return (UnboundedInt)this.Value!;
		}

		public Value1.MapInfo AsMap()
		{
			this.ValidateTag(Value1Tag.Map);
			return (Value1.MapInfo)this.Value!;
		}

		public UnboundedUInt AsNat()
		{
			this.ValidateTag(Value1Tag.Nat);
			return (UnboundedUInt)this.Value!;
		}

		public string AsText()
		{
			this.ValidateTag(Value1Tag.Text);
			return (string)this.Value!;
		}

		private void ValidateTag(Value1Tag tag)
		{
			if (!this.Tag.Equals(tag))
			{
				throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
			}
		}

		public class ArrayInfo : List<Value1>
		{
			public ArrayInfo()
			{
			}
		}

		public class MapInfo : List<Value1.MapInfo.MapInfoElement>
		{
			public MapInfo()
			{
			}

			public class MapInfoElement
			{
				[CandidTag(0U)]
				public string F0 { get; set; }

				[CandidTag(1U)]
				public Value1 F1 { get; set; }

				public MapInfoElement(string f0, Value1 f1)
				{
					this.F0 = f0;
					this.F1 = f1;
				}

				public MapInfoElement()
				{
				}
			}
		}
	}

	public enum Value1Tag
	{
		Array,
		Blob,
		Int,
		Map,
		Nat,
		Text
	}
}