using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        //instance of  FundooContext Class
        FundoosContext fundoosContext;
        public IConfiguration Configuration { get; }
        public LabelRL(FundoosContext fundoosContext, IConfiguration configuration)
        {
            this.fundoosContext = fundoosContext;
            this.Configuration = configuration;
        }

        // Add Label
        public async Task Addlabel(int userId, int Noteid, string LabelName)
        {
            try
            {
                var user = fundoosContext.User.FirstOrDefault(u => u.UserId == userId);
                var note = fundoosContext.Notes.FirstOrDefault(b => b.NoteId == Noteid);
                Entities.Label label = new Entities.Label
                {
                    User = user,
                    Note = note
                };
                label.LabelName = LabelName;
                fundoosContext.Label.Add(label);
                await fundoosContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Delete Label
        public async Task DeleteLabel(int LabelId, int userId)
        {
            try
            {
                var result = fundoosContext.Label.FirstOrDefault(u => u.LabelId == LabelId && u.UserId == userId);
                fundoosContext.Label.Remove(result);
                await fundoosContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // Update Label
        public async Task<Label> UpdateLabel(int userId, int LabelId, string LabelName)
        {
            try
            {
                Entities.Label result = fundoosContext.Label.FirstOrDefault(u => u.LabelId == LabelId && u.UserId == userId);
                if (result != null)
                {
                    result.LabelName = LabelName;
                    await fundoosContext.SaveChangesAsync();
                    var res = fundoosContext.Label.Where(u => u.LabelId == LabelId).FirstOrDefault();
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Label>> GetlabelByNoteId(int NoteId)
        {
            try
            {
                List<Label> result = await fundoosContext.Label.Where(u => u.NoteId == NoteId).Include(u => u.User).Include(u => u.Note).ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Label>> Getlabel(int userId)
        {
            try
            {
                List<Label> result = await fundoosContext.Label.Where(u => u.UserId == userId).Include(u => u.User).Include(u => u.Note).ToListAsync();
                return result;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
