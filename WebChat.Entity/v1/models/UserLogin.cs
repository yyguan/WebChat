using System;
using System.Collections.Generic;

namespace Entity.v1.models
{
    public partial class UserLogin
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? LoginTime { get; set; }
        public string LoginStatus { get; set; }
    }
}
