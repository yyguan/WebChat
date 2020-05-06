using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Entity.v1.models;
using WebChat.NetCoreServer.Util;
using WebChat.NetCoreServer.EntityDto;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebChat.NetCoreServer.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private UserLogic _userLogic;
        public UserController(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/user/GetCurrentUser
        [HttpGet("GetCurrentUser")]
        public ActionResult GetCurrentUser()
        {
            var response = new ResponseDataHelper<UserInfoDto>();
            try
            {
                var userString=HttpContext.Session.GetString("CurrentUser");
                if (!string.IsNullOrEmpty(userString))
                {
                    var user=JsonConvert.DeserializeObject<User>(userString);
                    if (user != null)
                    {
                        UserInfoDto userInfo = UserInfoDto.CreateFromUser(user);
                        response.ResponseData = userInfo;
                        response.ResponseCode = 0;
                        response.ResponseMessage = "获取用户信息成功";
                    }
                    else
                    {
                        response.ResponseCode = -1001;
                        response.ResponseMessage = "用户未登录";
                    }
                }
                else
                {
                    response.ResponseCode = -1001;
                    response.ResponseMessage = "用户未登录";
                }
                //response.PagerData = new PagerHelper()
                //{
                //    DataCount = totalCount,
                //    PageSize = pageSize
                //};
            }
            catch (Exception e)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = e.Message;
            }
            return Json(response);
        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var response = new ResponseDataHelper<UserInfoDto>();

            try
            {
                //var user=
                User u = _userLogic.GetUserById(id);
                UserInfoDto userInfo = UserInfoDto.CreateFromUser(u);
                response.ResponseData = userInfo;
                //response.PagerData = new PagerHelper()
                //{
                //    DataCount = totalCount,
                //    PageSize = pageSize
                //};
            }
            catch (Exception e)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = e.Message;
            }
            return Json(response);
        }
        //public UserInfo Get(int id)
        //{
        //    User u = _userLogic.GetUserById(id);
        //    UserInfo userInfo =UserInfo.CreateFromUser(u);
        //    return userInfo;
        //}

        // POST: api/user/UserLogin
        [HttpPost("userlogin")]
        public ActionResult UserLogin([FromBody] UserLoginDto userLogin)
        {
            var response = new ResponseDataHelper<UserInfoDto>();
            try
            {
                var user = _userLogic.GetUserByLoginName(userLogin.LoginName);
                if (user == null)
                {
                    response.ResponseCode = -1;
                    response.ResponseMessage = "登录名不存在";
                }
                else if (user.Password != userLogin.Password)
                {
                    response.ResponseCode = -1;
                    response.ResponseMessage = "密码不正确";
                }
                else
                {
                    response.ResponseCode = 0;
                    response.ResponseMessage = "登录成功";
                    response.ResponseData = UserInfoDto.CreateFromUser(user);

                    HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
                }
            }
            catch (Exception e)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = e.Message;
            }
            return Json(response);
        }
        [HttpPost("adduser")]
        public ActionResult AddUser([FromBody]AddUserInfoDto userInfo)
        {
            var response = new ResponseDataHelper<UserInfoDto>();
            try
            {
                //var user=
                var user = _userLogic.GetUserByLoginName(userInfo.LoginName);
                if (user == null)
                {
                    user = _userLogic.AddUser(userInfo.ToUser());
                    response.ResponseData = UserInfoDto.CreateFromUser(user);
                }
                else
                {
                    response.ResponseCode = -1;
                    response.ResponseMessage = "登录名已存在";
                }
            }
            catch (Exception e)
            {
                response.ResponseCode = -1;
                response.ResponseMessage = e.Message;
            }
            return Json(response);
        }
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
