using MauiApp1.Model;

namespace MauiApp1.Views;

public partial class TrackerPage : ContentPage
{
	DBRepository dB = new DBRepository();
    public TrackerPage()
	{
		InitializeComponent();
        LoadData();
    }

	protected override void OnAppearing()
	{ 
	    base.OnAppearing();
        LoadData();
    }

    private void LoadData()
    {
        int year = DateTime.Now.Year; int month = DateTime.Now.Month;
        List<CashTracker> items = dB.GetTxByMonthAndYear(year, month);
        TransactionList.ItemsSource = items;
        UpdateTotals(items);
        pickYear.ItemsSource = dB.GetTxYears();
        pickYear.SelectedItem = year;
        pickMonth.ItemsSource = dB.GetTxMonths();
        pickMonth.SelectedItem = month;
    }

    private void UpdateTotals(List<CashTracker> items)
    {
        bool isGreenUpdated = false;
        bool isRedUpdated = false;

        var result = items
                   .GroupBy(x => x.TxColor)
                   .Select(group => new { TransactionType = group.Key, Amount = group.Sum(x => x.Amount) });
        foreach (var item in result)
        {
            if (item.TransactionType == "Green")
            {
                lblSaved.Text = $"Your savings: {item.Amount} /-";
                isGreenUpdated = true;
            }
            if (item.TransactionType == "Red")
            {
                lblSpent.Text = $"You spent: {item.Amount} /-";
                isRedUpdated = true;
            }
        }

        if (!isGreenUpdated)
            lblSaved.Text = "Your savings: 0 /-";
        if (!isRedUpdated)
            lblSpent.Text = "You spent 0 /-";
    }
    
    private void Button_Clicked(object sender, EventArgs e)
    {
		EnterData enterData = new EnterData();
		Navigation.PushAsync(enterData);
    }

    private async void TransactionList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var item = (CashTracker)e.SelectedItem;
        string userResponse = await DisplayActionSheet("Delete transaction?", "NO", null, new string[] { "Delete" });
        if (userResponse == "Delete")
        {
            dB.DeleteTransaction(item);
        }    
    }

    private void btnSearch_Clicked(object sender, EventArgs e)
    {
        int year = pickYear.SelectedItem != null ? System.Convert.ToInt32(pickYear.SelectedItem.ToString()) : 0;
        int month = pickMonth.SelectedItem != null ? System.Convert.ToInt32(pickMonth.SelectedItem.ToString()) : 0;
        List<CashTracker> items = dB.GetTxByMonthAndYear(year, month);
        TransactionList.ItemsSource = items;
        UpdateTotals(items);
    }

    private void btnAll_Clicked(object sender, EventArgs e)
    {
        List<CashTracker> items = dB.GetTransactions();
        TransactionList.ItemsSource = items;
        UpdateTotals(items);
    }

    //private async void ToolbarItem_Clicked(object sender, EventArgs e)
    //{
    //    await DisplayAlert("Greetings!", "New feature will be added soon.!", "close");
    //}
}