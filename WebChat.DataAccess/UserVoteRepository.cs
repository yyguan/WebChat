using DataAccess;
using Entity.v1.models;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entity.v1.models;
using System.Linq;

namespace WebChat.DataAccess
{
    public class UserVoteRepository : GenericRepository<UserVote>
    {
        public UserVoteRepository(ChartContext context) :
            base(context)
        {
        }
        public List<UserVote> GetUserVoteByVoteInfoId(int voteInfoId)
        {
            var query = from userVote in context.UserVote
                        where userVote.VoteInfoId == voteInfoId
                        select userVote;
            var result = query.ToList();
            return result;
        }
        public UserVote GetUserVoteByUserAndVote(int userId,int voteInfoId)
        {
            var query = from userVote in context.UserVote
                        where userVote.UserId == userId && userVote.VoteInfoId == voteInfoId
                        select userVote;
            return query.FirstOrDefault();
        }
    }
}
