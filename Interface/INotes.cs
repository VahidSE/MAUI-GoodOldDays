using MauiApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Interface
{
    internal interface INotes
    {
        void CreateNote(Note note);
        void UpdateNote(Note note);
        void DeleteNote(Note note);
        List<Note> GetNotes();
    }
}
