using MauiApp1.Model;

namespace MauiApp1.Interface
{
    internal interface ITracker
    {
        void EnterTransaction(CashTracker cashTracker);
        void DeleteTransaction(CashTracker cashTracker);
        List<CashTracker> GetTransactions();
        int[] GetTxYears();
        int[] GetTxMonths();
        List<CashTracker> GetTxByMonthAndYear(int year, int month);
    }
}
