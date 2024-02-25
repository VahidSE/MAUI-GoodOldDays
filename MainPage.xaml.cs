namespace MauiApp1;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"Model: {DeviceInfo.Current.Model}");
        sb.AppendLine($"Manufacturer: {DeviceInfo.Current.Manufacturer}");
        sb.AppendLine($"Name: {DeviceInfo.Current.Name}");
        sb.AppendLine($"OS Version: {DeviceInfo.Current.VersionString}");
        lblDevice.Text = sb.ToString();

        var sb2 = new System.Text.StringBuilder();
        sb2.AppendLine($"App Name: {AppInfo.Current.Name}");
        sb2.AppendLine($"App Version: {AppInfo.Current.VersionString}");     
        lblApp.Text = sb2.ToString();
    }

}
   

