namespace The_Hunt_Khai_Tan_Sum.Models;

public class UserSettings
{
    public bool IsTimerDelayEnabled { get; set; }
    public TimeSpan SelectedDelayTime { get; set; }
    public bool IsPoltergeistMode { get; set; } // False = Whisper, True = Poltergeist
    public bool IsDigitalLockdownEnabled { get; set; }
    public bool IsRunInBackgroundEnabled { get; set; }
}