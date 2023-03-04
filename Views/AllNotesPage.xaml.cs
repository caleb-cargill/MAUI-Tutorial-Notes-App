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
		if (BindingContext is AllNotes allNotes)
			allNotes.LoadNotes();
    }

	private async void Add_Clicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(NotePage));
	}

	private async void notesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection.Any())
		{
			var note = e.CurrentSelection[0] as Note;

			await Shell.Current.GoToAsync($"{nameof(NotePage)}?{nameof(NotePage.ItemId)}={note.FileName}");

			notesCollection.SelectedItem = null;
		}
	}
}
