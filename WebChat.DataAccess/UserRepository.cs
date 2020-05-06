using Entity.v1.models;
using System;
using System.Linq;
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
        /// <summary>
        /// 通过loginName获取用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public User GetByUserLoginName(string loginName)
        {
            var query = from user in context.User
                        where user.LoginName == loginName
                        select user;
            var result = query.FirstOrDefault();
            return result;
        }
    }
}
