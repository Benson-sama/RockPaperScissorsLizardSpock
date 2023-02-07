namespace RockPaperScissorsLizardSpock.Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window window = base.CreateWindow(activationState);

#if DEBUG && WINDOWS
        window.Width = 360;
        window.Height = 780;
        window.X = (2560 / 2) - (window.Width / 2);
        window.Y = (1440 / 2) - (window.Height / 2);
#endif

        return window;
    }
}
