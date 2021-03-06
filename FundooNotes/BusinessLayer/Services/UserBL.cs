using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Users;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL  userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public void AddUser(UserPostModel user)
        {
            try
            {
                this.userRL.AddUser(user);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string LoginUser(string email, string password)
        {
            try
            {
                return this.userRL.LoginUser(email, password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public bool ChangePassword(string email, ChangePasswordModel valid)
        {
            try
            {
                return this.userRL.ChangePassword(email, valid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
