using MauiApp1.GOD.BAL;
using MauiApp1.Model;

namespace MauiApp1.Views;

public partial class NotesPage : ContentPage
{
    NotesBAL notesBAL = null;

    public NotesPage()
	{
        try
        {
            InitializeComponent();
            notesBAL = new NotesBAL();
            LoadData();
        }
        catch (Exception ex)
        {
           
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadData();
    }
    private async void LoadData()
    {
        try
        {
            List<Note> items = notesBAL.GetNotes();
            NotesList.ItemsSource = items;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error!", ex.Message.ToString(), "close");
        }
    }

    public async void OnFabClicked(object sender, EventArgs e)
    {
        try
        {
            CreateNotes cnPage = new CreateNotes();
            await Navigation.PushAsync(cnPage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error!", ex.Message.ToString(), "close");
        }
    }

    public async void NotesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var item = (Note)e.SelectedItem;
            var nextPage = new CreateNotes(item);
            await Navigation.PushAsync(nextPage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error!", ex.Message.ToString(), "close");
        }
    }

    
}