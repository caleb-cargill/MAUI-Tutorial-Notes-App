using Notes.Models;

namespace Notes.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    private async void LearnMore_Clicked(System.Object sender, System.EventArgs e)
    {
		try
		{
			if (BindingContext is About about)
				await Launcher.Default.OpenAsync(about.MoreInfoURL);
		}
		catch (Exception ex)
		{
			throw new Exception(message: "Error displaying more information", innerException: ex);
		}
    }
}
