using BusinessLayer.Interfaces;
using CommonLayer.Users;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;

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
    }
}
