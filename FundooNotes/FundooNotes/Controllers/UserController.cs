using BusinessLayer.Interfaces;
using CommonLayer.Users;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;
using System.Linq;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        FundoosContext fundoosContext;
        IUserBL userBL;

        public UserController(FundoosContext fundoos, IUserBL userBL)
        {
            this.fundoosContext = fundoos;
            this.userBL = userBL;
        }

        [HttpPost]
        public ActionResult AddUser(UserPostModel user)
        {
            try
            {
                this.userBL.AddUser(user);
                return this.Ok(new {sucess = true, message = $"User data added successfully" });
            }
            catch (Exception e)
            {  
                throw e;
            }
        }

        [HttpPost("LoginUser")]
        public ActionResult LoginUser(string email,  string password)
        {
            try
            {
                var userdata = fundoosContext.User.FirstOrDefault(u => u.Email == email && u.Password == password);
                string token = this.userBL.LoginUser(email, password);
                if(token == null)
                {
                    return this.BadRequest(new { success = false, message = $"Email or Password Invalid" });
                }
                return this.Ok(new { sucess = true, message = $"Token generated is " + token });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = $"Login Failed {e.Message}" });
            }
        }

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(string email)
        {
            try
            {
                var result = this.userBL.ForgotPassword(email);
                if (result != false)
                {
                    return this.Ok(new{ success = true, message = $"Mail Sent Successfully " + $" token:  {result}" });
                }
                return this.BadRequest(new { success = false, message = $"mail not sent" });
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
