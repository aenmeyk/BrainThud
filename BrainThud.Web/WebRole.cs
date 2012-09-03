using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace BrainThud.Web
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            CloudStorageAccount.SetConfigurationSettingPublisher((configName, configSetter) =>
            {
                string connectionString;

                if (RoleEnvironment.IsAvailable)
                {
                    connectionString = RoleEnvironment.GetConfigurationSettingValue(configName);
                }
                else
                {
                    connectionString = ConfigurationManager.AppSettings[configName];
                }

                configSetter(connectionString);
            });

            return base.OnStart();
        }
    }
}
