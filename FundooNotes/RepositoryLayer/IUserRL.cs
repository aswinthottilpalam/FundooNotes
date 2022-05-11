using CommonLayer;
using CommonLayer.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entities

{
    public interface IUserRL
    {
        public void AddUser(UserPostModel user);

        public string LoginUser(string email, string password);

        public bool ForgotPassword(string email);

        public bool ChangePassword(string email, ChangePasswordModel valid);
    }
}
