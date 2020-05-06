using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.NetCoreServer.EntityDto
{

    public class UserLoginDto
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
    }
}
