using DataAccess;
using Entity.v1.models;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entity.v1.models;
using System.Linq;

namespace WebChat.DataAccess
{
    public class VoteDetailRepository : GenericRepository<VoteDetail>
    {
        public VoteDetailRepository(ChartContext context) :
            base(context)
        {
        }
        public List<VoteDetail> GetVoteDetailListByVoteInfoId(int voteInfoId)
        {
            var query = from voteDetail in context.VoteDetail
                        where voteDetail.VoteInfoId==voteInfoId
                        select voteDetail;
            var result = query.ToList();
            return result;
        }
        
    }
}
