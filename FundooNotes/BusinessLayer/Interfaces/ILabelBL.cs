﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task Addlabel(int userId, int Noteid, string LabelName);
        Task DeleteLabel(int LabelId, int userId);

    }
}
