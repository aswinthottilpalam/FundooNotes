using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL LabelRL;
        public LabelBL(ILabelRL ilabelRL)
        {
            this.LabelRL = ilabelRL;
        }

        // Add Label
        public async Task Addlabel(int userId, int Noteid, string LabelName)
        {
            try
            {
                await this.LabelRL.Addlabel(userId, Noteid, LabelName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Delete Label
        public async Task DeleteLabel(int LabelId, int userId)
        {
            try
            {
                await this.LabelRL.DeleteLabel(LabelId, userId);
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
                return await this.LabelRL.Getlabel(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Get Label by note Id
        public async Task<List<Label>> GetlabelByNoteId(int NoteId)
        {
            try
            {
               return await this.LabelRL.GetlabelByNoteId(NoteId);
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
               return await this.LabelRL.UpdateLabel(userId, LabelId, LabelName); 
            }
            catch (Exception e) 
            {

                throw e;
            }
        }


    }
}
