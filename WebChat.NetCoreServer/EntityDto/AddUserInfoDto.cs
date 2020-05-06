using Entity.v1.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.NetCoreServer.EntityDto
{

    public class AddUserInfoDto : UserInfoDto
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
}
