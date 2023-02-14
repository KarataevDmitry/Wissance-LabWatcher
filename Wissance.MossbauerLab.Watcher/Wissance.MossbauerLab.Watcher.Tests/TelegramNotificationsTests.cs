using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;

using Wissance.MossbauerLab.Watcher.Common.Data;
using Wissance.MossbauerLab.Watcher.Services.Config;
using Wissance.MossbauerLab.Watcher.Services.Notification;
using Microsoft.QualityTools.Testing.Fakes;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Wissance.MossbauerLab.Watcher.Tests
{
    [TestClass]
    public class TelegramNotificationsTests
    {
        [TestMethod]
        public async Task SendNotificationTest()
        {
            ApplicationConfig config = new ApplicationConfig
            {
                NotificationSettings = new NotificationConfig()
                {
                    TelegramSettings = new TelegramConfig("@WissanceBotTest", "Templates\\TelegramMessageTemplate.txt"),
                    Threshold = 2
                }
            };




            SpectrumReadyData spectra = new SpectrumReadyData
            {
                Spectrum = new byte[] { 4, 5, 6 },
                Name = "Test spectra",
                Channel = 1,
                Updated = new DateTime(2023, 2, 14, 14, 30, 0),
                RawInfo = new System.IO.FileInfo("textFileForFileInfo.txt")
            };
           
            TelegramNotifier telegramNotifier = new TelegramNotifier(config, null);
            bool result = await telegramNotifier.NotifySpectrumSavedAsync(new List<SpectrumReadyData> { spectra, spectra });
            Assert.IsTrue(result);

        }
    }
}
