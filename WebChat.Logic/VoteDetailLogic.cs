using DataAccess;
using Logic;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entity.v1.models;

namespace WebChat.Logic
{
    public class VoteDetailLogic:BaseLogic
    {
        public VoteDetailLogic(UnitOfWork work)
        {
            this.work = work;
        }

        public void AddVoteDetails(List <VoteDetail> voteDetails, bool needsave = true)
        {
            save(() =>
            {
                foreach (var record in voteDetails)
                {
                    work.VoteDetailRepository.Insert(record);
                }
            }, needsave);
        }
        public List<VoteDetail> GetVoteDetailListByVoteInfoId(int voteInfoId)
        {
            return work.VoteDetailRepository.GetVoteDetailListByVoteInfoId(voteInfoId);
        }
    }
}
