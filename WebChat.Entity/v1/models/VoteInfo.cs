using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Entity.v1.models
{
    [Serializable]
    public class VoteInfo
    {
        public int Id { get; set; }
        public int? CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public string Title { get; set; }

        public DateTime? CreateDateTime { get; set; }
        public List<VoteDetail> VoteDetails { get; set; }
    }
}
