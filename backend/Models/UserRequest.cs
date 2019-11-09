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

        public void Add(User user)
        {
            userList.Add(user);
        }

        public String Remove(int userId)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                User temp = userList.ElementAt(i);
                if (temp.UserId.Equals(userId))
                {
                    userList.RemoveAt(i);
                    return "Delete successful";
                }
            }
            return "Delete unsuccessful";
        }

        public List<User> getAllUsers()
        {
            return userList;
        }

        public String UpdateUser(User user)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                User temp = userList.ElementAt(i);
                if (temp.UserId.Equals(user.UserId))
                {
                    userList[i] = user; //update user
                    return "Update successful";
                }
            }
            return "Update unsuccessful";
        }

        public User getUserById(int id)
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
    }
}