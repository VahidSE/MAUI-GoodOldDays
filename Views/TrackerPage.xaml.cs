using MauiApp1.Model;
using MauiApp1.GOD.BAL;
using CommunityToolkit.Maui.Storage;

namespace MauiApp1.Views;

public partial class TrackerPage : ContentPage
{
    TrackerBAL trackerBAL = null;
    public TrackerPage()
	{
		InitializeComponent();
        trackerBAL = new TrackerBAL();
        LoadData();
    }

	protected override void OnAppearing()
	{ 
	    base.OnAppearing();
        LoadData();
    }

    private void LoadData()
    {
        int _year = DateTime.Now.Year;
        int _month = DateTime.Now.Month;
        List<CashTracker> items = trackerBAL.GetTxByMonthAndYear(_year, _month);
        TransactionList.ItemsSource = items;
        UpdateTotals(items);
        LoadYears();
        LoadMonths();    
    }

    private void LoadYears()
    {
        int appDesignedYear = 2024;
        int currentYear = DateTime.Now.Year;
        List<int> years = new List<int>();
        for (int yr = appDesignedYear; yr <= currentYear; yr++)
        {
            years.Add(yr);
        }
        pickerYear.ItemsSource = years;
        pickerYear.SelectedItem = currentYear;
    }
    private void LoadMonths()
    {
        List<int> months = new List<int> { 1,2,3,4,5,6,7,8,9,10,11,12};   
        pickerMonth.ItemsSource = months;
        pickerMonth.SelectedItem = DateTime.Now.Month;
    }

    private void UpdateTotals(List<CashTracker> items)
    {
        try
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
        catch (Exception ex)
        {
            DisplayAlert("Error!", ex.Message.ToString(), "close");
        }
    }
    private async void TransactionList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var item = (CashTracker)e.SelectedItem;
            string userResponse = await DisplayActionSheet("Delete transaction?", "NO", null, new string[] { "Delete" });
            if (userResponse == "Delete")
            {
                trackerBAL.DeleteTransaction(item);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error!", ex.Message.ToString(), "close");
        }
    }

    private void btnFilter_Clicked(object sender, EventArgs e)
    {
        try
        {
            int _year = Convert.ToInt32(pickerYear.SelectedItem.ToString());
            int _month = Convert.ToInt32(pickerMonth.SelectedItem.ToString());
            List<CashTracker> items = trackerBAL.GetTxByMonthAndYear(_year, _month);
            TransactionList.ItemsSource = items;
            UpdateTotals(items);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error!", ex.Message.ToString(), "close");
        }
    }

    private void btnAll_Clicked(object sender, EventArgs e)
    {
        try
        {
            List<CashTracker> items = trackerBAL.GetTransactions();
            TransactionList.ItemsSource = items;
            UpdateTotals(items);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error!", ex.Message.ToString(), "close");
        }
    }

    private async void btnDownload_Clicked(object sender, EventArgs e)
    {
        int _year = Convert.ToInt32(pickerYear.SelectedItem.ToString());
        int _month = Convert.ToInt32(pickerMonth.SelectedItem.ToString());
        List<CashTracker> items = trackerBAL.GetTxByMonthAndYear(_year, _month);
        string filePath = trackerBAL.PreparePDF(items);
        using var stream = trackerBAL.ConvertPdfToStream(filePath);
        FileSaverResult result = await FileSaver.Default.SaveAsync("DocName.pdf", stream, CancellationToken.None);
        if (result.IsSuccessful)
        {
            await DisplayAlert("Success", $"The file was saved successfully to location: {result.FilePath}", "OK");
        }
        else
        {
            await DisplayAlert("Error", $"The file was not saved successfully with error: {result.Exception.Message}", "OK");
        }
    }

    //private async void ToolbarItem_Clicked(object sender, EventArgs e)
    //{
    //    await DisplayAlert("Greetings!", "New feature will be added soon.!", "close");
    //}


}