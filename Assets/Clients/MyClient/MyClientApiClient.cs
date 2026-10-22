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

		public async Task<List<Models.User>> GetAdmins()
		{
			CandidArg arg = CandidArg.FromCandid();
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, "getAdmins", arg);
			CandidArg reply = response.ThrowOrGetReply();
			return reply.ToObjects<List<Models.User>>(this.Converter);
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
	}
}