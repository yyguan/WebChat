using System;
using System.Collections.Generic;
using System.Text;

namespace WebChat.Entity.v1.models
{
    public class VoteDetail
    {
        public int Id { get; set; }
        public int? VoteInfoId { get; set; }
        public string ItemTitle { get; set; }
    }
}
