using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using QRCoder;

namespace DiverBoard.Services
{
    public class QRCodesService
    {
        public static void GenerateQRCodes(List<string> bunks)
        {
            string qrCodesDirectory = SettingsService.QRCodesDirectory;
            if (Directory.Exists(qrCodesDirectory))
            {
                Directory.Delete(qrCodesDirectory, true);
            }
            Directory.CreateDirectory(qrCodesDirectory);

            bunks.Add("COMMAND-SPLASH");
            bunks.Add("COMMAND-UNSPLASH");
            bunks.Add("COMMAND-CLIMB");
            bunks.Add("COMMAND-UNCLIMB");
            foreach (var bunk in bunks)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode($"Y{bunk}Z", QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(4);
                qrCodeImage.Save(Path.Combine(qrCodesDirectory, $"{bunk}.png"));
            }
        }
    }
}
