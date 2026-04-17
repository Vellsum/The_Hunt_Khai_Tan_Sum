namespace The_Hunt_Khai_Tan_Sum;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        Current.UserAppTheme = AppTheme.Light;
    }
    protected override void OnSleep()
    {
        base.OnSleep();

        var runInBackground = Preferences.Default.Get("run_in_background", false);

        if (!runInBackground)
        {
            // stop hunt if user leaves app
            Preferences.Default.Set("hunt_interrupted", true);
        }
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}