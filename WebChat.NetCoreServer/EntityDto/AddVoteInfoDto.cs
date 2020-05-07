using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Entity.v1.models;

namespace WebChat.NetCoreServer.EntityDto
{
    public class AddVoteInfoDto
    {
        public int Id { get; set; }
        public int? CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string Title { get; set; }


        public VoteInfo ToVoteInfo()
        {
            var voteInfo = new VoteInfo()
            {
                CreateDateTime = DateTime.Now,
                CreateUserId = this.CreateUserId,
                CreateUserName = this.CreateUserName,
                Id = this.Id,
                Title = this.Title
            };
            return voteInfo;
        }
    }
}
