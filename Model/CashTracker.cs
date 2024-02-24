using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Model
{
    public class CashTracker
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string TransactionDescription { get; set; }
        public int Amount { get; set; }
        public string TransactionDate { get; set; }
        public int TxYear { get; set; }
        public int TxMonth { get; set; }
        public string TxColor { get; set; }

        public Color TxColorToDisplay {
            get {
                return (TxColor == "Red") ? Color.FromRgba("#ff0000") : Color.FromRgba("#5d782e");
                 }
        } 
    }



    public class DisplayGroupByData
    {
        public string TransactionType { get; set; } = "";
        public int Amount { get; set; } = 0;
    }
}
