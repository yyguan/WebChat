using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Entity.v1.models;
using WebChat.NetCoreServer.Util;
using WebChat.NetCoreServer.EntityDto;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebChat.NetCoreServer.Controllers
{
    public class BaseController : Controller
    {
        private User _currentUser
        {
            get;set;
        }
        public User CurrentUser
        {
            set
            {
                _currentUser = value;
            }
            get
            {
                return _currentUser;
            }
        }
        /// <summary>
        /// 请求过滤处理
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            byte[] result;
            var user=filterContext.HttpContext.Session.GetString("CurrentUser");
            if (string.IsNullOrEmpty(user))
            {
                //如果没有登录信息,则跳转到登录页面
                var errorResult = new ResponseDataHelper<UserInfoDto>()
                {
                    ResponseCode = -1001,
                    ResponseMessage = "用户未登录"
                };
                filterContext.Result = Json(errorResult);
                return;
            }
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        protected void SetSession(string key, string value)
        {
            HttpContext.Session.SetString(key, value);
        }

        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetSession(string key)
        {
            var value = HttpContext.Session.GetString(key);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }

    }

}
