using Entity.v1.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.NetCoreServer.EntityDto
{
    public class UserInfoDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string LoginName { get; set; }
        public string MobilePhone { get; set; }
        public int Id { get; set; }

        public static UserInfoDto CreateFromUser(User u)
        {
            UserInfoDto user = new UserInfoDto
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
