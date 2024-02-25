using MauiApp1.Model;

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
