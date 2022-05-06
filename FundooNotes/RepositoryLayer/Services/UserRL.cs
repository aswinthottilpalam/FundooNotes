using CommonLayer.Users;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundoosContext fundoosContext;
        public IConfiguration Configuration { get;  }
        public UserRL(FundoosContext fundoosContext, IConfiguration configuration)
        {
            this.fundoosContext = fundoosContext;
            this.Configuration = configuration;
        }

        public void AddUser(UserPostModel user)
        {
            try
            {
                User userdata = new User();
                userdata.FirstName = user.firstName;
                userdata.LastName = user.lastName;
                userdata.Email = user.email;
                userdata.Password = user.password;
                fundoosContext.Add(userdata);
                fundoosContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string LoginUser(string email, string password)
        {
            try
            {
                var result = fundoosContext.user.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
                if (result == null)
                {
                    return null;
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
