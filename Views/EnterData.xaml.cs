using MauiApp1.Model;
using Microsoft.VisualBasic;

namespace MauiApp1.Views;

public partial class EnterData : ContentPage
{
	DBRepository dbRepository;
	public EnterData()
	{
		InitializeComponent();
		pickTransactionType.SelectedIndex = 0;
		dateOfTransaction.Date = DateTime.Now;
        dbRepository = new DBRepository();
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
        dbRepository.EnterTransaction(ct);
		Navigation.PopAsync();
    }
}