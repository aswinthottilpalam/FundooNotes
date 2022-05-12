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
        public async Task ArchiveNote(int noteId)
        {
            try
            {
                await this.noteRL.ArchiveNote(noteId);
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

        // Pin note
        public async Task<Note> PinNote(int noteId)
        {
            try
            {
               return await this.noteRL.PinNote(noteId);
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
                return await this.noteRL.TrashNote(noteId, userId);
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

        // Get all notes
        public async Task<List<Note>> GetAllNote(int userId)
        {
            try
            {
                return await this.noteRL.GetAllNote(userId);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // Remainder note
        public async Task Remainder(int userId, int noteId, DateTime RemainderDate)
        {
            try
            {
                await this.noteRL.Remainder(userId, noteId, RemainderDate);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
