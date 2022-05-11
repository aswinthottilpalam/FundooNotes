using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;
using System.Linq;
using System.Security.Claims;

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

        [HttpPost("Login/{email}/{password}")]
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

        [HttpPost("ForgetPassword/{email}")]
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



        [Authorize]
        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(ChangePasswordModel valid)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = fundoosContext.User.Where(u => u.UserId == userId).FirstOrDefault();
                string email = result.Email.ToString();
                bool res = userBL.ChangePassword(email, valid);

                if(res == false)
                {
                    return this.BadRequest(new { success = false, message = $"Enter valid Password" });
                }
                return this.Ok(new { success = true, message = $"Change Password Successfull" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
