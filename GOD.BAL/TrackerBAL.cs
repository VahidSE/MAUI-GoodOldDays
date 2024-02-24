using MauiApp1.GOD.DAL;
using MauiApp1.Interface;
using MauiApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.GOD.BAL
{
    internal class TrackerBAL : ITracker
    {
        DBRepository dB = null;
        public TrackerBAL() 
        {
            dB = new DBRepository();
        }

        public void DeleteTransaction(CashTracker cashTracker)
        {
            dB.DeleteTransaction(cashTracker);
        }

        public void EnterTransaction(CashTracker cashTracker)
        {
            dB.EnterTransaction(cashTracker);
        }

        public List<CashTracker> GetTransactions()
        {
            return dB.GetTransactions();
        }

        public List<CashTracker> GetTxByMonthAndYear(int year, int month)
        {
            return dB.GetTxByMonthAndYear(year, month);
        }

        public int[] GetTxMonths()
        {
            return dB.GetTxMonths();
        }

        public int[] GetTxYears()
        {
            return dB.GetTxYears();
        }
    }
}
