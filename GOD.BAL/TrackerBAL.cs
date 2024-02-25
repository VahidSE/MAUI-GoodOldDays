using MauiApp1.GOD.DAL;
using MauiApp1.Interface;
using MauiApp1.Model;
using SkiaSharp;

namespace MauiApp1.GOD.BAL
{
    internal class TrackerBAL : ITracker
    {
        DBRepository dB = null;
        public TrackerBAL() 
        {
            dB = new DBRepository();
        }

        public void DeleteTransaction(CashTracker cashTracker)
        {
            dB.DeleteTransaction(cashTracker);
        }

        public void EnterTransaction(CashTracker cashTracker)
        {
            dB.EnterTransaction(cashTracker);
        }

        public List<CashTracker> GetTransactions()
        {
            return dB.GetTransactions();
        }

        public List<CashTracker> GetTxByMonthAndYear(int year, int month)
        {
            return dB.GetTxByMonthAndYear(year, month);
        }

        public int[] GetTxMonths()
        {
            return dB.GetTxMonths();
        }

        public int[] GetTxYears()
        {
            return dB.GetTxYears();
        }

        public string PreparePDF(List<CashTracker> cashTrackers)
        {
            string fileName = Guid.NewGuid().ToString() + ".pdf";
            string filePath = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, fileName);
            var document = SKDocument.CreatePdf(filePath);

            // Start a page with dimensions 840 x 1188 (A4 size).
            var canvas = document.BeginPage(840, 1188);

            var paintHeader = new SKPaint
            {
                Color =  SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.StrokeAndFill,
                TextAlign = SKTextAlign.Center,
                TextSize = 20
            };
            var positionOfText = new SKPoint(400, 30);
            canvas.DrawText("Statement", positionOfText, paintHeader);


            int directionX = 60;
            int directionY = 90;
            
            foreach (var c in cashTrackers)
            {
                // Draw some text on the page.
                var paint = new SKPaint
                {
                    Color = (c.TxColor == "Red")? SKColors.Red : SKColors.Green,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Left,
                    TextSize = 16
                };
                directionY += 20; 
                DateTime dt = Convert.ToDateTime(c.TransactionDate);
                var text = dt.ToString("dd-MM-yyyy");
                var textWidth = paint.MeasureText(text);               
                var textPosition = new SKPoint(directionX, directionY);
                canvas.DrawText(text, textPosition, paint);

                var text2 = c.TransactionType;
                var textWidth2 = paint.MeasureText(text2);
                var textPosition2 = new SKPoint(directionX + 195, directionY);
                canvas.DrawText(text2, textPosition2, paint);

                var text3 = c.TransactionDescription;
                var textWidth3 = paint.MeasureText(text3);
                var textPosition3 = new SKPoint(directionX + 390, directionY);
                canvas.DrawText(text3, textPosition3, paint);

                var text4 = c.Amount + "/-";
                var textWidth4 = paint.MeasureText(text4);
                var textPosition4 = new SKPoint(directionX + 585, directionY);
                canvas.DrawText(text4, textPosition4, paint);
            }

            var result = cashTrackers
                        .GroupBy(x => x.TxColor)
                        .Select(group => new { TransactionType = group.Key, Amount = group.Sum(x => x.Amount) });
            var paintFooter = new SKPaint
            {
                Color = SKColors.DarkBlue,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Right,
                TextSize = 16
            };

            foreach (var item in result)
            {
                directionY += 25;
                if (item.TransactionType == "Green")
                {
                    var savingText = $"saved: {item.Amount} /-";
                    var posSavingText = new SKPoint(directionX + 650, directionY);
                    canvas.DrawText(savingText, posSavingText, paintFooter);
                }
                if (item.TransactionType == "Red")
                {
                    var spentText = $"spent: {item.Amount} /-";
                    var posSpentText = new SKPoint(directionX + 650, directionY);
                    canvas.DrawText(spentText, posSpentText, paintFooter);
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
            // Open the file and convert it to a Stream.
            var stream = File.OpenRead(filePath);
            return stream;
        }
    }

   
}
