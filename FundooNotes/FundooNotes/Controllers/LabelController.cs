using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        FundoosContext fundoosContext;
        ILabelBL labelBL;
        public LabelController(ILabelBL labelBL, FundoosContext fundooContext)
        {
            this.labelBL = labelBL;
            this.fundoosContext = fundooContext;
        }

        // Add label

        [Authorize]
        [HttpPost("Addlabel/{NoteId}/{LabelName}")]
        public async Task<ActionResult> AddLabel(int NoteId, string LabelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.labelBL.Addlabel(userId, NoteId, LabelName);
                return this.Ok(new { success = true, message = $"Label added successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Delete Label

        [Authorize]
        [HttpDelete("DeleteLabel/{LabelId}")]
        public async Task<ActionResult> DeleteLabel(int LabelId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                await this.labelBL.DeleteLabel(LabelId, userId);
                return this.Ok(new { success = true, message = $"Label Deleted successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Update label

        [Authorize]
        [HttpPost("UpdateLabel/{LabelId}/{LabelName}")]
        public async Task<ActionResult> UpdateLabe(string LabelName, int LabelId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.labelBL.UpdateLabel(userId, LabelId, LabelName);
                if(result == null)
                {
                    return this.BadRequest(new { success = false, message = "Label Updation Failed" });
                }
                return this.Ok(new { success = true, message = $"Label Updated successfully", data = result });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Get label by note Id
        [Authorize]
        [HttpGet("GetlabelByNoteId/{noteId}")]
        public async Task<ActionResult> GetlabelByNoteId(int noteId)
        {
            try
            {
                List<Label> list = new List<Label>();
                list = await this.labelBL.GetlabelByNoteId(noteId);
                if(list == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to get label" });
                }
                return this.Ok(new { success = true, message = $"Label get successfully", data = list });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Get label
        [Authorize]
        [HttpGet("Getlabel")]
        public async Task<ActionResult> Getlabel()
        {
            try
            {
                List<Label> list = new List<Label>();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                list = await this.labelBL.Getlabel(userId);
                if (list == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to get label" });
                }
                return this.Ok(new { success = true, message = $"Label get successfully", data = list });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
