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

        //bool DeleteNote(int noteId);

        Task ChangeColor(int userId, int noteId, string color);

        Task ArchiveNote(int userId, int noteId);

        //Task<Note> UpdateNote(int userId, int noteId, NoteUpdateModel noteUpdateModel);
    }
}
