using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task Addlabel(int userId, int Noteid, string LabelName);
        Task DeleteLabel(int LabelId, int userId);
        Task<Label> UpdateLabel(int userId, int LabelId, string LabelName);
        Task<List<Label>> GetlabelByNoteId(int NoteId);
        Task<List<Label>> Getlabel(int userId);

    }
}
