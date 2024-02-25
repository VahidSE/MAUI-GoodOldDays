using MauiApp1.GOD.DAL;
using MauiApp1.Interface;
using MauiApp1.Model;

namespace MauiApp1.GOD.BAL
{
    public class NotesBAL:INotes
    {
        DBRepository dBRepository = null;
        public NotesBAL() 
        {
           dBRepository = new DBRepository();
        }

        public void CreateNote(Note note)
        {
            dBRepository.CreateNote(note);
        }

        public void DeleteNote(Note note)
        {
            dBRepository.DeleteNote(note);
        }

        public List<Note> GetNotes()
        {
            return dBRepository.GetNotes();
        }

        public void UpdateNote(Note note)
        {
            dBRepository.UpdateNote(note);
        }
    }
}
