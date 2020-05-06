using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebChat.NetCoreServer.Controllers
{
    public class BaseController : Controller
    {
        private Object _currentUser
        {
            get;set;
        }
        public object CurrentUser
        {
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
            filterContext.HttpContext.Session.TryGetValue("CurrentUser", out result);
            // 将获取的byte[] 转换为字符串
            var UserStr = System.Text.Encoding.UTF8.GetString(result);
            if (result == null)
            {
                filterContext.Result = new RedirectResult("/Login/SignIn");
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
