using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        FundoosContext fundoosContext;
        INoteBL noteBL;
        public NoteController(INoteBL noteBL, FundoosContext fundooContext)
        {
            this.noteBL = noteBL;
            this.fundoosContext = fundooContext;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                await this.noteBL.AddNote(UserId, notePostModel);
                return this.Ok(new { success = true, message = "Note Added Successfully " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Delete Note

        [Authorize]
        [HttpDelete]
        public ActionResult DeleteNote(int noteId)
        {
            try
            {
                if (noteBL.DeleteNote(noteId))
                {
                    return this.Ok(new { success = true, message = "Note Deleted Successfully" });
                }
                return this.BadRequest(new { success = true, message = "Note Deletion Failed" });
            }
            catch (Exception e)
            {

                throw e;
            }
        }



        // Change Color

        [Authorize]
        [HttpPut("ChangeColor/{noteId}/{color}")]
        public async Task<ActionResult>ChangeColor(int noteId, string color)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                var note = fundoosContext.Notes.FirstOrDefault(e => e.UserId == UserId && e.NoteId == noteId);
                if(note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Note does not exixt" });
                }


                await this.noteBL.ChangeColor(UserId, noteId, color);
                return this.Ok(new { success = true, message = "Color changed Successfully " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
