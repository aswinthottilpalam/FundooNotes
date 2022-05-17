using CommonLayer;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        Task AddNote(int userId, NotePostModel notePostModel);
        bool DeleteNote(int noteId);
        Task ChangeColor(int userId, int noteId, string color);
        Task ArchiveNote(int noteId);
        Task<Note> UpdateNote(int noteId, NoteUpdateModel noteUpdateModel);
        Task<Note> GetNote(int noteId);
        Task<Note> PinNote(int noteId);
        Task Remainder(int userId, int noteId, DateTime RemainderDate);
        Task<Note> TrashNote(int noteId, int userId);
        Task<List<Note>> GetAllNote(int userId);
        Task<List<Note>> GetAllNoteRedis(int userId);


    }
}
