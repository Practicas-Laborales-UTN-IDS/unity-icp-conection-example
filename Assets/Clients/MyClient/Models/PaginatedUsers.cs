using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using My.Namespace.MyClient.Models;

namespace My.Namespace.MyClient.Models
{
	public class PaginatedUsers
	{
		[CandidName("hasMore")]
		public bool HasMore { get; set; }

		[CandidName("users")]
		public List<User> Users { get; set; }

		public PaginatedUsers(bool hasMore, List<User> users)
		{
			this.HasMore = hasMore;
			this.Users = users;
		}

		public PaginatedUsers()
		{
		}
	}
}