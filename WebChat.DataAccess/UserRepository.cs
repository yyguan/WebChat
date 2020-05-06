using Entity.v1.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class UserRepository:GenericRepository<User>
    {
        public UserRepository(ChartContext context) :
            base(context)
        {
        }
    }
}
