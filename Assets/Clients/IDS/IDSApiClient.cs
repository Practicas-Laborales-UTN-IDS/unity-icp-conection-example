using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Candid.Models;
using EdjCase.ICP.Candid;
using System.Threading.Tasks;
using InsideDarkStudio.Clients.IDS;
using EdjCase.ICP.Agent.Responses;
using System.Collections.Generic;
using EdjCase.ICP.Candid.Mapping;
using System;
using Metrics = System.Collections.Generic.List<InsideDarkStudio.Clients.IDS.Models.MetricItem>;
using Metadata = System.Collections.Generic.List<InsideDarkStudio.Clients.IDS.Models.Entry>;
using GameId = EdjCase.ICP.Candid.Models.UnboundedInt;

namespace InsideDarkStudio.Clients.IDS
{
	public class IDSApiClient
	{
		public IAgent Agent { get; }
		public Principal CanisterId { get; }
		public CandidConverter? Converter { get; }

		public IDSApiClient(IAgent agent, Principal canisterId, CandidConverter? converter = default)
		{
			this.Agent = agent;
			this.CanisterId = canisterId;
			this.Converter = converter;
		}

		public async Task<IDSApiClient.AddAdminReturnArg0> AddAdmin(Principal p)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(p, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "addAdmin", arg);
			return reply.ToObjects<IDSApiClient.AddAdminReturnArg0>(this.Converter);
		}

