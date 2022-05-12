using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class LabelController : ControllerBase
    {
        FundoosContext fundoosContext;
        ILabelBL labelBL;
        public LabelController(ILabelBL labelBL, FundoosContext fundooContext)
        {
            this.labelBL = labelBL;
            this.fundoosContext = fundooContext;
        }


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

    }
}
