using Entity.v1.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class UserLogic : BaseLogic
    {
        public User GetUserByLoginName(string loginName)
        {
            return work.UserRepository.GetByUserLoginName(loginName);
        }
        public User GetUserById(int id)
        {
            return work.UserRepository.GetByID(id);
        }
        public User GetUserByUserNameAndPassword(string loginName,string password)
        {
            return work.UserRepository.GetByLoginNameAndPassword(loginName, password);
        }
        public User AddUser(User user, bool needsave=true)
        {
            save(() =>
            {
                work.UserRepository.Insert(user);
            }, needsave);
            return user;
        }
    }
}
