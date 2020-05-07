using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Entity.v1.models
{
    public class UserVote
    {

        public int Id { get; set; }
        public int? VoteInfoId { get; set; }
        public int? VoteDetailId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
