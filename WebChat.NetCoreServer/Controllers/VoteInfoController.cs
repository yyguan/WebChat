using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebChat.Entity.v1.models;
using WebChat.Logic;
using WebChat.NetCoreServer.EntityDto;
using WebChat.NetCoreServer.Util;

namespace WebChat.NetCoreServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteInfoController : Controller
    {
        private VoteInfoLogic _voteInfoLogic;
        public VoteInfoController(VoteInfoLogic voteInfoLogic)
        {
            _voteInfoLogic = voteInfoLogic;
        }
        // GET: api/VoteInfo
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/VoteInfo/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/VoteInfo/GetList
        [HttpGet("GetList")]
        public ActionResult GetList(int id)
        {
            var response = new ResponseDataHelper<List<VoteInfo>>();
            try
            {
                //var user=
                var voteInfoList = _voteInfoLogic.GetVoteInfoList();
                response.ResponseData = voteInfoList;
            }
            catch (Exception e)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = e.Message;
            }
            return Json(response);
        }
        [HttpGet(Name = "getvoteinfodetails")]
        public ActionResult GetVoteInfoDetails(int id)
        {
            var response = new ResponseDataHelper<UserVoteDetails>();
            try
            {
                var resultEntity = new UserVoteDetails();
                var voteInfo = _voteInfoLogic.GetVoteInfoById(id);
                var userVoteLogic = new UserVoteLogic(_voteInfoLogic.work);
                resultEntity.VoteInfo = voteInfo;
                resultEntity.UserVoteList = userVoteLogic.GetUserVoteByVoteInfoId(id);
                response.ResponseData = resultEntity;
            }
            catch (Exception e)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = e.Message;
            }
            return Json(response);
        }
        // POST: api/VoteInfo/addvoteinfo
        [HttpPost("addvoteinfo")]
        public ActionResult AddVoteInfo([FromBody] AddVoteInfoDto voteInfo)
        {
            var response = new ResponseDataHelper<VoteInfo>();
            try
            {
                //var user=
                var voteInfoList = _voteInfoLogic.AddVoteInfo(voteInfo.ToVoteInfo());
                response.ResponseData = voteInfoList;
            }
            catch (Exception e)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = e.Message;
            }
            return Json(response);
        }

        // PUT: api/VoteInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
