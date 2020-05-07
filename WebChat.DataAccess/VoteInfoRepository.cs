using DataAccess;
using Entity.v1.models;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entity.v1.models;
using System.Linq;

namespace WebChat.DataAccess
{
    public class VoteInfoRepository : GenericRepository<VoteInfo>
    {
        public VoteInfoRepository(ChartContext context) :
            base(context)
        {
        }

        public List<VoteInfo> GetVoteInfoList()
        {
            var query = from VoteInfo in context.VoteInfo
                        select VoteInfo;
            var result = query.ToList();
            return result;
        }
    }
}
