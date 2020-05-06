using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Entity.v1.models;
using WebChat.NetCoreServer.Util;

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

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var response = new ResponseDataHelper<UserInfo>();
            
            try
            {
                //var user=
                User u = _userLogic.GetUserById(id);
                UserInfo userInfo = UserInfo.CreateFromUser(u);
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
        [HttpPost("adduser")]
        public ActionResult AddUser([FromBody]AddUserInfo userInfo)
        {
            var response = new ResponseDataHelper<UserInfo>();
            try
            {
                //var user=
                var user = _userLogic.AddUser(userInfo.ToUser());
                response.ResponseData =UserInfo.CreateFromUser(user); 
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
    public class AddUserInfo:UserInfo
    {
        public string Password { get; set; }
        public User ToUser()
        {
            User u = new User
            {
                Id = this.Id,
                Email = this.Email,
                LoginName = this.LoginName,
                MobilePhone = this.MobilePhone,
                UserName = this.UserName,
                CreateTime = DateTime.Now,
                Password = this.Password
            };
            return u;
        }
    }
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public string MobilePhone { get; set; }
        public int Id { get; set; }

        public static UserInfo CreateFromUser(User u)
        {
            UserInfo user = new UserInfo
            {
                Id = u.Id,
                Email = u.Email,
                LoginName = u.LoginName,
                MobilePhone = u.MobilePhone,
                UserName = u.UserName
            };
            return user;
        }
    }
}
