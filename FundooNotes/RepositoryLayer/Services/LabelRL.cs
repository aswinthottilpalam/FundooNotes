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
    }
}
