using Azure.Data.AppConfiguration;
using System;

namespace HelloWorld
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new ConfigurationClient(Settings.AppConfigConnectionString);
			var setting = new ConfigurationSetting("HelloWorld", "Welcome to App Configuration!");

			client.SetConfigurationSetting(setting);  // Create a configuration setting

			ConfigurationSetting retrievedSetting = client.GetConfigurationSetting("HelloWorld");
			Console.WriteLine($"Thte value of the configuration setitng is {retrievedSetting.Value}");

			client.DeleteConfigurationSetting("HelloWorld");  // Delete a configuration setting

		}
	}
}