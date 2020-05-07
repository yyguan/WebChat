using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Entity.v1.models;

namespace WebChat.NetCoreServer.EntityDto
{
    public class UserVoteDetails
    {
        public VoteInfo VoteInfo { get; set; }
        
        public List<UserVote> UserVoteList { get; set; }
    }
}
