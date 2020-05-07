using DataAccess;
using Logic;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entity.v1.models;

namespace WebChat.Logic
{
    public class UserVoteLogic : BaseLogic
    {
        public UserVoteLogic(UnitOfWork work)
        {
            this.work = work;
        }
        public List<UserVote> GetUserVoteByVoteInfoId(int voteInfoId)
        {
            return work.UserVoteRepository.GetUserVoteByVoteInfoId(voteInfoId);
        }
        public UserVote GetUserVoteByUserAndVote(int userId, int voteInfoId)
        {
            return work.UserVoteRepository.GetUserVoteByUserAndVote(userId, voteInfoId);
        }
        public UserVote AddUserVote(int userId, int voteInfoId)
        {
            UserVote userVote = new UserVote()
            {
                UserId = userId,
                VoteInfoId = voteInfoId
            };
            save(() =>
            {
                work.UserVoteRepository.Insert(userVote);
            }, true);
            return userVote;
        }
    }
}
