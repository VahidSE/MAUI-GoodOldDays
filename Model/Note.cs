using SQLite;

namespace MauiApp1.Model
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int NoteId { get; set; }
        public string NoteName { get; set; } = string.Empty;
        public string NoteDescription { get; set; }
        public string NoteDate { get; set; }
        public bool IsNoteDeleted { get; set; }
    }
}
