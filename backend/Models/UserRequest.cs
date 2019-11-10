using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend.Models
{
    public class UserRequest
    {
        List<User> userList;
        static UserRequest userReq = null;

        private UserRequest()
        {
            userList = new List<User>();
        }

        public static UserRequest getInstance()
        {
            if (userReq == null)
            {
                userReq = new UserRequest();
                return userReq;
            }
            else
            {
                return userReq;
            }
        }

        public bool Add(User user)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                string str = guid.ToString();
                user.UserId = str;
                user.Password = SecurePasswordHasher.Hash(user.Password);
                userList.Add(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Remove(string userId)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                User temp = userList.ElementAt(i);
                if (temp.UserId.Equals(userId))
                {
                    userList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public List<User> getAllUsers()
        {
            return userList;
        }

        public bool UpdateUser(User user)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                User temp = userList.ElementAt(i);
                if (temp.UserId.Equals(user.UserId))
                {
                    userList[i] = user; //update user
                    return true;
                }
            }
            return false;
        }

        public User getUserById(string id)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                User temp = userList.ElementAt(i);
                if (temp.UserId.Equals(id))
                {
                    return temp;
                }
            }
            return null;
        }

        public User getUserByName(string name)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                User temp = userList.ElementAt(i);
                if (temp.Name.Equals(name))
                {
                    return temp;
                }
            }
            return null;
        }
    }
}