		public async Task<UnboundedInt> AddGame(Models.GameArgs args)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(args, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "addGame", arg);
			return reply.ToObjects<UnboundedInt>(this.Converter);
		}

		public async Task<IDSApiClient.AddSuperAdminReturnArg0> AddSuperAdmin(Principal p)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(p, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "addSuperAdmin", arg);
			return reply.ToObjects<IDSApiClient.AddSuperAdminReturnArg0>(this.Converter);
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

		public async Task<IDSApiClient.EndGameReturnArg0> EndGame(UnboundedInt gameId, List<byte> payload, List<byte> proof)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(gameId, this.Converter), CandidTypedValue.FromObject(payload, this.Converter), CandidTypedValue.FromObject(proof, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "endGame", arg);
			return reply.ToObjects<IDSApiClient.EndGameReturnArg0>(this.Converter);
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

		public async Task<Metrics> GetMetrics(OptionalValue<Principal> u, GameId gameId)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(u, this.Converter), CandidTypedValue.FromObject(gameId, this.Converter));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getMetrics", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Metrics>(this.Converter);
		}

		public async Task<List<Models.IdentityResult>> GetSuperAdmins()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getSuperAdmins", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<Models.IdentityResult>>(this.Converter);
		}

		public async Task<Models.PaginatedUsers> GetUsers(IDSApiClient.GetUsersArg0 arg0)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(arg0, this.Converter));
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getUsers", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Models.PaginatedUsers>(this.Converter);
		}

		public async Task<Metadata> IdsMetrics()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "idsMetrics", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<Metadata>(this.Converter);
		}

		public async Task<OptionalValue<Models.UserType>> Login()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "login", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<OptionalValue<Models.UserType>>(this.Converter);
		}

		public async Task<IDSApiClient.RemoveAdminReturnArg0> RemoveAdmin(Principal p)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(p, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "removeAdmin", arg);
			return reply.ToObjects<IDSApiClient.RemoveAdminReturnArg0>(this.Converter);
		}

		public async Task<IDSApiClient.RemoveSuperAdminReturnArg0> RemoveSuperAdmin(Principal p)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(p, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "removeSuperAdmin", arg);
			return reply.ToObjects<IDSApiClient.RemoveSuperAdminReturnArg0>(this.Converter);
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

		public async Task<IDSApiClient.StartSessionGameReturnArg0> StartSessionGame(UnboundedInt gameId)
		{
			CandidArg arg = CandidArg.FromCandid(CandidTypedValue.FromObject(gameId, this.Converter));
			CandidArg reply = await this.Agent.CallAsync(this.CanisterId, "startSessionGame", arg);
			return reply.ToObjects<IDSApiClient.StartSessionGameReturnArg0>(this.Converter);
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
			public IDSApiClient.AddAdminReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public AddAdminReturnArg0(IDSApiClient.AddAdminReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected AddAdminReturnArg0()
			{
			}

			public static IDSApiClient.AddAdminReturnArg0 Err(string info)
			{
				return new IDSApiClient.AddAdminReturnArg0(IDSApiClient.AddAdminReturnArg0Tag.Err, info);
			}

			public static IDSApiClient.AddAdminReturnArg0 Ok()
			{
				return new IDSApiClient.AddAdminReturnArg0(IDSApiClient.AddAdminReturnArg0Tag.Ok, null);
			}

			public string AsErr()
			{
				this.ValidateTag(IDSApiClient.AddAdminReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			private void ValidateTag(IDSApiClient.AddAdminReturnArg0Tag tag)
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

		[Variant]
		public class AddSuperAdminReturnArg0
		{
			[VariantTagProperty]
			public IDSApiClient.AddSuperAdminReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public AddSuperAdminReturnArg0(IDSApiClient.AddSuperAdminReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected AddSuperAdminReturnArg0()
			{
			}

			public static IDSApiClient.AddSuperAdminReturnArg0 Err(string info)
			{
				return new IDSApiClient.AddSuperAdminReturnArg0(IDSApiClient.AddSuperAdminReturnArg0Tag.Err, info);
			}

			public static IDSApiClient.AddSuperAdminReturnArg0 Ok()
			{
				return new IDSApiClient.AddSuperAdminReturnArg0(IDSApiClient.AddSuperAdminReturnArg0Tag.Ok, null);
			}

			public string AsErr()
			{
				this.ValidateTag(IDSApiClient.AddSuperAdminReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			private void ValidateTag(IDSApiClient.AddSuperAdminReturnArg0Tag tag)
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
			public IDSApiClient.EndGameReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public EndGameReturnArg0(IDSApiClient.EndGameReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected EndGameReturnArg0()
			{
			}

			public static IDSApiClient.EndGameReturnArg0 Err(string info)
			{
				return new IDSApiClient.EndGameReturnArg0(IDSApiClient.EndGameReturnArg0Tag.Err, info);
			}

			public static IDSApiClient.EndGameReturnArg0 Ok(Metrics info)
			{
				return new IDSApiClient.EndGameReturnArg0(IDSApiClient.EndGameReturnArg0Tag.Ok, info);
			}

			public string AsErr()
			{
				this.ValidateTag(IDSApiClient.EndGameReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			public Metrics AsOk()
			{
				this.ValidateTag(IDSApiClient.EndGameReturnArg0Tag.Ok);
				return (Metrics)this.Value!;
			}

			private void ValidateTag(IDSApiClient.EndGameReturnArg0Tag tag)
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
			public IDSApiClient.RemoveAdminReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public RemoveAdminReturnArg0(IDSApiClient.RemoveAdminReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected RemoveAdminReturnArg0()
			{
			}

			public static IDSApiClient.RemoveAdminReturnArg0 Err(string info)
			{
				return new IDSApiClient.RemoveAdminReturnArg0(IDSApiClient.RemoveAdminReturnArg0Tag.Err, info);
			}

			public static IDSApiClient.RemoveAdminReturnArg0 Ok()
			{
				return new IDSApiClient.RemoveAdminReturnArg0(IDSApiClient.RemoveAdminReturnArg0Tag.Ok, null);
			}

			public string AsErr()
			{
				this.ValidateTag(IDSApiClient.RemoveAdminReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			private void ValidateTag(IDSApiClient.RemoveAdminReturnArg0Tag tag)
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
			public IDSApiClient.RemoveSuperAdminReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public RemoveSuperAdminReturnArg0(IDSApiClient.RemoveSuperAdminReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected RemoveSuperAdminReturnArg0()
			{
			}

			public static IDSApiClient.RemoveSuperAdminReturnArg0 Err(string info)
			{
				return new IDSApiClient.RemoveSuperAdminReturnArg0(IDSApiClient.RemoveSuperAdminReturnArg0Tag.Err, info);
			}

			public static IDSApiClient.RemoveSuperAdminReturnArg0 Ok()
			{
				return new IDSApiClient.RemoveSuperAdminReturnArg0(IDSApiClient.RemoveSuperAdminReturnArg0Tag.Ok, null);
			}

			public string AsErr()
			{
				this.ValidateTag(IDSApiClient.RemoveSuperAdminReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			private void ValidateTag(IDSApiClient.RemoveSuperAdminReturnArg0Tag tag)
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
			public IDSApiClient.StartSessionGameReturnArg0Tag Tag { get; set; }

			[VariantValueProperty]
			public object? Value { get; set; }

			public StartSessionGameReturnArg0(IDSApiClient.StartSessionGameReturnArg0Tag tag, object? value)
			{
				this.Tag = tag;
				this.Value = value;
			}

			protected StartSessionGameReturnArg0()
			{
			}

			public static IDSApiClient.StartSessionGameReturnArg0 Err(string info)
			{
				return new IDSApiClient.StartSessionGameReturnArg0(IDSApiClient.StartSessionGameReturnArg0Tag.Err, info);
			}

			public static IDSApiClient.StartSessionGameReturnArg0 Ok(IDSApiClient.StartSessionGameReturnArg0.OkInfo info)
			{
				return new IDSApiClient.StartSessionGameReturnArg0(IDSApiClient.StartSessionGameReturnArg0Tag.Ok, info);
			}

			public string AsErr()
			{
				this.ValidateTag(IDSApiClient.StartSessionGameReturnArg0Tag.Err);
				return (string)this.Value!;
			}

			public IDSApiClient.StartSessionGameReturnArg0.OkInfo AsOk()
			{
				this.ValidateTag(IDSApiClient.StartSessionGameReturnArg0Tag.Ok);
				return (IDSApiClient.StartSessionGameReturnArg0.OkInfo)this.Value!;
			}

			private void ValidateTag(IDSApiClient.StartSessionGameReturnArg0Tag tag)
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