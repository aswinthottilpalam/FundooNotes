using BusinessLayer.Interfaces;
using CommonLayer.Users;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;

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
        public IActionResult AddUser(UserPostModel user)
        {
            try
            {
                this.userBL.AddUser(user);
                return this.Ok(new {sucess = true, message = $"User data added successfully" });
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
