using CommonLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        //instance of  FundooContext Class
        FundoosContext fundoosContext;
        public IConfiguration Configuration { get; }
        public NoteRL(FundoosContext fundoosContext, IConfiguration configuration)
        {
            this.fundoosContext = fundoosContext;
            this.Configuration = configuration;
        }

        // Add notes
        public async Task AddNote(int userId, NotePostModel notePostModel)
        {
            try
            {
                //var user = fundooDBContext.Users.FirstOrDefault(u => u.userID == userId);
                Note note = new Note();
                //note.NoteId = new Note().NoteId;
                note.UserId = userId;

                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.BgColor = notePostModel.BgColor;
                note.IsPin = false;
                note.IsRemainder = false;
                note.IsArchieve = false;
                note.IsTrash = false;
                note.RegisterDate = DateTime.Now;
                note.ModifyDate = DateTime.Now;

                fundoosContext.Add(note);
                await fundoosContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        // Delete note
        public bool DeleteNote(int noteId)
        {
            Note note = fundoosContext.Notes.FirstOrDefault(e => e.NoteId == noteId);
            if (note != null)
            {
                fundoosContext.Notes.Remove(note);
                fundoosContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        // Change Color
        public async Task ChangeColor(int userId, int noteId, string color)
        {
            try
            {
                var note = fundoosContext.Notes.FirstOrDefault(e => e.UserId == userId && e.NoteId == noteId);
                if(note != null)
                {
                    note.BgColor = color;
                    await fundoosContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Archive Notes
        public async Task ArchiveNote(int noteId)
        {
            try
            {
                var note = fundoosContext.Notes.FirstOrDefault(e => e.NoteId == noteId);

                if( note != null)
                {
                    if(note.IsArchieve == true)
                    {
                        note.IsArchieve = false;
                    }
                    else
                    {
                        note.IsArchieve = true;
                    }
                       
                }

                await fundoosContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Update Note
        public async Task<Note> UpdateNote(int noteId, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                var note = fundoosContext.Notes.FirstOrDefault(e => e.NoteId == noteId);
                if (note != null)
                {
                    note.Title = noteUpdateModel.Title;
                    note.Description = noteUpdateModel.Description;
                    note.IsArchieve = noteUpdateModel.IsArchieve;
                    note.BgColor = noteUpdateModel.BgColor;
                    note.IsPin = noteUpdateModel.IsPin;
                    note.IsRemainder = noteUpdateModel.IsRemainder;
                    note.IsTrash = noteUpdateModel.IsTrash;

                    await fundoosContext.SaveChangesAsync();
                }

                return await fundoosContext.Notes
                .Where(u => u.UserId == u.UserId && u.NoteId == noteId)
                .Include(u => u.user)
                .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Get Notes
        public async Task<Note> GetNote(int noteId)
        {
            try
            {
                return await fundoosContext.Notes.Where(u => u.NoteId == noteId).Include(u => u.user).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // Pin Note
        public async Task<Note> PinNote( int noteId)
        {
            try
            {
                var note = fundoosContext.Notes.FirstOrDefault(e => e.NoteId == noteId);

                if (note != null)
                {
                    if (note.IsPin == true)
                    {
                        note.IsPin = false;
                    }
                    else
                    {
                        note.IsPin = true;
                    }

                }
                await fundoosContext.SaveChangesAsync();
                return await fundoosContext.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // Trash Note
        public async Task<Note> TrashNote(int noteId, int userId)
        {
            try
            {
                var note = fundoosContext.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (note != null)
                {
                    if (note.IsTrash == false)
                    {
                        note.IsTrash = true;
                    }
                    else
                    {
                        note.IsTrash = false;
                    }
                    await fundoosContext.SaveChangesAsync();
                    return await fundoosContext.Notes.Where(a => a.NoteId == noteId).FirstOrDefaultAsync();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Get all Notes
        public async Task<List<Note>> GetAllNote(int userId)
        {
            try
            {
                return await fundoosContext.Notes.Where(u => u.UserId == userId).Include(u => u.user).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Remainder Date
        public async Task Remainder(int userId, int noteId, DateTime RemainderDate)
        {
            try
            {
                var note = fundoosContext.Notes.FirstOrDefault(u => u.NoteId == noteId && u.UserId == userId);
                if (note != null)
                {
                    if(note.IsRemainder == true)
                    {
                        note.RemainderDate = RemainderDate;
                    }
                }
                await fundoosContext.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        async Task<List<Note>> INoteRL.GetAllNoteRedis(int userId)
        {
            try
            {
                return await fundoosContext.Notes.Where(u => u.UserId == userId).Include(u => u.user).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
