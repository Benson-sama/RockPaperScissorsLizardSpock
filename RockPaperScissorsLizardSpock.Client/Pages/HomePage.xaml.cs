using RockPaperScissorsLizardSpock.Client.ViewModels;

namespace RockPaperScissorsLizardSpock.Client.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(GameViewModel gameViewModel)
    {
        InitializeComponent();
        BindingContext = gameViewModel;
    }
}
