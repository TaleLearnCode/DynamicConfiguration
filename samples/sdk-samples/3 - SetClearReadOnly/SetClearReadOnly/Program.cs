using Azure;
using Azure.Data.AppConfiguration;
using HelloWorld;
using System;

namespace SetClearReadOnly
{
	class Program
	{
		static void Main(string[] args)
		{

			// Create a ConfigurationClient
			var client = new ConfigurationClient(Settings.AppConfigConnectionString);

			// Create a configuration setting
			var setting = new ConfigurationSetting("Sample3", "Third Sample");
			client.SetConfigurationSetting(setting);

			// Make the setting read-only
			client.SetReadOnly(setting.Key, true);

			// Try modify read-only setting
			setting.Value = "new_value";
			try
			{
				client.SetConfigurationSetting(setting);
			}
			catch (RequestFailedException e)
			{
				Console.WriteLine(e.Message);
			}

			// Make the setting read-write
			client.SetReadOnly(setting.Key, false);

			// Modify read-write setting
			client.SetConfigurationSetting(setting);

			// Delete a configuration setting
			client.DeleteConfigurationSetting("Sample3");


		}
	}
}
