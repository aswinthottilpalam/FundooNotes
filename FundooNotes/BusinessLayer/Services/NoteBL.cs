using BusinessLayer.Interfaces;
using CommonLayer;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public async Task AddNote(int userId, NotePostModel notePostModel)
        {
            try
            {
                await this.noteRL.AddNote(userId, notePostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Archive Notes
        public async Task ArchiveNote(int userId, int noteId)
        {
            try
            {
                await this.noteRL.ArchiveNote(userId, noteId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Change Color
        public async Task ChangeColor(int userId, int noteId, string color)
        {
            try
            {
                await this.noteRL.ChangeColor(userId, noteId, color);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Delete note
        public bool DeleteNote(int noteId)
        {
            try
            {
                if (noteRL.DeleteNote(noteId))
                    return true;
                else
                    return false;
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
                return await this.noteRL.GetNote(noteId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // Update notes
        public async Task<Note> UpdateNote(int noteId, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                return await this.noteRL.UpdateNote(noteId, noteUpdateModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
