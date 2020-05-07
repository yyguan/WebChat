using Logic;
using System;
using System.Collections.Generic;
using System.Text;
using WebChat.Entity.v1.models;

namespace WebChat.Logic
{
    public class VoteInfoLogic : BaseLogic
    {

        public VoteInfo AddVoteInfo(VoteInfo voteInfo, bool needsave = true)
        {
            save(() =>
            {
                work.VoteInfoRepository.Insert(voteInfo);
            }, needsave);
            save(() =>
            {
                var voteDetailLogic = new VoteDetailLogic(this.work);
                foreach(var record in voteInfo.VoteDetails)
                {
                    record.VoteInfoId = voteInfo.Id;
                }
                voteDetailLogic.AddVoteDetails(voteInfo.VoteDetails);
            }, needsave);
            return voteInfo;
        }
        public VoteInfo GetVoteInfoById(int voteInfoId)
        {
            var voteInfo= work.VoteInfoRepository.GetByID(voteInfoId);
            voteInfo.VoteDetails = work.VoteDetailRepository.GetVoteDetailListByVoteInfoId(voteInfoId);
            return voteInfo;
        }
        public List<VoteInfo> GetVoteInfoList()
        {
            return work.VoteInfoRepository.GetVoteInfoList();
        }
    }
}
