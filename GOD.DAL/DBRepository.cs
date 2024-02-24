using MauiApp1.Model;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.GOD.DAL
{
    public class DBRepository
    {
        SQLiteConnection connection;
        public const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
        public DBRepository()
        {
            if (connection != null)
                return;

            string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "yourdbname.db3");
            connection = new SQLiteConnection(DatabasePath, Flags);
            connection.CreateTable<Note>();
            connection.CreateTable<CashTracker>();

        }

        public void CreateNote(Note item)
        {
            connection.Insert(item);
        }

        public void DeleteNote(Note item)
        {
            connection.Delete(item);
        }

        public List<Note> GetNotes()
        {
            List<Note> items = connection.Table<Note>().ToList();
            return items;
        }

        public void UpdateNote(Note item)
        {
            connection.Update(item);
        }

        public void EnterTransaction(CashTracker cashTracker)
        {
            connection.Insert(cashTracker);
        }

        public void DeleteTransaction(CashTracker cashTracker)
        {
            connection.Delete(cashTracker);
        }

        public List<CashTracker> GetTransactions()
        {
            List<CashTracker> items = connection.Table<CashTracker>().ToList();
            return items;
        }

        public int[] GetTxYears()
        {
            int[] arrYear = connection.Table<CashTracker>().Select(x => x.TxYear).Distinct().ToArray();
            return arrYear;
        }

        public int[] GetTxMonths()
        {
            int[] arrYear = connection.Table<CashTracker>().Select(x => x.TxMonth).Distinct().ToArray();
            return arrYear;
        }
        public List<CashTracker> GetTxByMonthAndYear(int year, int month)
        {
            string queryForData = "SELECT * FROM [CashTracker]";

            if (year != 0 && month != 0)
            {
                queryForData += " WHERE TxYear = " + year + " and TxMonth=" + month;
            }
            else if (year != 0 && month == 0)
            {
                queryForData += " WHERE TxYear = " + year;
            }
            else if (year == 0 && month != 0)
            {
                queryForData += " WHERE TxMonth=" + month;
            }
            List<CashTracker> items = connection.Query<CashTracker>(queryForData);

            return items;
        }

    }
}
