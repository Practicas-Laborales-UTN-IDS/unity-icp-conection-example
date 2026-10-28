using EdjCase.ICP.Candid.Mapping;
using System.Collections.Generic;
using InsideDarkStudio.Clients.IDS.Models;

namespace InsideDarkStudio.Clients.IDS.Models
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