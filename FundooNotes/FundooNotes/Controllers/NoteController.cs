using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        FundoosContext fundoosContext;
        INoteBL noteBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBL noteBL, FundoosContext fundooContext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.noteBL = noteBL;
            this.fundoosContext = fundooContext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }


        // Add Note

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

        // Archive Note

        [Authorize]
        [HttpPut("ArchiveNote/{noteId}")]
        public async Task<ActionResult> ArchiveNote(int noteId) 
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundoosContext.Notes.FirstOrDefault(e => e.UserId == UserId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Failed to Archive Note" });  
                }
                await this.noteBL.ArchiveNote(noteId);
                return this.Ok(new { success = true, message = "Note Archived Successfully" });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Update note
        [Authorize]
        [HttpPut("UpdateNote/{noteId}")]
        public async Task<ActionResult<Note>> UpdateNote(int noteId, NoteUpdateModel noteUpdateModel)
        {
          try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                //var note = noteBL.UpdateNote(userId, noteId, noteUpdateModel);
                var note = fundoosContext.Notes.FirstOrDefault(e => e.UserId == UserId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Update notes Failed" });  
                }
                await this.noteBL.UpdateNote(noteId, noteUpdateModel);
                return this.Ok(new { success = true, message = "Note Updated Successfully" });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Get Note
        [Authorize]
        [HttpGet(("GetNote/{noteId}"))]
        public async Task<ActionResult> GetNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var note = fundoosContext.Notes.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Get Note Failed" });
                }
                var res = await this.noteBL.GetNote(noteId);
                //await this.noteBL.GetNote(noteId);

                return this.Ok(new { success = true, message = "Get note Success", data = res });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Pin note 
        [Authorize]
        [HttpPut("PinNote/{noteId}")]
        public async Task<ActionResult<Note>> PinNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundoosContext.Notes.FirstOrDefault(e => e.UserId == UserId && e.NoteId == noteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Failed to Pin Note" });
                }
                await this.noteBL.PinNote(noteId);
                return this.Ok(new { success = true, message = "Note Pinned Successfully" });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Trash Note
        [Authorize]
        [HttpPut("TrashNote/{noteId}")]
        public async Task<ActionResult> TrashNote(int noteId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundoosContext.Notes.Where(x => x.UserId == UserId && x.NoteId == noteId).FirstOrDefault();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Note does not exist" });
                }
                await this.noteBL.TrashNote(noteId, UserId);
                return this.Ok(new { success = true, message = "Note Trashed Successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Remainder Notes
        [Authorize]
        [HttpPut("Remainder/{noteId}/{RemainderDate}")]
        public async Task<ActionResult> Remainder(int noteId, DateTime RemainderDate)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var note = fundoosContext.Notes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Note does not exist" });
                }

                await this.noteBL.Remainder(userId, noteId, RemainderDate);
                return this.Ok(new { success = true, message = $"Remainder added Successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Get all Notes
        [Authorize]
        [HttpGet("GetAllNotes")]
        public async Task<ActionResult> GetAllNote()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                List<Note> result = new List<Note>();
                result = await this.noteBL.GetAllNote(userId); 
                return this.Ok(new { success = true, message = $"Notes generated Successfully", data = result});
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //[HttpGet("GetAllNotesRedis")]
        //public async Task<ActionResult> GetAllNote()
        //{
        //    try
        //    {
        //        string serializeNoteList;
        //        var noteList = new List<Note>();
        //        var redisNoteList = await distributedCache.GetAsync(key);
        //        if (redisNoteList != null)
        //        {
        //            serializeNoteList = Encoding.UTF8.GetString(redisNoteList);
        //            noteList = JsonConvert.DeserializeObject<List<Note>>(serializeNoteList);
        //        }
        //        else
        //        {
        //            noteList = await this.noteBL.GetAllNote();
        //            serializeNoteList = JsonConvert.SerializeObject(noteList);
        //            redisNoteList = Encoding.UTF8.GetBytes(serializeNoteList);
        //            var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));

        //        }
        //        return this.Ok(new { success = true, message = "Get note successful!!!", data = noteList });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



    }
}
