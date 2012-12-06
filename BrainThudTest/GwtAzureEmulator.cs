using System.Configuration;
using System.Diagnostics;
using Microsoft.WindowsAzure;

namespace BrainThudTest
{
    public abstract class GwtAzureEmulator : Gwt
    {
        public override void SubInitialize()
        {
            var count = Process.GetProcessesByName("DFService").Length;
            if (count == 0)
            {
                var start = new ProcessStartInfo
                {
                    Arguments = "/devstore:start",
                    FileName = @"C:\Program Files\Microsoft SDKs\Windows Azure\Emulator\csrun.exe"
                };

                var proc = new Process { StartInfo = start };
                proc.Start();
                proc.WaitForExit();
            }

            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                var connectionString = ConfigurationManager.AppSettings[configName];
                configSetter(connectionString);
            });

            base.SubInitialize();
        }
    }
}