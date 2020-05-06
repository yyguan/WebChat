using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebChat.NetCoreServer.EntityDto;
using WebChat.NetCoreServer.Util;
using Newtonsoft;
using Newtonsoft.Json;

namespace WebChat.NetCoreServer.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private UserLogic _userLogic;
        //private IHttpContextAccessor _accessor;
        public LoginController(UserLogic userlogic)
        {
            _userLogic = userlogic;
            //_accessor = accessor;
        }

        // POST: api/login/userlogin
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
        // POST: api/Login
        [HttpPost]
        public ActionResult Post([FromBody] UserLoginDto userLogin)
        {
            return null;
        }

    }
}
