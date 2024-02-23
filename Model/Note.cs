using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
