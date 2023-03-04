using Notes.Models;

namespace Notes.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{

    public NotePage()
	{
		InitializeComponent();

        string appDirectory = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";
        LoadNote(Path.Combine(appDirectory, randomFileName));
    }

    public string ItemId
    {
        set => LoadNote(value);
    }

    private void LoadNote(string fileName)
    {
        Note noteModel = new Note() { FileName = fileName };

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }

	private async void SaveButton_Clicked(object sender, System.EventArgs e)
	{
		try
		{
            if (BindingContext is Note note)
                File.WriteAllText(note.FileName, TextEditor.Text);

            await Shell.Current.GoToAsync("..");
		}
		catch (Exception ex)
		{
			throw new Exception("Error occurred while saving note", ex);
		}
	}

    private async void DeleteButton_Clicked(object sender, System.EventArgs e)
    {
        try
        {
            if (BindingContext is Note note)
                if (File.Exists(note.FileName))
                    File.Delete(note.FileName);

            TextEditor.Text = string.Empty;

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred while saving note", ex);
        }
    }
}
