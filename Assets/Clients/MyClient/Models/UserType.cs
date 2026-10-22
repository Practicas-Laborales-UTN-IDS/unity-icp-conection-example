using EdjCase.ICP.Candid.Mapping;
using EdjCase.ICP.Candid.Models;
using System.Collections.Generic;
using Metadata = System.Collections.Generic.List<My.Namespace.MyClient.Models.Entry>;

namespace My.Namespace.MyClient.Models
{
	public class UserType
	{
		[CandidName("avatar")]
		public OptionalValue<List<byte>> Avatar { get; set; }

		[CandidName("email")]
		public OptionalValue<string> Email { get; set; }

		[CandidName("extraData")]
		public Metadata ExtraData { get; set; }

		[CandidName("name")]
		public string Name { get; set; }

		[CandidName("principal")]
		public Principal Principal { get; set; }

		[CandidName("registrationDate")]
		public UnboundedInt RegistrationDate { get; set; }

		public UserType(OptionalValue<List<byte>> avatar, OptionalValue<string> email, Metadata extraData, string name, Principal principal, UnboundedInt registrationDate)
		{
			this.Avatar = avatar;
			this.Email = email;
			this.ExtraData = extraData;
			this.Name = name;
			this.Principal = principal;
			this.RegistrationDate = registrationDate;
		}

		public UserType()
		{
		}
	}
}