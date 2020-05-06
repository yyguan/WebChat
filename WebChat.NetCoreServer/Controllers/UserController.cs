using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebChat.NetCoreServer.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public UserInfo Get(int id)
        {
            UserInfo u = new UserInfo
            {
                Id = id,
                Email = "gyybcy@163.com",
                LoginName = "yyguan",
                MobileNumber = "15026511483",
                UserName = "管延勇"

            };
            return u;
        }
        [HttpPost("adduser")]
        public UserInfo AddUser([FromBody]UserInfo userInfo)
        {
            Random r = new Random();
            userInfo.Id = r.Next(100);
            return userInfo;

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

    public class UserInfo
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public string MobileNumber { get; set; }
        public int Id { get; set; }
    }
}
