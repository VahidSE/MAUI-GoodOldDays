using System.IO;
using System;
using SkiaSharp;
using Microsoft.Maui;
using System.Text;
using CommunityToolkit.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using CommunityToolkit.Maui.Storage;

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
        sb.AppendLine($"Idiom: {DeviceInfo.Current.Idiom}");
        sb.AppendLine($"Platform: {DeviceInfo.Current.Platform}");

        bool isVirtual = DeviceInfo.Current.DeviceType switch
        {
            DeviceType.Physical => false,
            DeviceType.Virtual => true,
            _ => false
        };
        sb.AppendLine($"Virtual device? {isVirtual}");

        // Display the device info (you can update this based on your UI)
        lblDevice.Text = sb.ToString();

        var sb2 = new System.Text.StringBuilder();
        sb2.AppendLine($"App Name: {AppInfo.Current.Name}");
        sb2.AppendLine($"Package Name: {AppInfo.Current.PackageName}");
        sb2.AppendLine($"App Version: {AppInfo.Current.VersionString}");
        sb2.AppendLine($"App Build: {AppInfo.Current.BuildString}");
        lblApp.Text = sb2.ToString();
    }

    private void btnDeleteNotes_Clicked(object sender, EventArgs e)
    {

    }

    private void btnDeleteTasks_Clicked(object sender, EventArgs e)
    {

    }

    private void btnDeleteTracker_Clicked(object sender, EventArgs e)
    {

    }

    private async void btnDownloadTracker_Clicked(object sender, EventArgs e)
    {
        string filePath = CreatePDFDocument();
        using var stream = ConvertPdfToStream(filePath);
        //var text2 = "Hello, World!";
        //using var stream = new MemoryStream(Encoding.Default.GetBytes(text2));
        FileSaverResult result = await FileSaver.Default.SaveAsync("DocName.pdf", stream, CancellationToken.None);
        if (result.IsSuccessful)
        {
            await DisplayAlert("Success", $"The file was saved successfully to location: {result.FilePath}", "OK");
        }
        else
        {
            await DisplayAlert("Error", $"The file was not saved successfully with error: {result.Exception.Message}", "OK");
        }

    }
    public string CreatePDFDocument()
    {
        string fileName = Guid.NewGuid().ToString() + ".pdf";
        string filePath = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, fileName);
        var document = SKDocument.CreatePdf(filePath);

        // Start a page with dimensions 840 x 1188 (A4 size).
        var canvas = document.BeginPage(840, 1188);

        // Draw some text on the page.
        var paint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Center,
            TextSize = 24
        };
        var text = "Hello, World!";
        var textWidth = paint.MeasureText(text);
        var textPosition = new SKPoint(420, 594);
        canvas.DrawText(text, textPosition, paint);

        // Draw a table.
        var tablePaint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            StrokeWidth = 2
        };
        var cellWidth = 100;
        var cellHeight = 50;
        var tableStart = new SKPoint(100, 700);
        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                var rect = new SKRect(tableStart.X + i * cellWidth, tableStart.Y + j * cellHeight, tableStart.X + (i + 1) * cellWidth, tableStart.Y + (j + 1) * cellHeight);
                canvas.DrawRect(rect, tablePaint);
            }
        }

        // End the page.
        document.EndPage();

        // End the document.
        document.Close();
        document.Dispose();
        return filePath;
    }
    public Stream ConvertPdfToStream(string filePath)
    {
        //// Get the path to the PDF file in the application's data directory.
        //var path = Path.Combine(FileSystem.AppDataDirectory, filename);

        // Open the file and convert it to a Stream.
        var stream = File.OpenRead(filePath);

        return stream;
    }
}

