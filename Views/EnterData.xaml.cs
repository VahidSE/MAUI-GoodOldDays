using MauiApp1.Model;
using Microsoft.VisualBasic;
using MauiApp1.GOD.BAL;

namespace MauiApp1.Views;

public partial class EnterData : ContentPage
{
	TrackerBAL trackerBAL = null;
	public EnterData()
	{
		InitializeComponent();
		trackerBAL = new TrackerBAL();
		pickTransactionType.SelectedIndex = 0;
		dateOfTransaction.Date = DateTime.Now;
	}

    private void btnSave_Clicked(object sender, EventArgs e)
    {
		int amount = Convert.ToInt32(txtAmount.Text);
		string description = txtDescription.Text;
		string transactiotype = pickTransactionType.SelectedItem.ToString();
        int year = dateOfTransaction.Date.Year;
		int month = dateOfTransaction.Date.Month;
		string color = transactiotype.ToLower() == "credit" ? "Green" : "Red";

        CashTracker ct = new CashTracker()
		{
			Amount = amount,
			TransactionDescription = description,
			TransactionDate = Convert.ToString(dateOfTransaction.Date),
			TransactionType = transactiotype,
            TxYear = year,
			TxMonth = month,
            TxColor = color
		};
        trackerBAL.EnterTransaction(ct);
		Navigation.PopAsync();
    }
}