using Entity.v1.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class UserLoginRepository : GenericRepository<UserLogin>
    {
        public UserLoginRepository(ChartContext context) :
            base(context)
        {
        }
    }
}
