using Notes.Models;

namespace Notes.Views;

public partial class AllNotesPage : ContentPage
{
	public AllNotesPage()
	{
		InitializeComponent();

		BindingContext = new AllNotes();
	}

    protected override void OnAppearing()
    {
		try
		{
			if (BindingContext is AllNotes allNotes)
				allNotes.LoadNotes();
		}
		catch (Exception ex)
		{
			throw new Exception("Error occurred while loading existing notes", ex);
		}
    }

	private async void Add_Clicked(object sender, EventArgs e)
	{
		try
		{
			await Shell.Current.GoToAsync(nameof(NotePage));
		}
		catch (Exception ex)
		{
			throw new Exception("Error occurred while adding new Note", ex);
		}
	}

	private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		try
		{
			if (e.CurrentSelection.Any())
			{
				var note = e.CurrentSelection[0] as Note;

				await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.FileName}");

				notesCollection.SelectedItem = null;
			}
		}
		catch (Exception ex)
		{
			throw new Exception("Error occurred while selecting existing Note", ex);
		}
	}
}
