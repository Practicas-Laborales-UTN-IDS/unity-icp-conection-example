using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid;
using System.Threading.Tasks;
using My.Namespace.MyClient;
using EdjCase.ICP.Agent.Responses;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using System;
using Metadata = System.Collections.Generic.List<My.Namespace.MyClient.Models.Entry>;

namespace My.Namespace.MyClient
{
	public class MyClientApiClient
	{
		public IAgent Agent { get; }
		public Principal CanisterId { get; }
		public CandidConverter? Converter { get; }

		public MyClientApiClient(IAgent agent, Principal canisterId, CandidConverter? converter = default)
		{
			this.Agent = agent;
			this.CanisterId = canisterId;
			this.Converter = converter;
		}

		public async Task<MyClientApiClient.AddAdminReturnArg0> AddAdmin(Principal p)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(p, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "addAdmin", arg);
			return reply.ToObjects<MyClientApiClient.AddAdminReturnArg0>(this.Converter);
		}

		public async Task<UnboundedInt> AddGame(MyClientApiClient.AddGameArg0 arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "addGame", arg);
			return reply.ToObjects<UnboundedInt>(this.Converter);
		}

		public async Task<MyClientApiClient.AddSuperAdminReturnArg0> AddSuperAdmin(Principal p)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(p, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "addSuperAdmin", arg);
			return reply.ToObjects<MyClientApiClient.AddSuperAdminReturnArg0>(this.Converter);
		}

		public async Task<UnboundedUInt> Balance()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "balance", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<UnboundedUInt>(this.Converter);
		}

		public async Task<Models.UserResult> EditUser(Metadata data)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(data, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "editUser", arg);
			return reply.ToObjects<Models.UserResult>(this.Converter);
		}

		public async Task<MyClientApiClient.EndGameReturnArg0> EndGame(UnboundedInt gameId, List<byte> payload, List<byte> proof)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(gameId, this.Converter), CandidTypedValue.FromObject(payload, this.Converter), CandidTypedValue.FromObject(proof, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "endGame", arg);
			return reply.ToObjects<MyClientApiClient.EndGameReturnArg0>(this.Converter);
		}

		public async Task<List<Models.User>> GetAdmins()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getAdmins", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<Models.User>>(this.Converter);
		}

		public async Task<List<Models.Game>> GetGames()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getGames", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<Models.Game>>(this.Converter);
		}

		public async Task<Models.SharedInventory> GetInventory()
		{
			CandidArg arg = CandidArg.FromCandid();
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "getInventory", arg);
			return reply.ToObjects<Models.SharedInventory>(this.Converter);
		}

		public async Task<List<Models.IdentityResult>> GetSuperAdmins()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getSuperAdmins", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<Models.IdentityResult>>(this.Converter);
		}

		public async Task<Models.PaginatedUsers> GetUsers(MyClientApiClient.GetUsersArg0 arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0, this.Converter));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getUsers", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Models.PaginatedUsers>(this.Converter);
		}

		public async Task<OptionalValue<Models.UserType>> Login()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "login", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<OptionalValue<Models.UserType>>(this.Converter);
		}

		public async Task<Metadata> Metrics()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "metrics", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Metadata>(this.Converter);
		}

		public async Task<MyClientApiClient.RemoveAdminReturnArg0> RemoveAdmin(Principal p)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(p, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "removeAdmin", arg);
			return reply.ToObjects<MyClientApiClient.RemoveAdminReturnArg0>(this.Converter);
		}

		public async Task<MyClientApiClient.RemoveSuperAdminReturnArg0> RemoveSuperAdmin(Principal p)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(p, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "removeSuperAdmin", arg);
			return reply.ToObjects<MyClientApiClient.RemoveSuperAdminReturnArg0>(this.Converter);
		}

		public async Task<Models.UserResult> RenameUser(string name)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(name, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "renameUser", arg);
			return reply.ToObjects<Models.UserResult>(this.Converter);
		}

		public async Task SetUserPageSize(UnboundedUInt pageSize)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(pageSize, this.Converter));
			await this.Agent.CallAsync(this.CanisterId, "setUserPageSize", arg);
		}

		public async Task<Models.UserResult> SignUp(string name)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(name, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "signUp", arg);
			return reply.ToObjects<Models.UserResult>(this.Converter);
		}

		public async Task<MyClientApiClient.StartSessionGameReturnArg0> StartSessionGame(UnboundedInt gameId)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(gameId, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "startSessionGame", arg);
			return reply.ToObjects<MyClientApiClient.StartSessionGameReturnArg0>(this.Converter);
		}

		public async Task<Models.UserResult> UpdateAvatar(OptionalValue<List<byte>> avatar)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(avatar, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "updateAvatar", arg);
			return reply.ToObjects<Models.UserResult>(this.Converter);
		}

		public async Task<Principal> WhoAmi()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "whoAmi", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Principal>(this.Converter);
		}

		[Variant]
		public class AddAdminReturnArg0
		{
			[VariantTagProperty]
			public MyClientApiClient.AddAdminReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public AddAdminReturnArg0(MyClientApiClient.AddAdminReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected AddAdminReturnArg0()
			{
			}

			public static MyClientApiClient.AddAdminReturnArg0 Err(string info)
			{
				return new MyClientApiClient.AddAdminReturnArg0(MyClientApiClient.AddAdminReturnArg0Tag.Err, info);
			}

			public static MyClientApiClient.AddAdminReturnArg0 Ok()
			{
				return new MyClientApiClient.AddAdminReturnArg0(MyClientApiClient.AddAdminReturnArg0Tag.Ok, null);
			}

			public string AsErr()
			{
				this.ValidateTag(MyClientApiClient.AddAdminReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			private void ValidateTag(MyClientApiClient.AddAdminReturnArg0Tag tag)
			{
				if (!this.Tag.Equals(tag))
				{
					throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
				}
			}
		}

		public enum AddAdminReturnArg0Tag
		{
			Err,
			Ok
		}

		public class AddGameArg0
		{
			[CandidName("config")]
			public Metadata Config { get; set; }

			[CandidName("coverImg")]
			public List<byte> CoverImg { get; set; }

			[CandidName("description")]
			public string Description { get; set; }

			[CandidName("name")]
			public string Name { get; set; }

			public AddGameArg0(Metadata config, List<byte> coverImg, string description, string name)
			{
				this.Config = config;
				this.CoverImg = coverImg;
				this.Description = description;
				this.Name = name;
			}

			public AddGameArg0()
			{
			}
		}

		[Variant]
		public class AddSuperAdminReturnArg0
		{
			[VariantTagProperty]
			public MyClientApiClient.AddSuperAdminReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public AddSuperAdminReturnArg0(MyClientApiClient.AddSuperAdminReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected AddSuperAdminReturnArg0()
			{
			}

			public static MyClientApiClient.AddSuperAdminReturnArg0 Err(string info)
			{
				return new MyClientApiClient.AddSuperAdminReturnArg0(MyClientApiClient.AddSuperAdminReturnArg0Tag.Err, info);
			}

			public static MyClientApiClient.AddSuperAdminReturnArg0 Ok()
			{
				return new MyClientApiClient.AddSuperAdminReturnArg0(MyClientApiClient.AddSuperAdminReturnArg0Tag.Ok, null);
			}

			public string AsErr()
			{
				this.ValidateTag(MyClientApiClient.AddSuperAdminReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			private void ValidateTag(MyClientApiClient.AddSuperAdminReturnArg0Tag tag)
			{
				if (!this.Tag.Equals(tag))
				{
					throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
				}
			}
		}

		public enum AddSuperAdminReturnArg0Tag
		{
			Err,
			Ok
		}

		[Variant]
		public class EndGameReturnArg0
		{
			[VariantTagProperty]
			public MyClientApiClient.EndGameReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public EndGameReturnArg0(MyClientApiClient.EndGameReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected EndGameReturnArg0()
			{
			}

			public static MyClientApiClient.EndGameReturnArg0 Err(string info)
			{
				return new MyClientApiClient.EndGameReturnArg0(MyClientApiClient.EndGameReturnArg0Tag.Err, info);
			}

			public static MyClientApiClient.EndGameReturnArg0 Ok(Models.Reward info)
			{
				return new MyClientApiClient.EndGameReturnArg0(MyClientApiClient.EndGameReturnArg0Tag.Ok, info);
			}

			public string AsErr()
			{
				this.ValidateTag(MyClientApiClient.EndGameReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			public Models.Reward AsOk()
			{
				this.ValidateTag(MyClientApiClient.EndGameReturnArg0Tag.Ok);
				return (Models.Reward)this.Value!;
			}

			private void ValidateTag(MyClientApiClient.EndGameReturnArg0Tag tag)
			{
				if (!this.Tag.Equals(tag))
				{
					throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
				}
			}
		}

		public enum EndGameReturnArg0Tag
		{
			Err,
			Ok
		}

		public class GetUsersArg0
		{
			[CandidName("page")]
			public UnboundedUInt Page { get; set; }

			[CandidName("pageSize")]
			public OptionalValue<UnboundedUInt> PageSize { get; set; }

			public GetUsersArg0(UnboundedUInt page, OptionalValue<UnboundedUInt> pageSize)
			{
				this.Page = page;
				this.PageSize = pageSize;
			}

			public GetUsersArg0()
			{
			}
		}

		[Variant]
		public class RemoveAdminReturnArg0
		{
			[VariantTagProperty]
			public MyClientApiClient.RemoveAdminReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public RemoveAdminReturnArg0(MyClientApiClient.RemoveAdminReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected RemoveAdminReturnArg0()
			{
			}

			public static MyClientApiClient.RemoveAdminReturnArg0 Err(string info)
			{
				return new MyClientApiClient.RemoveAdminReturnArg0(MyClientApiClient.RemoveAdminReturnArg0Tag.Err, info);
			}

			public static MyClientApiClient.RemoveAdminReturnArg0 Ok()
			{
				return new MyClientApiClient.RemoveAdminReturnArg0(MyClientApiClient.RemoveAdminReturnArg0Tag.Ok, null);
			}

			public string AsErr()
			{
				this.ValidateTag(MyClientApiClient.RemoveAdminReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			private void ValidateTag(MyClientApiClient.RemoveAdminReturnArg0Tag tag)
			{
				if (!this.Tag.Equals(tag))
				{
					throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
				}
			}
		}

		public enum RemoveAdminReturnArg0Tag
		{
			Err,
			Ok
		}

		[Variant]
		public class RemoveSuperAdminReturnArg0
		{
			[VariantTagProperty]
			public MyClientApiClient.RemoveSuperAdminReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public RemoveSuperAdminReturnArg0(MyClientApiClient.RemoveSuperAdminReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected RemoveSuperAdminReturnArg0()
			{
			}

			public static MyClientApiClient.RemoveSuperAdminReturnArg0 Err(string info)
			{
				return new MyClientApiClient.RemoveSuperAdminReturnArg0(MyClientApiClient.RemoveSuperAdminReturnArg0Tag.Err, info);
			}

			public static MyClientApiClient.RemoveSuperAdminReturnArg0 Ok()
			{
				return new MyClientApiClient.RemoveSuperAdminReturnArg0(MyClientApiClient.RemoveSuperAdminReturnArg0Tag.Ok, null);
			}

			public string AsErr()
			{
				this.ValidateTag(MyClientApiClient.RemoveSuperAdminReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			private void ValidateTag(MyClientApiClient.RemoveSuperAdminReturnArg0Tag tag)
			{
				if (!this.Tag.Equals(tag))
				{
					throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
				}
			}
		}

		public enum RemoveSuperAdminReturnArg0Tag
		{
			Err,
			Ok
		}

		[Variant]
		public class StartSessionGameReturnArg0
		{
			[VariantTagProperty]
			public MyClientApiClient.StartSessionGameReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public StartSessionGameReturnArg0(MyClientApiClient.StartSessionGameReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected StartSessionGameReturnArg0()
			{
			}

			public static MyClientApiClient.StartSessionGameReturnArg0 Err(string info)
			{
				return new MyClientApiClient.StartSessionGameReturnArg0(MyClientApiClient.StartSessionGameReturnArg0Tag.Err, info);
			}

			public static MyClientApiClient.StartSessionGameReturnArg0 Ok(MyClientApiClient.StartSessionGameReturnArg0.OkInfo info)
			{
				return new MyClientApiClient.StartSessionGameReturnArg0(MyClientApiClient.StartSessionGameReturnArg0Tag.Ok, info);
			}

			public string AsErr()
			{
				this.ValidateTag(MyClientApiClient.StartSessionGameReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			public MyClientApiClient.StartSessionGameReturnArg0.OkInfo AsOk()
			{
				this.ValidateTag(MyClientApiClient.StartSessionGameReturnArg0Tag.Ok);
				return (MyClientApiClient.StartSessionGameReturnArg0.OkInfo)this.Value!;
			}

			private void ValidateTag(MyClientApiClient.StartSessionGameReturnArg0Tag tag)
			{
				if (!this.Tag.Equals(tag))
				{
					throw new InvalidOperationException($"Cannot cast '{this.Tag}' to type '{tag}'");
				}
			}

			public class OkInfo
			{
				[CandidName("expiresAt")]
				public UnboundedInt ExpiresAt { get; set; }

				[CandidName("seed")]
				public UnboundedInt Seed { get; set; }

				public OkInfo(UnboundedInt expiresAt, UnboundedInt seed)
				{
					this.ExpiresAt = expiresAt;
					this.Seed = seed;
				}

				public OkInfo()
				{
				}
			}
		}

		public enum StartSessionGameReturnArg0Tag
		{
			Err,
			Ok
		}
	}
}