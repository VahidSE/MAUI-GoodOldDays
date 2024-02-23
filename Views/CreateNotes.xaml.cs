using MauiApp1.Model;

namespace MauiApp1.Views;

public partial class CreateNotes : ContentPage
{
    private bool isUpdate = false;
    private Note _modifyNote = null;
	public CreateNotes()
	{
		InitializeComponent();
        btnSave.Text = "Save";
        btnCancle.Text = "Cancle";
    }
    public CreateNotes(Note note)
    {
        InitializeComponent();
        BindingContext = note;
        isUpdate = true;
        _modifyNote = note;
        btnSave.Text = "Update";
        btnCancle.Text = "Delete";
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            string _title = Title.Text;
            string _description = Description.Text;
            DBRepository dBRepository = new DBRepository();
            if (isUpdate)
            {
                if (!string.IsNullOrEmpty(_modifyNote.NoteName) || !string.IsNullOrEmpty(_modifyNote.NoteDescription))
                {
                    dBRepository.UpdateNote(_modifyNote);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Warning", "Please enter text.!", "close");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_title) || !string.IsNullOrEmpty(_description))
                {
                    Note item = new Note()
                    {
                        NoteName = _title,
                        NoteDescription = _description,
                        NoteDate = DateTime.Now.ToString(),
                        IsNoteDeleted = false
                    };
                    dBRepository.CreateNote(item);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Warning", "Please enter text.!", "close");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error!", ex.Message.ToString(), "close");
        }
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            if (isUpdate)
            {
                if (!string.IsNullOrEmpty(_modifyNote.NoteName) || !string.IsNullOrEmpty(_modifyNote.NoteDescription))
                {
                    DBRepository dBRepository = new DBRepository();
                    dBRepository.DeleteNote(_modifyNote);
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Warning", "Please keep text.!", "close");
                }
            }
            else
            {
                await Navigation.PopAsync();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error!", ex.Message.ToString(), "close");
        }

    }
}