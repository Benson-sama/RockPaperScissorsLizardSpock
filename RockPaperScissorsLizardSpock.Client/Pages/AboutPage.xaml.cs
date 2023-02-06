namespace RockPaperScissorsLizardSpock.Client.Pages;

public partial class AboutPage : ContentPage
{
    private int count = 0;

    public AboutPage() => InitializeComponent();

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count is 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
